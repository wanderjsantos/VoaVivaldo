using UnityEngine;
using System.Collections;

public class gAudio : MonoBehaviour 
{
	
	public float tempoDeEsperaDeUmaNota = 0;
	
	bool esperandoParaTocar = false;
	
	public AudioSource sourceInstrumentos;
	
	float lastVolume = .5f;
	
	float iTime;
	
	public static gAudio s;
	void Awake()
	{
		s = this;
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
		
		audio.PlayOneShot( audio.clip );
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
