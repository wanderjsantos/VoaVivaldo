using UnityEngine;
using System.Collections;

public class MenuCreditos : Menu {

	public void OnClickHome()
	{
		gMenus.s.ShowMenu("Principal");
	}
	
	public void OnClickCreditos()
	{
		Loader.Load("Final");
	}
}
