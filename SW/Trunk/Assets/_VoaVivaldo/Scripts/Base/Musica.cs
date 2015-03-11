﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Musica : MonoBehaviour 
{

	public MusicaInfo 		mInfo;
	public AudioSource 		sourceBase;
	public AudioSource 		sourceInstrumento;
	public int			instrumentoAtual;
	public bool 		isPlaying 	= false;
	int 				posicaoNotaAtual = 0;

	public void Start()
	{
		VerifyAudioSources ();
	}

	public void Play()
	{
		VerifyAudioSources ();

		sourceBase.clip = mInfo.mBanda.musicaBase;
		sourceInstrumento.clip = mInfo.mBanda.instrumentoAtual;
		
		sourceBase.Play ();
		sourceInstrumento.Play ();
		
		isPlaying = true;

		mInfo.tempoDaMusica = 0f;
		posicaoNotaAtual = 0;
		iTime = Time.time;
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
			cTime = Time.time;
			mInfo.tempoDaMusica = cTime - iTime;
			if( mInfo.tempoDaMusica >= sourceBase.clip.length ) Stop();
			
		}
	}

	bool UpdateNotas (float tempoDaMusica)
	{
//		if (posicaoNotaAtual >= mInfo.mData.instrumentos[mInfo.instrumentoAtual].notas.Count) 
//		{
//			Debug.Log("FIM DAS NOTAS");
//			return false;
//		}

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
	public MusicaData mData;
	public int instrumentoAtual = -1;
	
	public Banda	mBanda;
	
	public MusicaInfo()
	{
		tempoDaMusica = 0f;
		mBanda = new Banda();
	}
	public float tempoDaMusica;
}
