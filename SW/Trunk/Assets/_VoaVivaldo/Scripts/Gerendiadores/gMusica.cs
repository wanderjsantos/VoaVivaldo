using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gMusica : MonoBehaviour 
{
	public static gMusica s;

	public MusicaData dadosDaMusicaAtual;

	public Musica musicaAtual;

	public bool verifyEnding = false;

	public Musica _prefabMusica;

	public int	musicaIndice = -1;
	public int	instrumentoIndice = -1;

	void Awake()
	{
		s = this;
	}


	void OnEnable()
	{
		gComandosDeMusica.onPlay += PlayMusica;
		gComandosDeMusica.onStop += StopMusica;
	}

	void OnDisable()
	{
		gComandosDeMusica.onPlay -= PlayMusica;
		gComandosDeMusica.onStop -= StopMusica;
	}

	public void SetMusica (int numero = -1)
	{
		if( numero != -1 )
			musicaIndice = numero;
	}

	public void SetInstrumento (int numero = -1)
	{
		if( numero != -1 )
			instrumentoIndice = numero;
	}

	public void Set( int musica = -1, int instrumento = -1 )
	{
		SetMusica (musica);
		SetInstrumento (instrumento);
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
		dadosDaMusicaAtual = GetMusica (musicaIndice);

		NovaMusica (dadosDaMusicaAtual.audioBase, dadosDaMusicaAtual.audioInstrumentos [instrumentoIndice], dadosDaMusicaAtual.notas, dadosDaMusicaAtual.BPM);

	}

	public void NovaMusica (string audioBase, string audioInstrumento, List<NotaInfo>notas, int beatsPerMinute = 120, bool autoPlay = false)
	{
		if (musicaAtual != null) 
		{
			StopMusica();
			DestroyMusica( musicaAtual );
		}

		MusicaInfo info 				= new MusicaInfo ();
		info.instrumentos.baseMusica 	= Resources.Load (audioBase) 		as AudioClip;
		info.instrumentos.instrumento	= Resources.Load (audioInstrumento) as AudioClip;

		if (notas != null)
						info.notas.AddRange (notas);


		Musica m = Instantiate (_prefabMusica) as Musica;
		m.mInfo = info;

		gRitmo.s.SetBPM (beatsPerMinute);

		musicaAtual = m;
		if (autoPlay)	PlayMusica ();

	}

	public MusicaData GetMusica( int indice = 0 )
	{
		MusicaData ret = gLevels.s.GetLevel (indice, false).mInfo.dadosDaMusica;
		return ret;
	}

//	public void CarregarMusica (int indice = 0)
//	{
////		MusicaData m = gLevels.s.SetLevel(indice).mInfo.dadosDaMusica;
////
////		if (m == null)
////						Debug.LogError ("Erro ao carregar a musica");
////		NovaMusica (m.audioBase, m.audioInstrumentos[instrumentoIndice], m.notas , m.BPM, false);
//	}

	public List<NotaInfo> TodasAsNotasNoCompasso( int compasso )
	{
		return musicaAtual.mInfo.notas.FindAll (e => e.compasso == compasso);
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
				gComandosDeMusica.s.Stop();

		}
	}
}
