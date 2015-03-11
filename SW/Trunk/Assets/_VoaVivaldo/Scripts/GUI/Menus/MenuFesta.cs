using UnityEngine;
using System.Collections;

public class MenuFesta : Menu 
{

	public bool tocandoMusica = false;
	
	public Musica musica;
	
	public int samplesBase;
	
	public bool ativo = true;
	
	public GameObject btnPlay;
	public GameObject goTrechos;
	
	public override void Show ()
	{
		base.Show ();
		goTrechos.SetActive(false);
	}
	
	public void OnClickMostrarTrecho()
	{
		goTrechos.SetActive(true);
	}
	
	public void OnClickFecharTrechos()
	{
		goTrechos.SetActive(false);
	}
	
	public void OnClickPlay()
	{		
		Play();
		
	}

	void Play ()
	{
		if( tocandoMusica ) Stop();
	
		if(gMusica.s.musicaAtual != null )
		{
			musica = gMusica.s.musicaAtual;
		}
		else
		{
			Debug.LogError("Nao ha uma musica para ser tocada");
		}
		
		musica.sourceBase.Stop();		
		musica.sourceBase.Play();
		
		musica.sourceInstrumento.Stop();
		musica.sourceInstrumento.Play();
		
		tocandoMusica = true;
		
		iTime = Time.realtimeSinceStartup;
		
		btnPlay.SetActive(false);
	}

	void Stop ()
	{
		musica.sourceBase.Stop();		
		musica.sourceInstrumento.Stop();		
		tocandoMusica = false;
		
		btnPlay.SetActive(true);
		
		OnClickFecharTrechos();
		
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
