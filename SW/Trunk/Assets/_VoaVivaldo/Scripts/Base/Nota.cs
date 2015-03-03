using UnityEngine;
using System.Collections;

public class Nota : MonoBehaviour
{
	public NotaInfo 	mInfo;
	public NotaView		mView;

	public Color 		cor;

	public bool			kill = false;

	public void Start()
	{
		mView.UpdateColor (cor);
		kill = false;
	}

}


public enum Timbre { ZERO, UM, DOIS, TRES, QUATRO, CINCO,SEIS, SETE, OITO, NOVE, DEZ, ONZE, DOZE, TREZE, QUATORZE }

[System.Serializable]
public class NotaInfo 
{
	public Timbre	timbre;
	public _NotaEditor.Estado estadoEditor;
	public int		compasso;
	public int		duracao = 1;
	public int 		batida;

}

[System.Serializable]
public class NotaView
{
//	public UISprite mSprite;

	public void UpdateColor (Color cor)
	{
//		mSprite.color = cor;
	}
}


