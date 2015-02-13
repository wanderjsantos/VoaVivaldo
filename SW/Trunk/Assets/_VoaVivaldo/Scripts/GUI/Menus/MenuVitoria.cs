using UnityEngine;
using System.Collections;

public class MenuVitoria : Menu {

	public void OnClickContinuar()
	{
		gMenus.s.ShowMenu("Principal");
	}
	
	public void OnClickReiniciar()
	{
		gGame.s.IniciarJogo();
	}
}
