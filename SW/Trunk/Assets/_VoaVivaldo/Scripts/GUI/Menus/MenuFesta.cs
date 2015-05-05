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
	
	public InstrumentoFestaInfo GetInstrumento(QualPersonagem personagem)
	{
		return instrumentos.Find( e => e.personagem == personagem );
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
//	[HideInInspector]
	public GameObject		goParent;
	
	public Sprite	partitura;
	
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
	
	public GameObject 	btnPause;
	public GameObject 	btnPlay;
	public UILabel		labelPlay;
	public GameObject 	goPartituras;
	
	public override void Show ()
	{
		base.Show ();
//		goPartituras.SetActive(false);
		
		Resetar();
		
		btnPause.SetActive(false);
		
		Debug.Log( "gLevels.s.currentLevelIndex :: " + gLevels.s.currentLevelIndex );

		musicaFesta = gLevels.s.allLevels[gLevels.s.currentLevelIndex].instrumentosDaFesta;
		
		musicaFesta.parentBase = new GameObject(musicaFesta.clipBase);
		musicaFesta.parentBase.transform.parent = transform;
			
		AudioSource ASB = musicaFesta.parentBase.AddComponent<AudioSource>();
		AudioClip	ACB = Vivaldos.NameToAudioClip( musicaFesta.clipBase );
		ASB.clip = ACB;
		ASB.volume = gSave.s.GetCurrentBaseVolume();
		musicaFesta.sourceBase = ASB;
		
		vPersonagens.DesativarTodos();
		
		foreach( InstrumentoFestaInfo inst in musicaFesta.instrumentos )
		{
			vPersonagens.Ativar(inst.personagem);
		
			inst.goParent = new GameObject(inst.clipInstrumento);
			inst.goParent.transform.parent = vPersonagens.GetInstrumento( inst.personagem ).transform;
			AudioSource AS = inst.goParent.AddComponent<AudioSource>();
			AudioClip	AC = Vivaldos.NameToAudioClip( inst.clipInstrumento );
			
			AS.volume = gSave.s.GetCurrentInstrumentosVolume();
			
			
			AS.clip = AC;
			
			inst.mSource = AS;
			inst.mClip = AC;
		}
		
//		Play();
		Stop();
		
	}
	
	public override void Resetar ()
	{
		base.Resetar ();
		
		if( musicaFesta == null ) return;
		
		Stop();	
		
		Destroy( musicaFesta.parentBase );
		foreach( InstrumentoFestaInfo i in musicaFesta.instrumentos )
		{
	
			Destroy(i.goParent);
		}
	}
	
	public override void Hide ()
	{
		base.Hide ();
		Destroy(  musicaFesta.parentBase);
		foreach( InstrumentoFestaInfo inst in musicaFesta.instrumentos )
		{
			Destroy( inst.goParent );			
		}
		
		musicaFesta = null;
		
		
	}
	
	public bool isPaused = false;
	public void Pause()
	{
		if(tocandoMusica == false) return;
	
		isPaused = true;
		btnPause.SetActive(false);
		labelPlay.text = "Play";
		
		musicaFesta.sourceBase.Pause();
		foreach( InstrumentoFestaInfo i in musicaFesta.instrumentos )
		{
			i.mSource.Pause();
		}
		
	}
	
	public void UnPause()
	{
		isPaused = false;
		
		btnPause.SetActive(true);
		labelPlay.text = "Replay";
		
		musicaFesta.sourceBase.Play();
		foreach( InstrumentoFestaInfo i in musicaFesta.instrumentos )
		{
			if( i.goParent.transform.parent.GetComponent<UIToggle>().value )
				i.mSource.Play();
		}
		
	}
	
	public void Replay()
	{
		Show();
		Play();	
	}
	
	public void OnClickPersonagem( GameObject go, bool isChecked )
	{
		if( tocandoMusica == false ) return;
	
		if( isChecked == false )
		{
			 go.GetComponent<UI2DSpriteAnimation>().Play("disabled");
//			 go.GetComponent<UIButton>().isEnabled = false;
			 
			 Debug.Log("Desligamdo as coisas aqui");
			 musicaFesta.instrumentos.Find( e => e.personagem == vPersonagens.GetPersonagem( go ) ).mSource.Stop();
		}
		else
		{
			go.GetComponent<UI2DSpriteAnimation>().Play("idle");
//			go.GetComponent<UIButton>().isEnabled = true;
			
			Debug.Log("Ligando as coisas aqui");
			if( isPaused == false )
			{
				AudioSource current = musicaFesta.instrumentos.Find( e => e.personagem == vPersonagens.GetPersonagem( go ) ).mSource;
				current.timeSamples = musicaFesta.sourceBase.timeSamples;
				current.Play();
			}
		}
		
		
		
	}
	
	public UI2DSprite poolSprites;
	public void OnClickMostrarPartitura( GameObject personagemGO )
	{
		QualPersonagem personagem = vPersonagens.GetPersonagem( personagemGO );
		
		Debug.Log("PErsonagem:" + personagem );
		
		poolSprites.sprite2D =  musicaFesta.GetInstrumento( personagem ).partitura;
	
		goPartituras.SetActive(true);
	}
	
	public void OnClickFecharPartituras()
	{
		goPartituras.SetActive(false);
	}
	
	public void OnClickPlay()
	{	
		if( tocandoMusica )
		{
			if( isPaused )
			{
				UnPause();
			}
			else
				Replay();
		}
		else
		{
			Stop();	
			Play();
		}
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
		
		btnPause.SetActive(true);
		labelPlay.text = "Replay";
	}

	void AtivarTodosOSPersonagens ()
	{
		vPersonagens.AtivarTodos(false);
	
		
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
		labelPlay.text = "Play";
		btnPause.SetActive(false);
		
		AtivarTodosOSPersonagens();
		
		OnClickFecharPartituras();
		
	}

	public void OnClickButton( bool active,   string nomeDoInstrumento, UIButton button )
	{
		if( musica == null  ) return;
	
	
		
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
		
		if( Time.realtimeSinceStartup > ( iTime + musicaFesta.sourceBase.clip.length ) )
		{
			Stop ();
		}
	}
}
