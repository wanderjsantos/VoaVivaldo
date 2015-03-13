using UnityEngine;
using System.Collections;

[RequireComponent( typeof( AudioSource ) )]
public class gAudio : MonoBehaviour 
{
	
	public float tempoDeEsperaDeUmaNota = 0;
	
	bool esperandoParaTocar = false;
	
	public AudioSource sourceInstrumentos;
	
	float lastVolume = .5f;
	
	float iTime;
	
	public AudioClip error;
	public AudioClip novaEstrela;
	
	public static gAudio s;
	void Awake()
	{
		s = this;
	}
	
	public void PlayErrorClip()
	{
		audio.clip = error;
		audio.PlayOneShot( audio.clip );
		
	}
	
	public void PlayNovaEstrelaClip()
	{
		audio.clip = novaEstrela;
		audio.PlayOneShot( audio.clip );
	}
	
	public void PararAudio()
	{	
		if( esperandoParaTocar == true ) return;
			
		tempoDeEsperaDeUmaNota = (60f/gRitmo.s.BPM); 
		sourceInstrumentos = gMusica.s.musicaAtual.sourceInstrumento;
		lastVolume = sourceInstrumentos.volume;
		sourceInstrumentos.volume = 0f;
		iTime = Time.realtimeSinceStartup;
		esperandoParaTocar = true;
		
		PlayErrorClip();
	}
	
	public void RecuperarAudio()
	{
		esperandoParaTocar = false;
		sourceInstrumentos.volume = lastVolume;
		
	}
	
	void Update()
	{
		if( Input.GetKeyUp( KeyCode.A ) ) PararAudio();
	
		if( esperandoParaTocar )
		{
			if( Time.realtimeSinceStartup - iTime >= tempoDeEsperaDeUmaNota )
			{
				RecuperarAudio();
			}
		}
	}
	
}
