using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gMusica : MonoBehaviour 
{
	public static gMusica s;

	public string audioBase = "Base1";
	public string audioInstrumento = "Instrumento1";
	public Musica musicaAtual;

	public Musica _prefabMusica;

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

	void PlayMusica ()
	{
		if (musicaAtual == null)
						NovaMusica (audioBase, audioInstrumento);
		musicaAtual.Play ();
	}

	void StopMusica ()
	{
		if (musicaAtual != null)
			musicaAtual.Stop ();
	}

	public void NovaMusica (string audioBase, string audioInstrumento, List<NotaInfo>notas = null, bool autoPlay = false)
	{
		if (musicaAtual != null) 
		{
			StopMusica ();
			DestroyMusica( musicaAtual );
		}

		MusicaInfo info 				= new MusicaInfo ();
		info.instrumentos.baseMusica 	= Resources.Load (audioBase) 		as AudioClip;
		info.instrumentos.instrumento	= Resources.Load (audioInstrumento) as AudioClip;

		if (notas != null)
						info.notas.AddRange (notas);

		Musica m = Instantiate (_prefabMusica) as Musica;
		m.mInfo = info;

		musicaAtual = m;
		if (autoPlay)	PlayMusica ();

	}

	public void CarregarMusica (string carregarArquivo)
	{
		MusicaData m = gSave.s.Load ();
		if (m == null)
						Debug.LogError ("Erro ao carregar a musica");
		NovaMusica (m.audioBase, m.audioInstrumento, m.notas);
	}

	public List<NotaInfo> TodasAsNotasNoCompasso( int compasso )
	{
		return musicaAtual.mInfo.notas.FindAll (e => e.compasso == compasso);
	}

	void DestroyMusica (Musica m)
	{
		Destroy (m);
	}
}
