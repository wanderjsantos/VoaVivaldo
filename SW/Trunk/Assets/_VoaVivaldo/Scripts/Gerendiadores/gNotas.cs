using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gNotas : MonoBehaviour 
{
	public static gNotas s;

	public List<Nota> todasAsNotas;
	public Nota			notaLonga;
	public Nota			notaPausa;
	public List<Nota>notasNaPista;

	public bool drawAreaDePontuacao = false;
	public Rect areaDePontuacao;

	public bool drawAreaDeDead = false;
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
		quantidadeDeNotasNaPista = notasNaPista.Count;
		currentNota = 0;
	}

	void OnGUI()
	{
		if (drawAreaDePontuacao) 
		{
			GUI.Button( areaDePontuacao, "") ;
		}

		if (drawAreaDeDead) 
		{
			GUI.Button( areaDeDead, "") ;
		}

		if (gGame.s.gameStarted == false)
						return;

		int c = Mathf.Clamp (verificarXNotasPorVez, 1, notasNaPista.Count);
		
		for (int i = 0; i < c; i++) 
		{
			if( c > notasNaPista.Count )
			{
				return;
			}
			
			Vector3 posNota = UICamera.mainCamera.WorldToScreenPoint( notasNaPista[i].transform.position );
			
			if( notasNaPista[i].mInfo.tipo != TipoDeNota.PAUSA )
			{	
				
				if( areaDePontuacao.Contains( posNota ) && notasNaPista[i].kill == false) 
				{
					if( gPontuacao.s.VerificarPontuacao( notasNaPista[i], gGame.s.player ) )
					{
						notasNaPista[i].kill = true;
						
						if( Vivaldos.VIBRAR && SystemInfo.supportsVibration )
							Handheld.Vibrate();
						
						gPontuacao.s.Pontuar(gGame.s.player);
							
						DestruirNota(notasNaPista[i]);
					}
				}
			}

			if( areaDeDead.Contains( posNota ) && notasNaPista[i].kill == false) 
			{				
				notasNaPista[i].kill = true;
				
				if( notasNaPista[i].mInfo.tipo != TipoDeNota.PAUSA )
				{
					gAudio.s.PararAudio();						
					gPontuacao.s.CancelarPontos();
				}
						
				DestruirNota( notasNaPista[i] );				
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
		Nota n;
	
		switch( info.tipo )
		{
			case TipoDeNota.NOTA:
			   	n = 	NovaNotaNormal(info);
				break;
			case TipoDeNota.PAUSA:
				n = 	NovaNotaPausa(info);
				break;
			case TipoDeNota.NOTA_LONGA:
				n = 	NovaNotaLonga(info);
				break;
			default :
				n =	 NovaNotaNormal(info);
				break;
		}
		
		return n;
	
	}

	Nota NovaNotaPausa (NotaInfo info)
	{
		Nota ret = Instantiate (notaPausa) as Nota;
		ret.gameObject.transform.parent = gPista.s.rootPista.transform;
		ret.mInfo = info;

		notasNaPista.Add (ret);
		
		return ret;
		
	}

	Nota NovaNotaNormal (NotaInfo info)
	{
		Nota ret = Instantiate (todasAsNotas.Find (e => e.mInfo.timbre == info.timbre)) as Nota;
		ret.gameObject.transform.parent = gPista.s.rootPista.transform;
		ret.mInfo = info;

		notasNaPista.Add (ret);
		
		return ret;
	}

	Nota NovaNotaLonga (NotaInfo info)
	{
		Nota ret = Instantiate (notaLonga) as Nota;
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

		Destroy (nota.gameObject);
	}
}
