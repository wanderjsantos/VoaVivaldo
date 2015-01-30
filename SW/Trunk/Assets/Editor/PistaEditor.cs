using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public class PistaEditor  {

	public Texture2D pistaBase;
	public Texture2D marcador;

	public float velocidadeDoMarcador = 2f;
	public Vector3 posicaoDoMarcador;
	public Vector2 marcadorNaTabela = Vector2.zero;

	public Vector2 tabela = new Vector2(2, 10 );
	Vector2 scrollPosition = Vector2.zero;

	public PistaEditor()
	{
		pistaBase 	= 	(Texture2D) Resources.Load ("PistaEditor");
		marcador 	= 	(Texture2D) Resources.Load ("Marcador");
		EditorApplication.update += MoverMarcador;
	}


	public void Draw()
	{
//		GUI.Label (new Rect (0, 0, 1000, 100), EditorApplication.timeSinceStartup.ToString());

		scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition);
		for( int j = 0; j < tabela.y; j++ )
			for( int i =0; i < tabela.x ; i++ )
		    	GUI.DrawTexture (new Rect (i * pistaBase.width, 100 + (j * pistaBase.height), 
				                           pistaBase.width, pistaBase.height), pistaBase);
	


		GUI.DrawTexture (new Rect (posicaoDoMarcador.x, posicaoDoMarcador.y, marcador.width, marcador.height), marcador);


		EditorGUILayout.EndScrollView ();

	

	}

	void MoverMarcador ()
	{
		posicaoDoMarcador.x += velocidadeDoMarcador ;
		posicaoDoMarcador.y = 100 + (marcadorNaTabela.y * pistaBase.height);
		
		if (posicaoDoMarcador.x > (tabela.x * pistaBase.width)+ pistaBase.width) 
		{
			posicaoDoMarcador.x = 0;
			marcadorNaTabela.y ++;
		}
		if( GravadorDeMusica.mWindow != null ) 
			GravadorDeMusica.mWindow.Repaint ();
	}
}
