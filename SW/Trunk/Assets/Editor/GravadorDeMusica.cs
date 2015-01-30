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

	public static int BPM = 120;
	public static AudioClip audioBase;
	public static AudioClip audioInstrumento;

	public static int linhas = 5;
	public static int colunas = 4;

	public static int totalDeCompassos = 1;
	
	public static string nomeDoArquivo = "Musica";

	public static Texture2D timbre1;
	public static Texture2D timbre2;
	public static Texture2D timbre3;
	public static Texture2D timbre4;
	public static Texture2D timbre5;
	public static Texture2D timbre0;

	public static BTN_Nota[,,] grid;
	public static List<BTN_Nota> allNotas = new List<BTN_Nota>();

	[MenuItem( "Vivaldo/Gravador de Musicas")]
	static void Init()
	{
		mWindow = EditorWindow.GetWindow<GravadorDeMusica> ();

		timbre1 = Resources.Load ("Nota1") as Texture2D;
		timbre2 = Resources.Load ("Nota2") as Texture2D;
		timbre3 = Resources.Load ("Nota3") as Texture2D;
		timbre4 = Resources.Load ("Nota4") as Texture2D;
		timbre5 = Resources.Load ("Nota5") as Texture2D;
		timbre0 = Resources.Load ("Nota0") as Texture2D;

		CreateButtons ();
	}

	void OnGUI()
	{
		DrawBPM ();
		DrawAudioClips ();


		DrawButtons ();
		GUILayout.BeginHorizontal ();
			GUI.color = Color.red;
			if (GUILayout.Button ("Resetar")) 
			{
				CreateButtons();
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
		BPM = EditorGUILayout.IntField ("Beats per minute (BPM):", BPM);
	}

	void DrawAudioClips ()
	{
		audioBase 			= EditorGUILayout.ObjectField ("Base:",audioBase, typeof(AudioClip), false) as AudioClip;
		audioInstrumento 	= EditorGUILayout.ObjectField ("Instrumento:",audioInstrumento, typeof(AudioClip), false) as AudioClip;
		
		EditorGUILayout.BeginHorizontal();
		
			if( GUILayout.Button( "Play" ) )
			{
				PlayAudios();
			}
			
			if( GUILayout.Button( "Stop" ) )
			{
				StopAudios();
			}
		EditorGUILayout.EndHorizontal();
	}

	public static GameObject aBase;
	public static GameObject aInst;
	void PlayAudios ()
	{
		aBase = new GameObject( "AudioBase" );
		aInst = new GameObject( "AudioInstrumento" );
		
		aBase.hideFlags = HideFlags.HideAndDontSave;
		aInst.hideFlags = HideFlags.HideAndDontSave;
		
		AudioSource b = aBase.AddComponent<AudioSource>();
		AudioSource i = aInst.AddComponent<AudioSource>();
		
		b.clip = audioBase;
		i.clip = audioInstrumento;
		
		b.Play();
		i.Play();
		
	}

	void StopAudios ()
	{
		if( aBase )
		{
			aBase.audio.Stop();
			DestroyImmediate(aBase);
		}
		
		if( aInst )
		{
			aInst.audio.Stop();
			DestroyImmediate(aInst);
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
		
		audioBase 			= Resources.Load("Base1") as AudioClip;
		audioInstrumento 	= Resources.Load("Instrumento1") as AudioClip;
	}
	
	static void AddCompasso ()
	{
				
		BTN_Nota[,,] temp = grid.Clone() as BTN_Nota[,,];
		
		totalDeCompassos ++;
		CreateButtons();
						
		for (int x = 0; x < totalDeCompassos-1; x ++)
		{
			for (int i =0; i< linhas; i++) 
			{
				for (int j = 0; j < colunas; j++) 
				{
					grid[x,i,j] = temp[x,i,j];
				}
			}
		}
	}
	
	static void RemoveCompasso()
	{
		BTN_Nota[,,] temp = grid.Clone() as BTN_Nota[,,];
		
		totalDeCompassos--;
		
		totalDeCompassos = Mathf.Clamp( totalDeCompassos , 1, 10000000);
		CreateButtons();
		
		for (int x = 0; x < totalDeCompassos; x ++)
		{
			for (int i =0; i< linhas; i++) 
			{
				for (int j = 0; j < colunas; j++) 
				{
					grid[x,i,j] = temp[x,i,j];
				}
			}
		}
	}
	
	
	
	public static Vector2 scroll;
	static void DrawButtons ()
	{
		nomeDoArquivo = GUILayout.TextField( nomeDoArquivo );

		scroll = EditorGUILayout.BeginScrollView (scroll);
			for (int x = 0; x < totalDeCompassos; x ++)
			{
				GUILayout.Label( "Compasso: " + x );

				for (int i = 0; i < linhas; i++) 
				{
					GUILayout.BeginHorizontal ();
						for (int j = 0; j < colunas; j++) 
						{
							if( GUILayout.Button (grid [x ,i, j].mTexture, GUILayout.Width (30f), GUILayout.Height (30)) )
							{
								ChangeValue(x, j, i,grid[x,i,j]);
							}
						}
					GUILayout.EndHorizontal ();
				}
			}
		EditorGUILayout.EndScrollView ();
		
		if( GUILayout.Button(" - Remover Compasso") )
		{
			RemoveCompasso();
		}
		
		if( GUILayout.Button(" + Adicionar Compasso") )
		{
			AddCompasso();
		}
		
		
		
		
	}

	static void ChangeValue (int compasso, int batida, int timbre, BTN_Nota bTN_Nota)
	{

		int l = linhas - timbre;

		Texture2D tex;
		switch (l)
		{
		case 1:
			tex = timbre1;
			break;
		case 2:
			tex = timbre2;
			break;
		case 3:	
			tex = timbre3;
			break;
		case 4:
			tex = timbre4;
			break;
		case 5:
			tex = timbre5;
			break;
		default:
			tex = timbre0;
			break;
		}

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
		if (currentTimbre == timbre || timbre > 5) {
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
		MusicaData data = TransformButtonsToData (botoes);


		Debug.LogWarning ("Salvando");
		XmlSerializer serializer = new XmlSerializer (typeof(MusicaData));
		
		FileStream stream = new FileStream (Application.persistentDataPath + "/" + GravadorDeMusica.nomeDoArquivo +".xml", FileMode.Create);
		serializer.Serialize (stream, data);
		
		stream = new FileStream( "Assets/Resources/" + GravadorDeMusica.nomeDoArquivo +".xml", FileMode.Create );
		serializer.Serialize( stream, data );
		
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
					if( currentButton.currentTimbre <= 0 || currentButton.currentTimbre > 5 ) continue;


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
		ret.audioInstrumento = GravadorDeMusica.audioInstrumento.name;

		return ret;
	}

	public static BTN_Nota[,,] Load()
	{
		return null;
	}
}