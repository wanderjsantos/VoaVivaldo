using UnityEngine;
using System.Collections;

public class MenuPause : Menu {

	public void OnClickReiniciar()
	{
		gGame.s.Pause( false);
		gGame.s.IniciarJogo();		
	}
	
	public void OnClickContinuar()
	{
		gGame.s.Pause(false);
	}
}
