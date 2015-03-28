using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MusicaFestaInfo
{
	public string clipBase;
	[HideInInspector]
	public AudioSource sourceBase;
	[HideInInspector]
	public GameObject parentBase;
	
	public List<InstrumentoFestaInfo> instrumentos;
	
	public MusicaFestaInfo()
	{
		instrumentos = new List<InstrumentoFestaInfo>();
	}
}
[System.Serializable]
public class InstrumentoFestaInfo
{
	public QualPersonagem 	personagem;
	public string			clipInstrumento;
	
	[HideInInspector]
	public AudioSource 		mSource;
	[HideInInspector]
	public AudioClip 		mClip;
	[HideInInspector]
	public GameObject		goParent;
	
	public InstrumentoFestaInfo( QualPersonagem _personagem, string _clipInstrumento )
	{
		personagem = _personagem;
		clipInstrumento = _clipInstrumento;
	}
	
}

public class MenuFesta : Menu 
{

	public bool tocandoMusica = false;
	
	public Musica musica;
	public MusicaFestaInfo musicaFesta;
	
	public vPersonagensDancando vPersonagens;
	
	public int samplesBase;
	
	public bool ativo = true;
	
	public GameObject btnPlay;
	public GameObject goPartituras;
	
	public override void Show ()
	{
		base.Show ();
//		goPartituras.SetActive(false);
		musicaFesta = gLevels.s.allLevels[gLevels.s.currentLevelIndex].instrumentosDaFesta;
		
		musicaFesta.parentBase = new GameObject(musicaFesta.clipBase);
		musicaFesta.parentBase.transform.parent = transform;
			
		AudioSource ASB = musicaFesta.parentBase.AddComponent<AudioSource>();
		AudioClip	ACB = Vivaldos.NameToAudioClip( musicaFesta.clipBase );
		ASB.clip = ACB;
		
		musicaFesta.sourceBase = ASB;
		
		foreach( InstrumentoFestaInfo inst in musicaFesta.instrumentos )
		{
			inst.goParent = new GameObject(inst.clipInstrumento);
			inst.goParent.transform.parent = vPersonagens.GetInstrumento( inst.personagem ).transform;
			AudioSource AS = inst.goParent.AddComponent<AudioSource>();
			AudioClip	AC = Vivaldos.NameToAudioClip( inst.clipInstrumento );
			
			AS.clip = AC;
			
			inst.mSource = AS;
			inst.mClip = AC;
		}
		
		Play();
		
	}
	
	public void OnClickPersonagem( GameObject go, bool isChecked )
	{
		if( isChecked == false )
		{
			 go.GetComponent<UI2DSpriteAnimation>().Play("disabled");
			 go.GetComponent<UIButton>().isEnabled = false;
			 
			 Debug.Log("Desligamdo as coisas aqui");
			 musicaFesta.instrumentos.Find( e => e.personagem == vPersonagens.GetPersonagem( go ) ).mSource.Stop();
		}
		else
		{
			go.GetComponent<UI2DSpriteAnimation>().Play("idle");
			go.GetComponent<UIButton>().isEnabled = true;
			
			Debug.Log("Ligando as coisas aqui");
			AudioSource current = musicaFesta.instrumentos.Find( e => e.personagem == vPersonagens.GetPersonagem( go ) ).mSource;
			current.time = musicaFesta.sourceBase.time;
			current.Play();
		}
		
		
		
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
		
		musicaFesta.sourceBase.Stop();	
		musicaFesta.sourceBase.Play();	
		
		foreach( InstrumentoFestaInfo clip in musicaFesta.instrumentos)
		{
			clip.mSource.Stop();
			clip.mSource.Play();
		}
		
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
