using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;

[RequireComponent ( typeof(AudioSource))]
public class GravadorDeMusica : EditorWindow {

	public static GravadorDeMusica 	mWindow;
	public static _LevelEditor		levelEditor;
	
	public static bool 				init 		= false;
	

	[MenuItem( "Vivaldo/Criar Musica")]
	static void Init()
	{
		mWindow = EditorWindow.GetWindow<GravadorDeMusica> ();
		
		init = true;

		levelEditor = new _LevelEditor();
		

	}

	void OnGUI()
	{
		if( mWindow == null || !init || levelEditor == null ) return;
		
		levelEditor.Draw();
		
		DrawRodaPe();		
	}
		
	void DrawRodaPe ()
	{
		EditorGUILayout.BeginHorizontal();
		GUI.color = Color.cyan;
		if( GUILayout.Button("Salvar", GUILayout.Height(20) ))
		{
			SaveManager.SaveLevel( levelEditor);
		}
		
		GUI.color = Color.magenta;
		if( GUILayout.Button("Carregar XML", GUILayout.Height(20)))
		{
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
	}

	
}






