using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _NotaEditor
{
	public enum Estado { NOTHING = 0, PRESSED = 1, TWO_NOTES_PRESS = 2, SECOND_PRESS = 3, LENGHT = 4 }
	
	public Estado estado = Estado.NOTHING;
	public Color cor = Color.white;
	
	public bool			pressed = false;
	public Vector2 				mIndex = new Vector2 (0, 0);
	public Texture2D 			mTexture;
	public Texture2D 			defaultTexture;
	public int					currentTimbre;
	public int					compasso	=	0;
	public int					batida 		=	0;	
	
	public _NotaEditor( int x, int y, Texture2D texturetimbre0 )
	{
		mIndex = new Vector2 ();
		mTexture = texturetimbre0;
		defaultTexture = mTexture;
		
		currentTimbre = -1;
		mIndex.x = x;
		mIndex.y = y;
	}
	
	public int mEstado = 0;
	public void Press(int compasso, int batida, int timbre )
	{
	
		mEstado ++;
		if( mEstado > ((int)Estado.LENGHT)-1 ) mEstado = 0;
		
		
		if( mEstado != 0 ) 	pressed = true;
		else 				pressed = false;
		
		Estado e = (Estado) mEstado;
		switch(e)
		{
			case Estado.NOTHING:
				UnpressNota();
				break;
			case Estado.PRESSED:
				PressNota( timbre, compasso, batida, 0f );
				break;
			case Estado.SECOND_PRESS:
				SegundaNotaPressionada( timbre,compasso,batida );
				break;
			case Estado.TWO_NOTES_PRESS:
				DuasNotasPressionadas( timbre,compasso,batida );
				break;
			default:
				UnpressNota();
				break;
		}
	
	}
	
	public void Draw(int compasso, int linha, int coluna)
	{
//		GUI.color = cor;

		switch( estado )
		{
			case Estado.PRESSED:
				DrawPressed(compasso, linha, coluna);
				break;
			case Estado.SECOND_PRESS:
				DrawSegundoPressionado(compasso, linha, coluna);
				break;
			case Estado.TWO_NOTES_PRESS:
				DrawDoisPressionados(compasso, linha, coluna);
				break;
			default:
//				DrawPressed(compasso, linha, coluna);
				if(	GUILayout.Button ("" , GUILayout.Width (20f), GUILayout.Height (20f)) )
				{
					int l = Vivaldos.LINHAS - linha;
					Press(compasso, linha, coluna);
				}
				break;
			
		}

		GUI.color = Color.white;
	
	}

	void DrawPressed (int compasso, int linha, int coluna)
	{
		GUI.color = Color.green;
		
		if(	GUILayout.Button ("" , GUILayout.Width (20f), GUILayout.Height (20f)) )
		{
			int l = Vivaldos.LINHAS - linha;
			Press(compasso, l, coluna);
		}
		
		GUI.color = Color.white;
	}

	void DrawDoisPressionados (int compasso, int linha, int coluna)
	{
		GUI.color = Color.green;
	
		if(	GUILayout.Button ("" , GUILayout.Width (10f), GUILayout.Height (20f)) ||
		    GUILayout.Button ("" , GUILayout.Width (10f), GUILayout.Height (20f)) )
		{
			int l = Vivaldos.LINHAS - linha;
			Press(compasso, l, coluna);
		}		
		
		GUI.color = Color.white;
	}

	void DrawSegundoPressionado (int compasso, int linha, int coluna)
	{
		GUI.color = Color.white;
		if(	GUILayout.Button ("" , GUILayout.Width (10f), GUILayout.Height (20f)) )
		{
			int l = Vivaldos.LINHAS - linha;
			Press(compasso, l, coluna);
		}
		
		GUI.color = Color.green;
		if(	GUILayout.Button ("" , GUILayout.Width (10f), GUILayout.Height (20f)) )
		{
			int l = Vivaldos.LINHAS - linha;
			Press(compasso, l, coluna);
		}		
		
		GUI.color = Color.white;
	}
	
	void DuasNotasPressionadas( int t, int c, int b )
	{
		PressNota( t,c,b,.5f);
		
		estado = Estado.TWO_NOTES_PRESS;
//		cor = Color.red;
	} 
	
	void SegundaNotaPressionada( int t, int c, int b)
	{
		PressNota( t,c,b,.5f );
		
		estado = Estado.SECOND_PRESS;
//		cor = Color.yellow;
	}

	void PressNota( int t, int c, int b, float e )
	{		
		currentTimbre 	= t;
		this.compasso 	= c;
		this.batida 	= b;
//		this.extra 		= e;
		
		estado = Estado.PRESSED;
//		cor = Color.green;
	}
	
	void UnpressNota()
	{
		currentTimbre 	= 0;
		this.compasso 	= 0;
		this.batida 	= 0;
		
		estado = Estado.NOTHING;
		cor = Color.white;
	}
		
	void Resetar ()
	{
		mTexture = defaultTexture;
		currentTimbre = -1;
	}
	
}




