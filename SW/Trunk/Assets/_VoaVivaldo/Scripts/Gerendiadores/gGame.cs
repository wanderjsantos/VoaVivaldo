using UnityEngine;
using System.Collections;

public class gGame : MonoBehaviour 
{
	public delegate void ChangeGameStatusHandler ();
	public delegate void ChangePauseStatusHandler (bool pausado);

	public static event ChangeGameStatusHandler 	onInit;
	public static event ChangeGameStatusHandler 	onPlayGame;
	public static event ChangeGameStatusHandler 	onStopGame;
	public static event ChangePauseStatusHandler 	onPauseGame;
	public static event ChangeGameStatusHandler 	onReset;

	public static gGame s;
	public bool		gameStarted = false;
	public Player 	player;
	public Player	playerPrefab;
	public Transform spawnPoint;

	public	int		tempoParaIniciar = 0;
	public 	float 	contagemRegressiva = 5f;
	float			iTime = 0f;
	bool			contando = false;

	void Awake()
	{
		s = this;
	}
	public void Start()
	{
		IniciarInterfaces ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	public void OnEnable()
	{
		gComandosDeMusica.onStop += FimDePartida;
	}
	
	public void OnDisable()
	{
		gComandosDeMusica.onStop -= FimDePartida;
	}

	public void IniciarInterfaces()
	{
		gMenus.s.ShowMenu ("MenuPrincipal");
	}
	
	public void NewPlayer(  ) 
	{
//		if( player != null ) return;
		
		player = Instantiate( playerPrefab ) as Player;
		
		player.transform.parent = spawnPoint;
		player.transform.localScale = Vector3.one;
		player.transform.localPosition = Vector3.zero;
		
		player.mInfo = new PlayerInfo();
		
		if( player.vPlayer != null ) Destroy( player.vPlayer.gameObject );
		
		player.vPlayer = gPersonagens.s.GetPersonagem( player.mInfo.meuPersonagem );
		player.vPlayer.gameObject.SetActive(true);
		player.vPlayer.transform.localPosition = Vector3.zero;
		player.vPlayer.transform.localScale = Vector3.one;
		
	}
	
	public void NewPlayer( QualPersonagem personagem  ) 
	{
		//		if( player != null ) return;
		
		player = Instantiate( playerPrefab ) as Player;
		
		player.transform.parent = spawnPoint;
		player.transform.localScale = Vector3.one;
		player.transform.localPosition = Vector3.zero;
		
		player.mInfo = new PlayerInfo();
		player.mInfo.meuPersonagem = personagem;
		
		if( player.vPlayer != null ) Destroy( player.vPlayer.gameObject );
		
		player.vPlayer = gPersonagens.s.GetPersonagem( player.mInfo.meuPersonagem );
		player.vPlayer.transform.parent = player.transform;
		player.vPlayer.transform.localPosition = Vector3.zero;
		player.vPlayer.transform.localScale = Vector3.one;
		
		
	}

	public void IniciarJogo()
	{
		if( player != null ) Destroy(player.gameObject);
		if( onReset != null ) onReset();
		
		NewPlayer( gLevels.s.currentPartitura.info.personagem );
		
		gMusica.s.NovaMusica ();
		
		tempoParaIniciar = 0;
		iTime = Time.realtimeSinceStartup;
		contando = true;

		player.Disable ();

		gMenus.s.ShowMenu ("Gameplay");

		if (onInit != null)
						onInit ();
	}
	
	public bool pausado = false;
	
	public void Pause( bool pausar )
	{
		if( gameStarted == false ) return ;
		
		pausado = pausar;
		Time.timeScale = (pausado)? 0f : 1f ;
	
		if( onPauseGame != null ) onPauseGame( pausar );
	}

	public void PlayGame()
	{
		gameStarted = true;
		gComandosDeMusica.s.Play ();

		player.Enable ();

		if (onPlayGame != null)
						onPlayGame ();
	}

	void FimDePartida ()
	{
		if( gameStarted == false ) return;
		
		gMenus.s.ShowMenu("Vitoria");
		
		gameStarted = false;
		
		if( onStopGame != null)
			onStopGame();
	}
	
	void FimDeJogo()
	{
		
	}

	void Update()
	{
		if (contando)
		{
			if( Time.realtimeSinceStartup < (iTime + contagemRegressiva ) )
			{
				tempoParaIniciar = (int) ((iTime + contagemRegressiva) - Time.realtimeSinceStartup);
			}
			else
			{
				contando = false;
				PlayGame();
			}
		}
	}


}
