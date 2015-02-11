using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;

[RequireComponent ( typeof(AudioSource))]
public class GravadorDeMusica : EditorWindow {

	public static GravadorDeMusica mWindow;

	public static string nome = "CurrentLevel";

	public static int 				BPM = 120;
	public static AudioClip 		audioBase;
	public static AudioClip 		instrumentoTemporario;
	public static List<AudioClip> 	audioInstrumento;

	public static int linhas = 14;
	public static int colunas = 4;

	public static int totalDeCompassos = 3;

	public static Texture2D timbre1;
//	public static Texture2D timbre2;
//	public static Texture2D timbre3;
//	public static Texture2D timbre4;
//	public static Texture2D timbre5;
	public static Texture2D timbre0;

	public static BTN_Nota[,,] grid;
	public static List<BTN_Nota> allNotas = new List<BTN_Nota>();

	[MenuItem( "Vivaldo/Gravador de Musicas")]
	static void Init()
	{
		mWindow = EditorWindow.GetWindow<GravadorDeMusica> ();

		timbre1 = Resources.Load ("Nota1") as Texture2D;
//		timbre2 = Resources.Load ("Nota2") as Texture2D;
//		timbre3 = Resources.Load ("Nota3") as Texture2D;
//		timbre4 = Resources.Load ("Nota4") as Texture2D;
//		timbre5 = Resources.Load ("Nota5") as Texture2D;
		timbre0 = Resources.Load ("Nota0") as Texture2D;

		audioBase = Resources.Load ("Base1") as AudioClip;
		instrumentoTemporario = Resources.Load ("Instrumento1") as AudioClip;
		audioInstrumento = new List<AudioClip> ();

		CreateButtons ();
	}

	void OnGUI()
	{

		DrawBPM ();
		DrawAudioClips ();


		DrawButtons ();

		nome = EditorGUILayout.TextField ("Nome do Arquivo:",nome);
		GUILayout.BeginHorizontal ();
			GUI.color = Color.red;
			if (GUILayout.Button ("Resetar")) 
			{
				Init();
			}

			GUI.color = Color.green;
			if (GUILayout.Button ("Salvar")) 
			{
				SaveManager.Save(grid);
			}
		GUILayout.EndHorizontal ();
	}

	void DrawBPM ()
	{
		BPM = EditorGUILayout.IntField ("Batidas por minuto (BPM):", BPM);
	}
	public static bool audioInstrumentosBool = false;
	void DrawAudioClips ()
	{
		audioBase 				= EditorGUILayout.ObjectField ("Base:",audioBase, typeof(AudioClip), false) as AudioClip;

		audioInstrumentosBool = EditorGUILayout.Foldout (audioInstrumentosBool, "Instrumentos");
		if (audioInstrumentosBool) 
		{
			foreach (AudioClip clip in audioInstrumento) 
			{
				GUILayout.Label(clip.name + "\n");
			}

			instrumentoTemporario 	= EditorGUILayout.ObjectField ("Instrumento:",instrumentoTemporario, typeof(AudioClip), false) as AudioClip;

			if( instrumentoTemporario != null )
			{
				GUI.color = Color.green;
				if( GUILayout.Button( "Adicionar Instrumento" ))
			  	{
					audioInstrumento.Add( instrumentoTemporario );
				}
				GUI.color = Color.white;
		   }
		}


	}

	static void CreateButtons ()
	{
		grid = new BTN_Nota[totalDeCompassos, linhas, colunas];
		allNotas = new List<BTN_Nota> ();
		for (int x = 0; x < totalDeCompassos; x ++)
		{
			for (int i =0; i< linhas; i++) 
			{
				for (int j = 0; j < colunas; j++) 
				{
					Texture2D t = new Texture2D (30, 30);
					t = timbre0;
					BTN_Nota button = new BTN_Nota (i, j, t);
					allNotas.Add (button);
					grid [x,i, j] = button;
				}
			}
		}
	}
	public static Vector2 scroll;
	static void DrawButtons ()
	{


		EditorGUILayout.Space ();

		scroll = EditorGUILayout.BeginScrollView (scroll);
			for (int x = 0; x < totalDeCompassos; x ++)
			{
				GUILayout.Label( "Compasso: " + x );

				for (int i = 0; i < linhas; i++) 
				{
					GUILayout.BeginHorizontal ();
						for (int j = 0; j < colunas; j++) 
						{
							if (grid == null || grid[x,i,j] == null || grid [x, i, j].mTexture == null)
								return;
							if( GUILayout.Button (grid [x ,i, j].mTexture, GUILayout.Width (30f), GUILayout.Height (30)) )
							{
								ChangeValue(x, j, i,grid[x,i,j]);
							}
						}
					GUILayout.EndHorizontal ();
				}
			}
		EditorGUILayout.EndScrollView ();

		totalDeCompassos = EditorGUILayout.IntField ("Total de compassos: ", totalDeCompassos);
	}

