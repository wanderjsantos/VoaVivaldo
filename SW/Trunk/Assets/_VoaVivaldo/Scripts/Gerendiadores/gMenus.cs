using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gMenus : MonoBehaviour 
{	
	public UIRoot	uiRoot;

	public static gMenus s;
	public Menu		menuAtual;
	public Menu		menuPause;
	public List<Menu>		todosOsMenus;
	void Awake()
	{
		s = this;
		todosOsMenus = new List<Menu> ();
		todosOsMenus.AddRange (transform.GetComponentsInChildren<Menu>(true));
		menuPause = Get("Pause");
	}
	
	void OnEnable()
	{
		gGame.onPauseGame += OnPause;
	}
	
	void Disable()
	{
		gGame.onPauseGame -= OnPause;
	}
	
	public void OnPause( bool estado )
	{
		if( estado )
			menuPause.Show( );
		else
			menuPause.Hide();
	}

	public void ShowMenu( Menu novoMenu )
	{
		if (menuAtual != null)
						menuAtual.Hide ();
		
		novoMenu.Show ();
		
		gTemas.s.AplicarAtual();
		
		menuAtual = novoMenu;
	}

	public void ShowMenu( string novoMenu )
	{
		ShowMenu (Get (novoMenu));
	}

	public Menu Get(string menu)
	{
		return todosOsMenus.Find (e => e.name.Contains (menu));
	}

}