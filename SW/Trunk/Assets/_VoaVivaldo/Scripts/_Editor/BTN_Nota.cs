using UnityEngine;
using System.Collections;

public class _NotaEditor
{
	public bool			pressed = false;

	public Vector2 		mIndex = new Vector2 (0, 0);
	public Texture2D 	mTexture;
	public Texture2D 	defaultTexture;
	public int 			currentTimbre = -1;
	public int 			compasso = 1;
	public int 			batida = 1;
	
	public _NotaEditor( int x, int y, Texture2D texturetimbre0 )
	{
		mIndex = new Vector2 ();
		mTexture = texturetimbre0;
		defaultTexture = mTexture;
		currentTimbre = -1;
		mIndex.x = x;
		mIndex.y = y;
	}
	
	public void Change(int compasso, int batida, int timbre )
	{
		pressed = !pressed;
		
		if( pressed )
		{
			currentTimbre 	= timbre;
			this.compasso 	= compasso;
			this.batida 	= batida;
		}
		else
		{
			currentTimbre 	= 0;
			this.compasso 	= 0;
			this.batida 	= 0;
		}
		
	}
	
	void Resetar ()
	{
		mTexture = defaultTexture;
		currentTimbre = -1;
	}
}