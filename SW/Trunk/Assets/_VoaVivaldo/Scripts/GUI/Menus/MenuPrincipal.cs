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
	
	public override void Resetar ()
	{
		base.Resetar ();
	}

	public void OnClickJogar()
	{
//		gGame.s.IniciarJogo ();
		gMenus.s.ShowMenu ("SelecaoDeMusica");
	}
	
	public void OnClickCreditos()
	{
		gMenus.s.ShowMenu("Creditos");
	}
	
	public void OnClickOpcoes()
	{
		gMenus.s.ShowMenu("Opcoes");
	}
}
