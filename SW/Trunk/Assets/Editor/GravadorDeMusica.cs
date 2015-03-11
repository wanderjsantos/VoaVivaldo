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
	public static _MusicaEditor		musicaAtual;
	public static bool 				init 		= false;
	
	public static bool				isShift 	= false;
	public static bool				isControl	= false;
	
	public static Vector2			scroll;

	[MenuItem( "Vivaldo/Gravador de Musicas")]
	static void Init()
	{
		mWindow = EditorWindow.GetWindow<GravadorDeMusica> ();
		
		init = true;

		musicaAtual = new _MusicaEditor();

	}

	void OnGUI()
	{
		if( mWindow == null || !init || musicaAtual == null ) return;
		
		isShift 	= Event.current.shift;
		isControl 	= Event.current.control;
		
		EditorGUILayout.Toggle( "isShift:", isShift );
		EditorGUILayout.Toggle( "isControl:", isControl );
		
		musicaAtual.nome 		= DrawNome		( musicaAtual.nome );
		musicaAtual.BPM			= DrawBPM		( musicaAtual.BPM );
		musicaAtual.audioBase 	= DrawAudioClip	( musicaAtual.audioBase );
		
		
		scroll = EditorGUILayout.BeginScrollView(scroll);
		foreach( _InstrumentoEditor banda in musicaAtual.banda )
		{
			DrawInstrumentos( banda );
		}
		EditorGUILayout.EndScrollView();		
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Instrumento: ");
		GUI.color = Color.green;
		if( GUILayout.Button("+") )
		{
			musicaAtual.AddInstrumento();
		}
		GUI.color = Color.red;
		if( GUILayout.Button("-") )
		{
			musicaAtual.RemoveInstrumento();
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
		
		DrawTema();
		
		DrawRodaPe();
		
	}

	void DrawTema ()
	{		
		GUILayout.Space( 20f );
		
		EditorGUILayout.BeginHorizontal();	
		
		if( musicaAtual.tema == null ) return;
				
		musicaAtual.tema.nome 			= EditorGUILayout.TextField( "Tema:", musicaAtual.tema.nome );
							
		musicaAtual.tema.corBackground 	= EditorGUILayout.ColorField( "Background:", musicaAtual.tema.corBackground );
		musicaAtual.tema.corEscuro 		= EditorGUILayout.ColorField( "Tons escuros:", musicaAtual.tema.corEscuro );
		musicaAtual.tema.corClaro 		= EditorGUILayout.ColorField( "Tons claros:", musicaAtual.tema.corClaro );
		musicaAtual.tema.corTextos		= EditorGUILayout.ColorField( "Textos:", musicaAtual.tema.corTextos );
		
		EditorGUILayout.EndHorizontal();
		
		GUILayout.Space( 20f );
	}	
	
	void DrawInstrumentos (_InstrumentoEditor banda)
	{
		banda.foldout = EditorGUILayout.Foldout( banda.foldout, banda.nome );
		if( banda.foldout == false ) return;
		
		banda.nome = DrawNome ( banda.nome );
		banda.audio = DrawAudioClip( banda.audio );
		
		banda.personagem = (QualPersonagem) EditorGUILayout.EnumPopup("Personagem:", banda.personagem );
		
		banda.scroll = EditorGUILayout.BeginScrollView( banda.scroll );
			foreach( _TrechoEditor trecho in banda.trechos )
			{
				DrawTrecho( trecho );
			}
		EditorGUILayout.EndScrollView();
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Trecho: ");
		GUI.color = Color.green;
		if( GUILayout.Button("+") )
		{
			banda.AddTrecho();
		}
		
		GUI.color = Color.cyan;
		if( GUILayout.Button("||") )
		{
			banda.DuplicarTrecho();
		}
		
		GUI.color = Color.red;
		if( GUILayout.Button("-") )
		{
			banda.RemoveTrecho();
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
		
	}

	void DrawTrecho (_TrechoEditor trecho)
	{
		trecho.foldout = EditorGUILayout.Foldout( trecho.foldout, trecho.nome );
		if( trecho.foldout == false ) return;		
		
		trecho.nome = DrawNome( trecho.nome );
		trecho.Draw();		
		trecho.DrawComandos();
		
	}

	void DrawRodaPe ()
	{
		EditorGUILayout.BeginHorizontal();
		GUI.color = Color.cyan;
		if( GUILayout.Button("Salvar", GUILayout.Height(20) ))
		{
			SaveManager.Save( musicaAtual );
		}
		
		GUI.color = Color.magenta;
		if( GUILayout.Button("Carregar XML", GUILayout.Height(20)))
		{
			musicaAtual = SaveManager.Load();
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
	}
	
	AudioClip DrawAudioClip (AudioClip clip)
	{
		return EditorGUILayout.ObjectField ("Musica de base:",clip, typeof(AudioClip), false) as AudioClip;
	}
	
	string DrawNome (string nome)
	{
		return EditorGUILayout.TextField ("Nome da Musica:",nome);
	}

	int DrawBPM (int valor)
	{
		return EditorGUILayout.IntField ("Batidas por minuto (BPM):", valor);
	}
	
}


public static class SaveManager
{
	public static void Save( _MusicaEditor musica )
	{
		
		GameObject 	go = new GameObject ("Level:" + GravadorDeMusica.musicaAtual.nome);
		Level 		level = go.AddComponent<Level> ();
		level.mInfo = new LevelInfo ();
		LevelInfo 	levelInfo = level.mInfo;
		
		
		
		MusicaData data = Vivaldos.TransformEditorToMusicaData( musica );
				
		levelInfo.dadosDaMusica = data;
		levelInfo.nome = musica.nome;
				
		Debug.LogWarning ("Salvando");
		
				  
		PrefabUtility.CreatePrefab ("Assets/_VoaVivaldo/Prefabs/Levels/" + GravadorDeMusica.musicaAtual.nome+".prefab", go);
		AssetDatabase.Refresh ();
		
		XmlSerializer serializer = new XmlSerializer (typeof(MusicaData));
		FileStream stream = new FileStream ("Assets/Resources/MusicaXML-"+GravadorDeMusica.musicaAtual.nome+".xml", FileMode.Create);
		serializer.Serialize (stream, data);
		
		serializer = new XmlSerializer (typeof(LevelInfo));
		stream = new FileStream ("Assets/Resources/LevelXML-"+GravadorDeMusica.musicaAtual.nome+".xml", FileMode.Create);
		serializer.Serialize (stream, levelInfo);
		
		
						
		UnityEngine.MonoBehaviour.DestroyImmediate(go);
		
		stream.Close ();
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
 		
 		ret = Vivaldos.TransformDataToMusicaEditor( data );
		
		 Debug.LogWarning("Carregando:" + ret.nome );
	   
	   return ret;
	}
	
	
}




