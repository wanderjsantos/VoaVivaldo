using UnityEngine;
using System.Collections;

public class MenuPause : Menu {

	public void OnClickReiniciar()
	{
		
	}
	
	public void OnClickContinuar()
	{
		gGame.s.Pause(false);
	}
}
