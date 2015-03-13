using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class _LevelEditor 
{
	public 	LevelInfo		info;	
	public	int				lenght = 0;
				
	public _LevelEditor()
	{
		info = new LevelInfo();
		info.tema = new Tema();
	}
				
	public void Draw()
	{
		info.nome = EditorGUILayout.TextField("Nome da Musica: ", info.nome );
		
		DrawComandos();
		
		DrawPartituras();	
		
		DrawTema();
	}
	
	void DrawComandos ()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Partituras:");
		GUI.color = Color.green;
		if( GUILayout.Button("+") )
		{
			AddPartitura();
		}
		GUI.color = Color.cyan;
		if( GUILayout.Button("||") )
		{
			DuplicarPartitura(info.partituras[info.partituras.Length-1]);
		}
		GUI.color = Color.red;
		if( GUILayout.Button("-") )
		{
			RemoverPartitura();
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
	}	

	void DrawPartituras ()
	{
		if( info.partituras != null && info.partituras.Length > 0 ) 
		{
			for( int i = 0; i < info.partituras.Length; i++ )
			{
				info.partituras[i] = (Partitura) EditorGUILayout.ObjectField( "Partitura:", info.partituras[i], typeof(Partitura), false );				
			}
		}
		
	}	
	
	void UpdateList()
	{
		if( info == null ) info = new LevelInfo();
		if( info.partituras == null ) info.partituras = new Partitura[lenght];
	
		info.partituras = new Partitura[lenght];
	}
	
	
	void AddPartitura ()
	{
		lenght ++;
		UpdateList();
	}

	void DuplicarPartitura (Partitura p )
	{
		
	}

	void RemoverPartitura ()
	{
		lenght --;
		UpdateList();
	}

	void DrawTema ()
	{
		Tema t = info.tema;
		
		EditorGUILayout.BeginHorizontal();
		
		t.corBackground 	= EditorGUILayout.ColorField( "Background:", t.corBackground );
		t.corEscuro		 	= EditorGUILayout.ColorField( "Tons escuros:", t.corEscuro );
		t.corClaro		 	= EditorGUILayout.ColorField( "Tons claros:", t.corClaro );
		t.corTextos		 	= EditorGUILayout.ColorField( "Tons textos:", t.corTextos );
		
		EditorGUILayout.EndHorizontal();
		
	}
}