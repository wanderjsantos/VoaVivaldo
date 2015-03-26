using UnityEngine;
using System.Collections;

public class MenuFesta : Menu 
{

	public bool tocandoMusica = false;
	
	public Musica musica;
	
	public int samplesBase;
	
	public bool ativo = true;
	
	public GameObject btnPlay;
	public GameObject goPartituras;
	
	public override void Show ()
	{
		base.Show ();
//		goPartituras.SetActive(false);
//		
//		Play();
	}
	
	public void OnClickMostrarPartitura()
	{
		goPartituras.SetActive(true);
	}
	
	public void OnClickFecharPartituras()
	{
		goPartituras.SetActive(false);
	}
	
	public void OnClickPlay()
	{		
		Play();
		
	}

	void Play ()
	{
		if( tocandoMusica ) Stop();
	
//		if(gMusica.s.musicaAtual != null )
//		{
			musica = gMusica.s.musicaAtual;
//		}
		if( musica == null )
		{
			Debug.LogError("Nao ha uma musica para ser tocada");
			return;
		}


		
		musica.sourceBase.Stop();		
		musica.sourceBase.Play();
		
		musica.sourceInstrumento.Stop();
		musica.sourceInstrumento.Play();
		
		tocandoMusica = true;
		
		iTime = Time.realtimeSinceStartup;
		
		btnPlay.SetActive(false);
		
		Play();
	}

	void Stop ()
	{
		if( musica )
		{
			musica.sourceBase.Stop();		
			musica.sourceInstrumento.Stop();		
		}
		
		tocandoMusica = false;
		
		btnPlay.SetActive(true);
		
		OnClickFecharPartituras();
		
	}

	public void OnClickButton( bool active,   string nomeDoInstrumento, UIButton button )
	{
		if( musica == null  ) return;
	
						
		if( active )
		{
//			Debug.Log("ON");
		
			button.normalSprite = "FESTA_" + nomeDoInstrumento + "_ON";
			
			samplesBase = musica.sourceBase.timeSamples;
			
			musica.sourceInstrumento.timeSamples = samplesBase;
			musica.sourceInstrumento.Play();
		}
		else
		{
//			Debug.Log("OFF");			
			
			button.normalSprite = "FESTA_" + nomeDoInstrumento + "_OFF";
			
			musica.sourceInstrumento.Pause();
		}
		
		
	}
	
	public void OnClickHome()
	{
		Stop();
	
		gMenus.s.ShowMenu("Principal");
	}
	
	float iTime;
	
	public void Update()
	{
		if( tocandoMusica == false ) return;
		
		if( Time.realtimeSinceStartup > ( iTime + musica.sourceBase.clip.length ) )
		{
			Stop ();
		}
	}
}
