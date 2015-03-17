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
	
	public bool trilhaBloqueada = false;
	public void PararAudio()
	{	
		PlayErrorClip();
	
		if( esperandoParaTocar == true || trilhaBloqueada ) return;
			
//		tempoDeEsperaDeUmaNota = (60f/gRitmo.s.BPM); 
		sourceInstrumentos = gMusica.s.musicaAtual.sourceInstrumento;
		lastVolume = sourceInstrumentos.volume;
		sourceInstrumentos.volume = 0f;
		
		trilhaBloqueada = true;
		
//		iTime = Time.realtimeSinceStartup;
//		esperandoParaTocar = true;
		
		
	}
	
	public void RecuperarAudio()
	{
		if( !trilhaBloqueada ) return ;
		
		trilhaBloqueada = false;
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
