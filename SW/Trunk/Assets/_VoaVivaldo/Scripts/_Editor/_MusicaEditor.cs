using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class _MusicaEditor  : VivaldoEditor
{
	MusicaDataInfo	dataInfo;
		
	public 	List<_InstrumentoEditor>	banda;
		
	public _MusicaEditor()
	{
		dataInfo = new MusicaDataInfo();
		
		banda = new List<_InstrumentoEditor>();
		banda.Add(new _InstrumentoEditor());
	}
	
	public void DrawMusica( )
	{
		foldout = EditorGUILayout.Foldout( foldout, "Levels:" );
		if( !foldout ) return;
		
		dataInfo.nome 	= DrawNome		( dataInfo.nome );
		dataInfo.BPM	= DrawBPM		( dataInfo.BPM );
		
		scroll = EditorGUILayout.BeginScrollView(scroll);
		foreach( _InstrumentoEditor b in banda )
		{
			b.Draw();
		}
		EditorGUILayout.EndScrollView();		
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Instrumento: ");
		GUI.color = Color.green;
		if( GUILayout.Button("+") )
		{
			AddInstrumento();
		}
		GUI.color = Color.red;
		if( GUILayout.Button("-") )
		{
			RemoveInstrumento();
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
		
	}
	string DrawNome (string nome)
	{
		return EditorGUILayout.TextField ("Nome da Musica:",nome);
	}
	
	int DrawBPM (int valor)
	{
		return EditorGUILayout.IntField ("Batidas por minuto (BPM):", valor);
	}
	
		
	_InstrumentoEditor NewInstrumento()
	{
		return new _InstrumentoEditor();
	}
	
	public void AddInstrumento()
	{
		banda.Add( NewInstrumento() );
	}
	
	public void RemoveInstrumento()
	{
		if( banda.Count > 1 )
			banda.RemoveAt( banda.Count-1 );
	}
		
	}
