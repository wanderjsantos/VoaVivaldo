using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gMenus : MonoBehaviour 
{
	public UIRoot	uiRoot;

	public static gMenus s;
	public Menu		menuAtual;
	public List<Menu>		todosOsMenus;
	void Awake()
	{
		s = this;
		todosOsMenus = new List<Menu> ();
		todosOsMenus.AddRange (transform.GetComponentsInChildren<Menu>(true));
	}

	public void ShowMenu( Menu novoMenu )
	{
		if (menuAtual != null)
						menuAtual.Hide ();

		novoMenu.Show ();
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