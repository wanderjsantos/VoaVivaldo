using UnityEngine;
using System.Collections;

public class MenuLevelSelect : Menu {

	public int levelToSelect = 1;

	public void SelectFase( int numero )
	{
		Debug.Log ("Select Fase: " + numero);
		gMusica.s.SetMusica (numero);
		gMenus.s.ShowMenu ("Instrumento");
	}
}
