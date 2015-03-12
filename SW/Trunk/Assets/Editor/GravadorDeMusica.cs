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
	public static _LevelManagerEditor	levelManager;
	
	public static bool 				init 		= false;
	
	public static bool				isShift 	= false;
	public static bool				isControl	= false;
	
	public static Vector2			scroll;

	[MenuItem( "Vivaldo/Gravador de Musicas")]
	static void Init()
	{
		mWindow = EditorWindow.GetWindow<GravadorDeMusica> ();
		
		init = true;

		levelManager = new _LevelManagerEditor();

	}

	void OnGUI()
	{
		if( mWindow == null || !init || levelManager == null ) return;
		
		for( int i = 0; i< levelManager.levels.Count; i++ )
		{
			levelManager.levels[i].Draw();
		}
		
		DrawRodaPe();
		
	}
		
	void DrawRodaPe ()
	{
		EditorGUILayout.BeginHorizontal();
		GUI.color = Color.cyan;
		if( GUILayout.Button("Salvar", GUILayout.Height(20) ))
		{
			SaveManager.Save( levelManager );
		}
		
		GUI.color = Color.magenta;
		if( GUILayout.Button("Carregar XML", GUILayout.Height(20)))
		{
//			levelManager = SaveManager.Load();
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
	}
}


public static class SaveManager
{
	public static void Save( _LevelManagerEditor levelManager )
	{
		
		GameObject 	go = new GameObject ("Level:" + levelManager.nome);
		Level 		level = go.AddComponent<Level> ();
		level.mInfo = new LevelInfo ();
		LevelInfo 	levelInfo = level.mInfo;
		
//		List<LevelData> data = Vivaldos.TransformEditorToLevelData( levelManager );
		
				
//		levelInfo.dadosDaMusica = data;
//		levelInfo.nome = musica.nome;
				
//		Debug.LogWarning ("Salvando");
//	  
//		PrefabUtility.CreatePrefab ("Assets/_VoaVivaldo/Prefabs/Levels/" + GravadorDeMusica.musicaAtual.nome+".prefab", go);
//		AssetDatabase.Refresh ();
//		
//		XmlSerializer serializer = new XmlSerializer (typeof(MusicaData));
//		FileStream stream = new FileStream ("Assets/Resources/MusicaXML-"+GravadorDeMusica.musicaAtual.nome+".xml", FileMode.Create);
//		serializer.Serialize (stream, data);
//						
//		UnityEngine.MonoBehaviour.DestroyImmediate(go);
//		
//		stream.Close ();
	}

	public static _MusicaEditor Load()
	{
		_MusicaEditor ret = new _MusicaEditor();
		MusicaData data = new MusicaData();
		
		string path =	EditorUtility.OpenFilePanel( "Selecione um XML de level","Assets/Resources/", "xml");
	   
	  	if( path == null ) return null;
	   	
 		XmlSerializer serializer = new XmlSerializer( typeof(MusicaData) );
 		StreamReader reader = new StreamReader( path );
 		
 		data = (MusicaData) serializer.Deserialize( reader );
 		reader.Close();
 		
// 		ret = Vivaldos.TransformDataToMusicaEditor( data );
		
//		Debug.LogWarning("Carregando:" + ret.nome );
	   
	   return ret;
	}
	
	
}




