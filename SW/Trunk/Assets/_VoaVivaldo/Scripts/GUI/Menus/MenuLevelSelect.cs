using UnityEngine;
using System.Collections;

public class MenuLevelSelect : Menu {

	public int levelToSelect = 1;

	public void SelectFase( int numero )
	{
		gGame.s.IniciarJogo ();
	}
}
