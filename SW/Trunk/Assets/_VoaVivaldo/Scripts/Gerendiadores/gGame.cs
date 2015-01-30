using UnityEngine;
using System.Collections;

public class gGame : MonoBehaviour 
{
	public static gGame s;
	public bool		gameStarted = false;
	public Player player;

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
		gMenus.s.ShowMenu ("Gameplay");
		gRecord.s.recordOnPlayMusic = false;
		gMusica.s.CarregarMusica ("Musica.xml");
		gComandosDeMusica.s.Play ();
		gameStarted = true;

	}
}