	static void ChangeValue (int compasso, int batida, int timbre, BTN_Nota bTN_Nota)
	{

		int l = linhas - timbre;
//		Debug.Log ("timbre: " + timbre + "Valor: " + l);

		Texture2D tex;
//		switch (l)
//		{
//		case 1:
			tex = timbre1;
//			break;
//		case 2:
//			tex = timbre2;
//			break;
//		case 3:	
//			tex = timbre3;
//			break;
//		case 4:
//			tex = timbre4;
//			break;
//		case 5:
//			tex = timbre5;
//			break;
//		default:
//			tex = timbre0;
//			break;
//		}

		bTN_Nota.ChangeNota ( compasso, batida, l , tex);
	}
}

public class BTN_Nota
{
	public Vector2 mIndex = new Vector2 (0, 0);
	public Texture2D mTexture;
	public Texture2D defaultTexture;
	public int currentTimbre = -1;
	public int compasso = 1;
	public int batida = 1;

	public BTN_Nota( int x, int y, Texture2D texturetimbre0 )
	{
		mIndex = new Vector2 ();
		mTexture = texturetimbre0;
		defaultTexture = mTexture;
		currentTimbre = -1;
		mIndex.x = x;
		mIndex.y = y;
	}

	public void ChangeNota(int compasso, int batida, int timbre, Texture2D texture )
	{
		if (currentTimbre == timbre || timbre > GravadorDeMusica.linhas) {
			Resetar();
			return;
		}
		mTexture 		= texture;
		currentTimbre 	= timbre;
		this.compasso 	= compasso;
		this.batida 	= batida;

	}

	void Resetar ()
	{
		mTexture = defaultTexture;
		currentTimbre = -1;
	}
}

public static class SaveManager
{
	public static void Save( BTN_Nota[,,]botoes)
	{
		
		GameObject 	go = new GameObject ("Level:" + GravadorDeMusica.nome);
		Level 		level = go.AddComponent<Level> ();
		level.mInfo = new LevelInfo ();
		LevelInfo 	levelInfo = level.mInfo;
		
		MusicaData data = TransformButtonsToData (botoes);
		
		levelInfo.dadosDaMusica = data;
		levelInfo.nome = GravadorDeMusica.nome;
		
		Debug.LogWarning ("Salvando");
		
		PrefabUtility.CreatePrefab ("Assets/_VoaVivaldo/Prefabs/Levels/" + GravadorDeMusica.nome+".prefab", go);
		AssetDatabase.Refresh ();
		
		XmlSerializer serializer = new XmlSerializer (typeof(MusicaData));
		FileStream stream = new FileStream ("Assets/Resources/MusicaXML-"+GravadorDeMusica.nome+".xml", FileMode.Create);
		serializer.Serialize (stream, data);
		
		serializer = new XmlSerializer (typeof(LevelInfo));
		stream = new FileStream ("Assets/Resources/LevelXML-"+GravadorDeMusica.nome+".xml", FileMode.Create);
		serializer.Serialize (stream, levelInfo);
		
		
		UnityEngine.MonoBehaviour.DestroyImmediate(go);
		
		stream.Close ();
	}
	
	static MusicaData TransformButtonsToData (BTN_Nota[,,] botoes)
	{
		MusicaData ret = new MusicaData ();
		List<NotaInfo> notas = new List<NotaInfo> ();
		
		for (int x = 0; x < GravadorDeMusica.totalDeCompassos; x++) 
		{
			for(int i = 0; i < GravadorDeMusica.linhas; i++ )
			{
				for(int j = 0; j < GravadorDeMusica.colunas; j++ )
				{
					BTN_Nota currentButton = botoes[x,i,j];
					if( currentButton.currentTimbre <= 0 || currentButton.currentTimbre > GravadorDeMusica.linhas ) continue;
					
					
					NotaInfo nota = new NotaInfo();
					
					nota.batida = currentButton.batida+1;
					nota.compasso = currentButton.compasso+1;
					nota.timbre = (Timbre) currentButton.currentTimbre;
					notas.Add(nota);
				}
			}
		}
		
		ret.notas = notas;
		ret.BPM = GravadorDeMusica.BPM;
		
		ret.audioBase = GravadorDeMusica.audioBase.name;
		ret.audioInstrumentos = new List<string> ();
		foreach (AudioClip clip in GravadorDeMusica.audioInstrumento) 
		{
			ret.audioInstrumentos.Add(clip.name);
		}

//		ret.audioInstrumento = GravadorDeMusica.audioInstrumento[0].name;
		
		return ret;
	}
	
	public static BTN_Nota[,,] Load()
	{
		return null;
	}
}

