using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gMusica : MonoBehaviour 
{
	public static gMusica s;

	public PartituraInfo	partituraAtual;
	
	public Musica musicaAtual;

	public bool verifyEnding = false;

	public Musica _prefabMusica;
	
	public float tempoEmPause;
	

	public int	indiceMusica = -1;
	public int	indiceFase = -1;

	void Awake()
	{
		s = this;
	}


	void OnEnable()
	{
		gComandosDeMusica.onPlay += PlayMusica;
		gComandosDeMusica.onStop += StopMusica;
		gGame.onReset += Resetar;
		gGame.onPauseGame += OnPause;
	}

	void OnDisable()
	{
		gComandosDeMusica.onPlay -= PlayMusica;
		gComandosDeMusica.onStop -= StopMusica;
		gGame.onReset -= Resetar;
		gGame.onPauseGame -= OnPause;
	}
	
	
	public void OnPause( bool estado )
	{
		if( musicaAtual == null ) return;
		
		if( estado )
		{
			musicaAtual.sourceBase.Pause();
			musicaAtual.sourceInstrumento.Pause();
			musicaAtual.sourcesExtras.ForEach( delegate( AudioSource source )
			{
				source.Pause();
			});
		}
		else
		{
			musicaAtual.sourceBase.Play();
			musicaAtual.sourceInstrumento.Play();
			musicaAtual.sourcesExtras.ForEach( delegate( AudioSource source )
          	{
				source.Play();
			});
		}
	}

	public void Resetar ()
	{
//		Set();
		if( musicaAtual != null ) 
			Destroy(musicaAtual.gameObject);
		partituraAtual = null;
		tempoEmPause = 0f;
	}

	public void SetMusica (int numero)
	{
		indiceMusica = numero;
	}

	public void SetFase (int numero = -1)
	{
		indiceFase = numero;		
	}

	public void Set( int musica = -1, int instrumento = -1 )
	{
		SetMusica (musica);
		SetFase  (instrumento);
	}

	void PlayMusica ()
	{
		if (musicaAtual == null)return;
//						NovaMusica (audioBase, audioInstrumento);
		musicaAtual.Play ();
		verifyEnding = true;
	}

	void StopMusica ()
	{
		if (musicaAtual != null)
			musicaAtual.Stop ();
		verifyEnding = false;
	}

	public void NovaMusica()
	{
		partituraAtual = GetPartitura (indiceMusica);

		NovaMusica (partituraAtual);

	}

	PartituraInfo GetPartitura (int indice)
	{
		return gLevels.s.currentPartitura.info;
	}

	public void NovaMusica (PartituraInfo dados, bool autoPlay = false)
	{
		if (musicaAtual != null) 
		{
			StopMusica();
			DestroyMusica( musicaAtual );
		}
		
		Debug.Log("Dados> Compasso: " + dados.compassos.Count );
		Debug.Log("Dados> Base: " + dados.nomeAudioBase );
		Debug.Log("Dados> Instrumento: " + dados.nomeAudioInstrumento );

		MusicaInfo info 				= new MusicaInfo ();
		info.mPartitura = dados;

		info.mBanda.musicaBase		 	= Vivaldos.NameToAudioClip (dados.nomeAudioBase);
		info.mBanda.instrumentoAtual	= Vivaldos.NameToAudioClip (dados.nomeAudioInstrumento);

		Musica m = Instantiate (_prefabMusica) as Musica;
		m.mInfo = info;
						
		gRitmo.s.SetBPM (info.mPartitura.BPM);
		
		List<string> outrosInstrumentos = gLevels.s.GetInstrumentosAnteriores( );
		foreach( string s in outrosInstrumentos )
		{
			AudioClip clip = Vivaldos.NameToAudioClip( s ) ;
			m.AdicionarInstrumentoExtra( clip );
		}
		
		
		
		musicaAtual = m;
		if (autoPlay)	PlayMusica ();

	}

	void DestroyMusica (Musica m)
	{
		Destroy (m);
	}

	void Update()
	{
		if (verifyEnding && musicaAtual!=null) 
		{
			if( musicaAtual.isPlaying == false )
			{
				gComandosDeMusica.s.Stop();
			}

		}
	}
}
