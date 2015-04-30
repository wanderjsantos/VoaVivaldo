using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class gFX : MonoBehaviour {

	public static gFX s;
	public AudioSource source;
	
	public List<FxSetInfo> allFXSet;
	public FxSetInfo currentFxSet;
	
	public void Awake()
	{
		s = this;
		source = GetComponent<AudioSource>();
		source.playOnAwake = false;
	}
	
	public void Start()
	{
		Set (0);
	}
	
	public void Set( QualPersonagem quallPersonagem )
	{
		switch( quallPersonagem )
		{
			case QualPersonagem.TRUMPET:
				Set (0);
				break;
			case QualPersonagem.HORNET:
				Set (1);
				break;
			case QualPersonagem.FLAUTA:
				Set (2);
				break;
			case QualPersonagem.SANFONA:
				Set (3);
				break;
			default :
				Set(0);
				break;
		}
	}
	
	public void Set( int index )
	{
		index = Mathf.Clamp( index, 0, allFXSet.Count-1 );
		
		currentFxSet = allFXSet[index];
	}
	
	public void Play( FX tipo )
	{
		switch( tipo )
		{
			case FX.AVANCAR:
				Play( currentFxSet.avancar );
				break;		
			case FX.VOLTAR:
				Play( currentFxSet.voltar );
				break;
			case FX.ERRO:
				Play( currentFxSet.erro );
				break;
			case FX.VITORIA:
				Play( currentFxSet.vitoria );
				break;
			case FX.DERROTA:
				Play( currentFxSet.derrotas );
				break;
			default :
				Play(currentFxSet.avancar);
				break;
		}
		
		
	}
	
	void Play( AudioClip clip )
	{
		source.volume = gSave.s.GetCurrentBaseVolume();
	
		source.clip = clip;
		source.PlayOneShot(clip);
	}
	
}

public enum FX{ AVANCAR, VOLTAR, ERRO, VITORIA,DERROTA }

[System.Serializable]
public class FxSetInfo
{
	public AudioClip avancar;
	public AudioClip voltar;
	public AudioClip erro;
	public AudioClip vitoria;
	public AudioClip derrotas;
	
}
