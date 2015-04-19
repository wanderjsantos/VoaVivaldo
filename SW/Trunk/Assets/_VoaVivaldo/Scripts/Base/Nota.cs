using UnityEngine;
using System.Collections;

public class Nota : MonoBehaviour
{
	public NotaInfo 	mInfo;
	public NotaView		mView;

	public Color 		cor;

	public bool			kill = false;
	public bool			pontuando = false;
	public bool			jaPontuei = false;
	
	public float		sizeX;

	public void Start()
	{
		if( mView.mSprite == null ) mView.SetSprite( transform.GetComponentInChildren<UI2DSprite>() );
		mView.UpdateColor (cor);
		
		kill = false;
		
	}
	
	public float GetSizeX(  )
	{
		
		sizeX = mView.mSprite.localSize.x;
//		mView.SetSpriteSize( sizeX );
		return sizeX;
	}

	public bool VerificarZonaDeMorte (Rect areaDeDead, Vector3 posNota)
	{
		Vector3 v = new Vector3(GetSizeX(), 0, 0 );
		if( areaDeDead.Contains( posNota + v ) && !kill )
		{
			kill = true;
			return true;
		}
		
		return false;
	}

	public bool VerificarZonaDePontuacao (Rect areaDePontuacao, Vector3 posNota)
	{
		if( mInfo.tipo == TipoDeNota.PAUSA ) return false;
		
		if( mInfo.tipo == TipoDeNota.NOTA )
		{
			Vector3 v = new Vector3(GetSizeX(), 0, 0 );
			if( areaDePontuacao.Contains( posNota + v ) && !kill )
			{
				return true;
			}
		}else
		{
			if( posNota.x <= ( areaDePontuacao.x + areaDePontuacao.width ) && (posNota.x + GetSizeX()) >= ( areaDePontuacao.x + areaDePontuacao.width ))
				return true;
		}
		
		return false;
	}
}


[System.Serializable]
public class NotaView
{
	public UI2DSprite mSprite;

	public void UpdateColor (Color cor)
	{
//		mSprite.color = cor;
	}
	
	public void SetSprite( UI2DSprite spt )
	{
		mSprite = spt;
	}
	
	public void SetSpriteSize(float size)
	{
		Vector2 s = mSprite.localSize;
		s.x = size;
		
		mSprite.SetDimensions((int)s.x, (int)s.y );
	}
}


