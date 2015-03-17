using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gNotas : MonoBehaviour 
{
	public static gNotas s;
	
	public List<Nota>	_prefabNotas;

//	public Nota			notaX1;
//	public Nota			notaX2;
//	public Nota			notaX3;
//	public Nota			notaX4;
//	public Nota			notaPausa;
	public List<Nota>	notasNaPista;
	
	public bool dbg = false;
	
	public bool drawAreaDePontuacao = false;
	public float	porcentagemNaTela = .19f;
	public Vector2 offsetPontuacao;
	public Rect areaDePontuacao;

	public bool drawAreaDeDead = false;
	public float	porcentagemNaTelaDead = .05f;
	public Vector2 offsetDead;
	public Rect areaDeDead;

	public int verificarXNotasPorVez = 1;

	public int currentNota = 0;
	
	public int quantidadeDeNotasNaPista;

	public void Awake()
	{
		s = this;
	}

	void OnEnable()
	{
		gComandosDeMusica.onPlay += Init;
		gGame.onReset += Resetar;
		gGame.onPauseGame += OnPause;
		
	}
	
	void OnDisable()
	{
		gComandosDeMusica.onPlay -= Init;
		gGame.onReset -= Resetar;
		gGame.onPauseGame -= OnPause;
	}
	
		
	public void OnPause( bool estado )
	{
		
	}

	void Resetar ()
	{
		if( notasNaPista.Count > 0 )
		{
			notasNaPista.ForEach( delegate( Nota n ) 
			{
				Destroy( n.gameObject );
			});
		}
		notasNaPista = new List<Nota>();
		currentNota = 0;
		quantidadeDeNotasNaPista = 0;
		
	
	}

	void Init ()
	{
//		notasNaPista = new List<Nota> ();
		quantidadeDeNotasNaPista = notasNaPista.FindAll( e => e.mInfo.tipo != TipoDeNota.PAUSA ).Count;
		currentNota = 0;
		
		areaDePontuacao.width = Screen.width * porcentagemNaTela;
		areaDePontuacao.height = Screen.height;
		
		areaDeDead.width = Screen.width * porcentagemNaTelaDead;
		areaDeDead.height = Screen.height;
		
		areaDePontuacao.position 	= new Vector2( offsetPontuacao.x * Screen.width, offsetPontuacao.y * Screen.height );
		areaDeDead.position 		= new Vector2( offsetDead.x * Screen.width, offsetDead.y * Screen.height );
	
		verificarXNotasPorVez = notasNaPista.Count;	
	}

	void OnGUI()
	{
		if( dbg ) 
		{
			areaDePontuacao.width = Screen.width * porcentagemNaTela;
			areaDePontuacao.height = Screen.height;
			
			areaDeDead.width = Screen.width * porcentagemNaTelaDead;
			areaDeDead.height = Screen.height;
			
			areaDePontuacao.position 	= new Vector2( offsetPontuacao.x * Screen.width, offsetPontuacao.y * Screen.height );
			areaDeDead.position 		= new Vector2( offsetDead.x * Screen.width, offsetDead.y * Screen.height );
			
		}

		if( drawAreaDePontuacao )
			GUI.Box( areaDePontuacao, "" );
		if( drawAreaDeDead )
			GUI.Box( areaDeDead, "" );
		
			
		if (gGame.s.gameStarted == false)
						return;
		
		
		int c = Mathf.Clamp (verificarXNotasPorVez, 1, notasNaPista.Count);
		
		for (int i = 0; i < c; i++) 
		{
			if( c > notasNaPista.Count )
			{
				return;
			}
			
//			if( notasNaPista[i].kill ) continue;
			
			Vector3 posNota = UICamera.mainCamera.WorldToScreenPoint( notasNaPista[i].transform.position );
			
			if( notasNaPista[i].mInfo.tipo != TipoDeNota.PAUSA )
			{	
				if( areaDePontuacao.Contains( posNota ) && notasNaPista[i].kill == false) 
				{
					
					VerificarPontuacao(posNota, notasNaPista[i] );
				}
			}
			
			VerificarFimDePercurso(posNota, notasNaPista[i]);

		}
	}

	void VerificarFimDePercurso (Vector3 posNota, Nota nota)
	{
		if( (int)nota.mInfo.tipo == (int)TipoDeNota.NOTA )
		{
			if( areaDeDead.Contains( posNota ) && nota.kill == false ) 
			{				
				nota.kill = true;
				
				if( nota.mInfo.tipo != TipoDeNota.PAUSA )
				{
					gAudio.s.PararAudio();						
					gPontuacao.s.CancelarPontos();
				}
				DestruirNota( nota );				
			}
		}
		else if((int)nota.mInfo.tipo > (int)TipoDeNota.NOTA )
		{
			if( areaDeDead.Contains( posNota ) && gPontuacao.s.pontuandoNotaLonga == false && nota.pontuando == false && gPontuacao.s.VerificarPontuacao(nota,
			gGame.s.player) ) 
			{				
				nota.kill = true;				
				DestruirNota( nota );				
			}
		}	
	}

	void VerificarPontuacao (Vector3 posNota, Nota nota)
	{
		switch( nota.mInfo.tipo )
		{
			case TipoDeNota.NOTA:
				VerificarNotaComum( posNota, nota );
				break;
			case TipoDeNota.NOTA_X2:
				VerificarNotaLonga(posNota, nota);
				break;
			default:
				VerificarNotaLonga(posNota, nota);
				break;
		}
	}

	void VerificarNotaComum (Vector3 posNota, Nota nota)
	{
		if( gPontuacao.s.VerificarPontuacao( nota, gGame.s.player ))
		{
			gPontuacao.s.Pontuar( nota, gGame.s.player );
			DestruirNota(nota);
		}
	}

	void VerificarNotaLonga (Vector3 posNota, Nota nota)
	{
		if( gPontuacao.s.VerificarPontuacao( nota, gGame.s.player ))
		{
			gPontuacao.s.PontuarNotaLonga( nota, gGame.s.player );
		}
	}

	void VerificarBrilhoDaPista (Nota n)
	{
		if( (int) n.mInfo.timbre == gGame.s.player.mController.pista )
		{
			gPista.s.FeedbackPista( (int) n.mInfo.timbre );
		}
	}
	
	public Nota NovaNota( NotaInfo info)
	{
		return InstanciarNota( info );	
	}


	Nota InstanciarNota (NotaInfo info)
	{
		Nota ret = Instantiate ( _prefabNotas.Find( e => e.mInfo.tipo == info.tipo ) ) as Nota;
		ret.gameObject.transform.parent = gPista.s.rootPista.transform;
		ret.mInfo = info;

		notasNaPista.Add (ret);
		
		return ret;
	}


	void DestruirNota (Nota nota)
	{
		if (notasNaPista.Contains (nota))
						notasNaPista.Remove (nota);

		currentNota++;// = Mathf.Clamp(currentNota++,0,notasNaPista.Count);

		gPontuacao.s.pontuandoNotaLonga = false;
		
		Destroy (nota.gameObject);
	}
}
