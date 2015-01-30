using UnityEngine;
using System.Collections;

[RequireComponent( typeof(UIPanel))]
public class Menu : MonoBehaviour, iMenu
{
//	public void Awake()
//	{
//		ForceHide ();
//	}

	private void ForceHide()
	{
		gameObject.SetActive (false);
	}

	public virtual void Show()
	{
		gameObject.SetActive (true);
	}

	public virtual void Hide()
	{
		gameObject.SetActive (false);
	}

}

public interface iMenu
{
	void Show();
	void Hide();
}
