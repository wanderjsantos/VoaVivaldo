using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class _LevelManagerEditor
{
	public List<_LevelEditor>	levels;
	public string				nome;
	public bool					foldout = true;
	
	public _LevelManagerEditor()
	{
		levels = new List<_LevelEditor>();
		_LevelEditor e = new _LevelEditor();
		levels.Add(e);
	}
}

public class _LevelEditor : VivaldoEditor
{
	public LevelInfo 					info;
	public 	List<_MusicaEditor>			musicas;
	
	public _LevelEditor()
	{
		info = new LevelInfo();
		musicas = new List<_MusicaEditor>();
		AddMusica();
	}

	public void Draw ()
	{
		DrawLevelInfo();
		DrawMusicas();
		DrawTema();
		DrawComandos();
	}

	void DrawLevelInfo ()
	{
		info.nome 				= EditorGUILayout.TextField("Level:", info.nome );		
	}

	void DrawComandos ()
	{
		EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Musicas: ");
			GUI.color = Color.green;
			if( GUILayout.Button("+") )	 	AddMusica();
			
			GUI.color = Color.red;
			if( GUILayout.Button("-") ) 	RemoverMusica();
		
			GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
	}
		
	void DrawMusicas()
	{
		for( int i = 0; i < musicas.Count; i++ )
		{
			musicas[i].DrawMusica();			
		}				
	}
	
	void DrawTema ()
	{		
		GUILayout.Space( 20f );

		EditorGUILayout.BeginHorizontal();	

		if( info.tema == null ) return;
				
		info.tema.nome 			= EditorGUILayout.TextField( "info.tema:", info.tema.nome );							
		info.tema.corBackground 	= EditorGUILayout.ColorField( "Background:", info.tema.corBackground );
		info.tema.corEscuro 		= EditorGUILayout.ColorField( "Tons escuros:", info.tema.corEscuro );
		info.tema.corClaro 		= EditorGUILayout.ColorField( "Tons claros:", info.tema.corClaro );
		info.tema.corTextos		= EditorGUILayout.ColorField( "Textos:", info.tema.corTextos );

		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 20f );
	}	
	
	void AddMusica()
	{
		_MusicaEditor m = new _MusicaEditor();
		musicas.Add(m);
	}
	
	
	void RemoverMusica()
	{
		if( musicas.Count > 0 )
			musicas.RemoveAt( musicas.Count -1 );	
	}
	
}

