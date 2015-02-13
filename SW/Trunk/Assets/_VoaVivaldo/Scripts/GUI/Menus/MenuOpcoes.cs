using UnityEngine;
using System.Collections;

public class MenuOpcoes : Menu {

	public void OnClickHome()
	{
		gMenus.s.ShowMenu("Principal");
	}
	
	public void OnChangeValueVibrar( bool valor )
	{}
	
	public void OnChangeValueAudio( bool valor )
	{}
}
