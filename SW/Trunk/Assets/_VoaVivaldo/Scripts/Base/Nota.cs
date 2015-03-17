using UnityEngine;
using System.Collections;

public class Nota : MonoBehaviour
{
	public NotaInfo 	mInfo;
	public NotaView		mView;

	public Color 		cor;

	public bool			kill = false;
	public bool			pontuando = false;
	
	public float		sizeX;

	public void Start()
	{
		if( mView.mSprite == null ) mView.SetSprite( transform.GetComponentInChildren<UI2DSprite>() );
		mView.UpdateColor (cor);
		
		kill = false;
		
	}
	
	public void SetSize( float x )
	{
		sizeX = x;
		mView.SetSpriteSize( sizeX );
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


