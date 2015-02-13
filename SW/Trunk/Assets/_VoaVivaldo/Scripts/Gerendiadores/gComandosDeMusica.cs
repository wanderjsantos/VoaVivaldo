using UnityEngine;
using System.Collections;

public enum PlayMode{ GRAVAR, NORMAL, EDITAR, TOCAR }

public delegate void OnMusicaChangedHandler();
public delegate void OnChangedPlayModeHandler( PlayMode newMode  );
public delegate void OnBeepEventHandler( int newCount);

public class gComandosDeMusica : MonoBehaviour
{
	public static event OnMusicaChangedHandler onPlay;
	public static event OnMusicaChangedHandler onStop;
	public static event OnChangedPlayModeHandler onChangePlayMode;
	public static event OnBeepEventHandler		onBeep;

	void OnPlay()
	{
		if (onPlay != null)	onPlay ();
	}	
	void OnStop()
	{
		if (onStop != null)	onStop ();
	}
	void OnChangePlayMode( PlayMode novoModo)
	{
		if (onChangePlayMode != null) onChangePlayMode( novoModo );
	}

	public void OnNovoCompasso( int compassoAtual )
	{
		if (onBeep != null)
						onBeep (compassoAtual);
	}

	public static gComandosDeMusica s;

	PlayMode modoAtual = PlayMode.NORMAL;
	public PlayMode ModoAtual
	{
		get{return modoAtual;}
		set{ modoAtual = value; OnChangePlayMode (value);}
	}
	void Awake()
	{
		s = this;
	}

	void OnEnable()
	{
		onChangePlayMode += ChangeMode;
	}
	void OnDisable()
	{
		onChangePlayMode += ChangeMode;
	}

	void ChangeMode ( PlayMode newMode)
	{
		Stop ();
		switch (newMode) 
		{
		case PlayMode.TOCAR:
			Play();
			break;
//		case PlayMode.GRAVAR:
//			Record();
//			break;
		default:
			break;
		}
	}

	public void Play()
	{
		OnPlay ();		
	}

	public void Stop()
	{
		OnStop ();		
	}

//	void Record ()
//	{
//		Stop ();
//	}


}
