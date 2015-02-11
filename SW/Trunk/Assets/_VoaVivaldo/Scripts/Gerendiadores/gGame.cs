using UnityEngine;
using System.Collections;

public class gGame : MonoBehaviour 
{
	public delegate void ChangeGameStatusHandler ();

	public static event ChangeGameStatusHandler 	onInit;
	public static event ChangeGameStatusHandler 	onPlayGame;
	public static event ChangeGameStatusHandler 	onStopGame;
	public static event ChangeGameStatusHandler 	onReset;

	public static gGame s;
	public bool		gameStarted = false;
	public Player 	player;

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
	}

	public void IniciarInterfaces()
	{
		gMenus.s.ShowMenu ("MenuPrincipal");
	}

	public void IniciarJogo()
	{
		gMusica.s.NovaMusica ();
		gameStarted = true;



		tempoParaIniciar = 0;
		iTime = Time.realtimeSinceStartup;
		contando = true;

		player.Disable ();

		gMenus.s.ShowMenu ("Gameplay");

		if (onInit != null)
						onInit ();
	}

	public void PlayGame()
	{
		gComandosDeMusica.s.Play ();

		player.Enable ();

		if (onPlayGame != null)
						onPlayGame ();
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
