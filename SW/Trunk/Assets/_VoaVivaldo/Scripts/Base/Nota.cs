using UnityEngine;
using System.Collections;

public class Nota : MonoBehaviour
{
	public NotaInfo 	mInfo;
	public NotaView		mView;

	public Color 		cor;

	public void Start()
	{
		mView.UpdateColor (cor);
		
	}

	public void OnHitAreaDePontuacao()
	{

	}

	public void OnHitDeadArea()
	{

	}

}


public enum Timbre{ ZERO, UM, DOIS, TRES, QUATRO, CINCO }
[System.Serializable]
public class NotaInfo 
{
	public Timbre	timbre;
//	[HideInInspector]
//	public float	noTempo;
//	[HideInInspector]
//	public float	duracao;
	public int		compasso;
	public int 		batida;

}

[System.Serializable]
public class NotaView
{
	public UISprite mSprite;

	public void UpdateColor (Color cor)
	{
		mSprite.color = cor;
		mSprite.depth = 100;
	}
}


