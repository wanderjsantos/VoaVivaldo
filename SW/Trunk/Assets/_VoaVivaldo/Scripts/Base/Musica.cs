using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Musica : MonoBehaviour 
{

	public MusicaInfo 		mInfo;
	AudioSource 		sourceBase;
	AudioSource 		sourceInstrumento;
	public bool 		isPlaying 	= false;
	int 				posicaoNotaAtual = 0;

	public void Start()
	{
		VerifyAudioSources ();
	}

	public void Play()
	{
		VerifyAudioSources ();

		sourceBase.clip = mInfo.instrumentos.baseMusica;
		sourceInstrumento.clip = mInfo.instrumentos.instrumento;
		
		sourceBase.Play ();
		sourceInstrumento.Play ();
		
		isPlaying = true;

		mInfo.tempoDaMusica = 0f;
		posicaoNotaAtual = 0;
		iTime = Time.realtimeSinceStartup;
	}

	public void Stop()
	{
		sourceBase.Stop ();
		sourceInstrumento.Stop ();

		isPlaying = false;
	}

	void VerifyAudioSources ()
	{
		if (sourceBase == null)
			sourceBase = (new GameObject ("SourceBase")).AddComponent<AudioSource> ();
		if (sourceInstrumento == null)
			sourceInstrumento = (new GameObject ("SourceInstrumento")).AddComponent<AudioSource> ();

		sourceBase.transform.parent = gameObject.transform;
		sourceInstrumento.transform.parent = gameObject.transform;
	}

	float cTime;
	float iTime;

	public void Update()
	{
		if (isPlaying) 
		{
			cTime = Time.realtimeSinceStartup;
			mInfo.tempoDaMusica = cTime - iTime;

//			UpdateNotas( mInfo.tempoDaMusica );

			if( mInfo.tempoDaMusica >= sourceBase.clip.length ) Stop();
			
		}
	}

	bool UpdateNotas (float tempoDaMusica)
	{
		if (posicaoNotaAtual >= mInfo.notas.Count) 
		{
			Debug.Log("FIM DAS NOTAS");
			return false;
		}

//		if (mInfo.notas [posicaoNotaAtual].noTempo <= tempoDaMusica)
//		{
//			Debug.Log("Criar nota: " + mInfo.notas[ posicaoNotaAtual ] .timbre );
//			posicaoNotaAtual++;
//		}

		return true;
	}
}

[System.Serializable]
public class MusicaInfo
{
	public MusicaInfo()
	{
		instrumentos = new InstrumentosInfo ();
		notas = new List<NotaInfo> ();
		tempoDaMusica = 0f;
	}

	public InstrumentosInfo instrumentos;
	public List<NotaInfo> 	notas;
	[HideInInspector]
	public float tempoDaMusica;
}
