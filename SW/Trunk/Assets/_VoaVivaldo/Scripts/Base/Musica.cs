using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Musica : MonoBehaviour 
{

	public MusicaInfo 		mInfo;
	public AudioSource 		sourceBase;
	public AudioSource 		sourceInstrumento;
	public int			instrumentoAtual;
	public bool 		isPlaying 	= false;
//	int 				posicaoNotaAtual = 0;

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
//		posicaoNotaAtual = 0;
		iTime = Time.time;
	}
	
	void OnEnable()
	{
		Vivaldos.onChangeAudioSettings += UpdateAudioSettings;
	}
	
	void OnDisable()
	{
		Vivaldos.onChangeAudioSettings -= UpdateAudioSettings;
	}
		
	public void UpdateAudioSettings( float volumeBase, float volumeInstr, float volumeGeral )
	{
		sourceBase.volume = volumeBase;
		sourceInstrumento.volume = volumeInstr;
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

		sourceBase.volume = gSave.s.GetCurrentBaseVolume();
		sourceInstrumento.volume = gSave.s.GetCurrentInstrumentosVolume();

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

}

[System.Serializable]
public class MusicaInfo
{
	public PartituraInfo mPartitura;
//	public int instrumentoAtual = -1;	
	public Banda	mBanda;
	public float tempoDaMusica;
	
	public MusicaInfo()
	{
		tempoDaMusica = 0f;
		mBanda = new Banda();
	}
	
}
