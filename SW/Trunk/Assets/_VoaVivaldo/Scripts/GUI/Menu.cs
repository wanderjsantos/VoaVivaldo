using UnityEngine;
using System.Collections;

[RequireComponent( typeof(UIPanel))]
public class Menu : MonoBehaviour, iMenu
{
	private void ForceHide()
	{
		gameObject.SetActive (false);
	}

	public virtual void Show()
	{
		Resetar();
		gameObject.SetActive (true);
		
//		gTemas.s.AplicarAtual();
		
	}

	public virtual void Hide()
	{
		gameObject.SetActive (false);
	}

	public virtual void Resetar ()
	{
		
	}
}

public interface iMenu
{
	void Show();
	void Hide();
}
