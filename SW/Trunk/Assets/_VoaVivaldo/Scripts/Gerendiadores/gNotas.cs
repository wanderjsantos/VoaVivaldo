using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gNotas : MonoBehaviour 
{
	public static gNotas s;
	
	public List<Nota>	_prefabNotas;

	public List<Nota>	notasNaPista;
	
	public bool dbg = false;
	
	public bool drawAreaDePontuacao = false;
	public float	tamanhoAreaDePontuacao = 30f;
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

	public GameObject root;
	void Init ()
	{
//		notasNaPista = new List<Nota> ();
		quantidadeDeNotasNaPista = notasNaPista.FindAll( e => e.mInfo.tipo != TipoDeNota.PAUSA ).Count;
		currentNota = 0;
		
		CalcularAreas();	
		verificarXNotasPorVez = notasNaPista.Count;	
	}

	void CalcularAreas ()
	{
		float posXRoot = UICamera.mainCamera.WorldToScreenPoint( root.transform.position ).x ;
		
		areaDePontuacao.width = tamanhoAreaDePontuacao;
		areaDePontuacao.height = Screen.height;
		
		areaDeDead.width = tamanhoAreaDePontuacao;
		areaDeDead.height = Screen.height;
		
		areaDePontuacao.position 	= new Vector2( posXRoot - tamanhoAreaDePontuacao, 0 );
		areaDeDead.position 		= new Vector2( posXRoot - tamanhoAreaDePontuacao * 2, 0 );
		
	}

	void OnGUI()
	{
		if( dbg ) 
		{
			CalcularAreas();
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
			if( c > notasNaPista.Count || i >= notasNaPista.Count )
			{
				return;
			}
			
			Vector3 posNota = UICamera.mainCamera.WorldToScreenPoint( notasNaPista[i].transform.position );
			
			
			VerificarPercursoDePontuacao(posNota, notasNaPista[i]);
			VerificarFimDePercurso(posNota, notasNaPista[i]);
		}
	}

	void VerificarFimDePercurso (Vector3 posNota, Nota nota)
	{
	
		if( nota.VerificarZonaDeMorte( areaDeDead, posNota ) )
		{
			if( nota.mInfo.tipo != TipoDeNota.PAUSA )
			{
				gAudio.s.PararAudio();						
				gPontuacao.s.CancelarPontos();
			}
				DestruirNota(nota);
		}
	}

	void VerificarPercursoDePontuacao (Vector3 posNota, Nota nota)
	{
		if( nota.mInfo.tipo == TipoDeNota.PAUSA ) return;
		
		if( gPontuacao.s.VerificarPontuacao( nota, gGame.s.player ) == false )
		{
			if( (int)nota.mInfo.tipo >= (int)TipoDeNota.NOTA && gPontuacao.s.pontuandoNotaLonga )
			{
//				DestruirNota(nota);
			} 
			
			return;
		} 
		
		
		
		if( nota.VerificarZonaDePontuacao( areaDePontuacao, posNota ) && !nota.jaPontuei && !nota.kill  )
		{
			if( nota.mInfo.tipo == TipoDeNota.NOTA)
			{				
				gPontuacao.s.PontuarNotaComum( nota, gGame.s.player ) ;
				nota.jaPontuei = true;			
				nota.kill = true;
				gAudio.s.RecuperarAudio();
				DestruirNota(nota);
			}
			else
			{
				if( gPontuacao.s.pontuandoNotaLonga == false )
				{
					gPontuacao.s.PontuarNotaLonga( nota, gGame.s.player );				
					gAudio.s.RecuperarAudio();
				}
			}
		}
		else
		{
			if( nota.pontuando )
			{	
				nota.pontuando = false;
				gPontuacao.s.pontuandoNotaLonga = false;
				nota.kill = true;
				DestruirNota(nota);
			}
			
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
