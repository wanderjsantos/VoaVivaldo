using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
using System.Xml.Serialization;
using System.IO;

public static class SaveManager
{
	public static void SaveFase( _PartituraEditor info )
	{
		GameObject go = new GameObject("Partitura:" + info.partituraInfo.nome);
		Partitura p = go.AddComponent<Partitura>();
		
		p.info = info.partituraInfo;
		
		p.info.compassos = new List<Compasso>();
		foreach( _CompassoEditor e in info.compassos )
		{
			p.info.compassos.Add( e.GetCompasso() );
		}
		
//		XmlSerializer 	serializer = new XmlSerializer( typeof( PartituraInfo ) );
//		FileStream 		stream = new FileStream( "Assets/_VoaVivaldo/Partituras/" + info.partituraInfo.nome + ".xml", FileMode.Create);
//		serializer.Serialize(stream, p.info);
//		stream.Close();

		PrefabUtility.CreatePrefab ("Assets/_VoaVivaldo/Prefabs/Partituras/" + info.partituraInfo.nome +".prefab", go);
		AssetDatabase.Refresh ();
		
		Debug.LogWarning ("Salvando");
		
		UnityEngine.MonoBehaviour.DestroyImmediate( go );
		
	}

	public static _PartituraEditor LoadFase ()
	{
		_PartituraEditor ret = new _PartituraEditor();
		
		string path =	EditorUtility.OpenFilePanel( "Selecione um Prefab de partitura","Assets/_VoaVivaldo/Prefabs/Partituras", "prefab");
	   
	  	if( path == null ) return null;	   	
		
		Partitura partitura = AssetDatabase.LoadAssetAtPath( "Assets/_VoaVivaldo/Prefabs/Partituras/" + Path.GetFileName(path), typeof(Partitura) ) as Partitura;
		
		ret.partituraInfo = partitura.info;
		
		for( int i = 0; i < ret.partituraInfo.compassos.Count; i++ )
		{
			_CompassoEditor newCompassoEditor = new _CompassoEditor();
			newCompassoEditor = newCompassoEditor.GetCompassoEditor( ret.partituraInfo.compassos[i] );
			
			ret.compassos.Add(newCompassoEditor);
		}
		
		ret.partituraInfo.clipAudioBase 		= Vivaldos.NameToAudioClip( ret.partituraInfo.nomeAudioBase );
		ret.partituraInfo.clipAudioInstrumento 	= Vivaldos.NameToAudioClip( ret.partituraInfo.nomeAudioInstrumento );
		
		
		return ret;
	}
	
	public static void SaveLevel( _LevelEditor level )
	{
		GameObject go = new GameObject("Level:" + level.info.nome);
		Level l = go.AddComponent<Level>();
		
		l.info = level.info;

		PrefabUtility.CreatePrefab ("Assets/_VoaVivaldo/Prefabs/Levels/" + level.info.nome +".prefab", go);
		AssetDatabase.Refresh ();
		
		Debug.LogWarning ("Salvando");
		
		UnityEngine.MonoBehaviour.DestroyImmediate( go );
		
	}
	

	public static void Save( _LevelEditor level )
	{
		
		//		GameObject 	go			= new GameObject ("Level:" + level.editorInfo.levelInfo.nome);
		//		Level 		levelGO 		= go.AddComponent<Level> ();
		//
		//
		//		level.editorInfo.levelInfo.dadosDaMusica = level.editorInfo.ConvertToData();
		//		levelGO.mInfo = level.editorInfo.levelInfo;
		//		
		//		XmlSerializer 	serializer = new XmlSerializer( typeof( LevelInfo ) );
		//		FileStream 		stream = new FileStream( "Assets/_VoaVivaldo/XML/" + level.editorInfo.levelInfo.nome+".xml", FileMode.Create);
		//		serializer.Serialize(stream, levelGO.mInfo);
		//		stream.Close();
		//	
		//		Debug.LogWarning ("Salvando");
		//
		//		PrefabUtility.CreatePrefab ("Assets/_VoaVivaldo/Prefabs/Levels/" + level.editorInfo.levelInfo.nome +".prefab", go);
		//		AssetDatabase.Refresh ();
		//						
		//		UnityEngine.MonoBehaviour.DestroyImmediate(go);
	}
	
	public static _LevelEditor Load()
	{
		_LevelEditor ret = new _LevelEditor();
		
		//		string path =	EditorUtility.OpenFilePanel( "Selecione um Prefab de level","Assets/_VoaVivaldo/XML", "xml");
		//	   
		//	  	if( path == null ) return null;	   	
		//
		//		XmlSerializer 	serializer = new XmlSerializer( typeof( LevelInfo ) );
		//		StreamReader 		stream = new StreamReader(path);
		//		
		//		LevelInfo info = (LevelInfo) serializer.Deserialize( stream );
		//		Debug.Log(info);
		//		
		//		ret.editorInfo = new EditorLevelInfo();
		//		ret.editorInfo.listMusicaEditor = info.ConvertToEditor();
		//		ret.editorInfo.levelInfo = info;
		//		
		//		Debug.LogWarning("Carregando:" + ret.editorInfo.levelInfo.nome );
		
		
		return ret;
	}
	
	
}
