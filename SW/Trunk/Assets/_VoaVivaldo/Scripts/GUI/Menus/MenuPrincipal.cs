using UnityEngine;
using System.Collections;

public class MenuPrincipal : Menu 
{
	public override void Show ()
	{
		base.Show ();
	}

	public override void Hide()
	{
		base.Hide ();
	}

	public void OnClickJogar()
	{
//		gGame.s.IniciarJogo ();
		gMenus.s.ShowMenu ("Select");
	}
}
