using UnityEngine;
using System.Collections;

public class MenuSelecaoDeInstrumento : Menu 
{
	public bool 		iniciarGame = false;
	public UIButton		buttonIniciarGame;

	public override void Show ()
	{
		base.Show ();
		buttonIniciarGame.isEnabled = false;
	}

	public void SelectInstrumento(int numero)
	{
		gMusica.s.SetInstrumento (numero);
		buttonIniciarGame.isEnabled = true;
	}

	public void IniciarPartida()
	{
		gGame.s.IniciarJogo ();
	}
}
