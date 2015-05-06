using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net;


[System.Serializable]
public class LoadContentInfo
{
	public string pathOrUrlToAsset;
	public bool destroyAssetsInCurrentScene = false;
	public bool placeInSceneWhenLoaded = true;
	public bool saveLocallyWhenBundle = true;
		
}

public delegate void LoadingAssetsCompleteEventHandler(object sender, EventArgs e);
public delegate void LoadingAssetsInitEventHandler(object sender, EventArgs e);
public delegate void LoadingAssetsErrorEventHandler(object sender, EventArgs e);


public class Loader : MonoBehaviour {
			
	//Instance variables
	public static Loader instance;
	public float percent;
	
	public static float Percent
	{
		get
		{
			if( !instance ) Error( "VirgoLoader::>> There's no instance in scene" );
			return instance.percent;	
		}
		
		set
		{
			if( !instance ) Error( "VirgoLoader::>> There's no instance in scene" );
			instance.percent = value;
		}
	}
	
	public LoadContentInfo currentLoadingInfo;	
	
	//Events
	public static event LoadingAssetsCompleteEventHandler LoadComplete;
	public static event LoadingAssetsInitEventHandler LoadBegin;
	public static event LoadingAssetsErrorEventHandler LoadError;
		
	public static string cacheFolder;
	
	void Awake()
	{
		if( instance != null )
		{
			Debug.LogWarning("VirgoLoader::>>There's a instance on scene, destroying myself");
			
			DestroyImmediate(gameObject);
		}
		instance = this;
		DontDestroyOnLoad( this );
	}
	
	//Public Methods
	public static void Load()
	{	
		Load( instance.currentLoadingInfo );
	}
	
	public static void Load( string name, bool placeInSceneWhenLoaded = true, bool destroyAllAssets = true )
	{
		LoadContentInfo info = new LoadContentInfo();
		info.pathOrUrlToAsset = name;
		info.placeInSceneWhenLoaded = placeInSceneWhenLoaded;
		info.destroyAssetsInCurrentScene = destroyAllAssets; 
		Load( info );
	}
	
	public static void Load( LoadContentInfo info )
	{	
				
		if( !instance )
		{
			Error("VirgoLoader::>>Error: There's no a instance of Loader in scene!");
			return;
		}
		
		if( info == null || info.pathOrUrlToAsset == null || info.pathOrUrlToAsset == "" )
		{
			Error("VirgoLoader::>> Path to asset null or empty");
			return;
		}
				
		string currentPath = info.pathOrUrlToAsset;		
		bool isUrl = isURL( currentPath );
		bool isScene = isSceneFile( currentPath );		
						
		
		if( !isUrl && !isScene )
		{
			currentPath = "file://" + info.pathOrUrlToAsset;
		}
		
	/*
		if( isUrl && isScene ) 
			Debug.Log("VirgoLoader::>> Trying to load a scene in web");
		else
		if( isUrl && !isScene )
			Debug.Log("VirgoLoader::>> Trying to load a bundle in web");
		else
		if( !isUrl && isScene )
			Debug.Log("VirgoLoader::>> Trying to load a local scene");
		else
		if( !isUrl && !isScene )
			Debug.Log("VirgoLoader::>> Trying to load a local bundle");
	*/	
					
		instance.OnLoadedAssetsBegin(new EventArgs());
		instance.StartCoroutine( _Load( currentPath, isScene, info) );
						
	}
		
	public static bool isURL( string url )
	{
		if( String.IsNullOrEmpty( url ) ) return false;
		
		if( url.Contains("http://") || 
			url.Contains("https://")|| 
			url.Contains("www.")	
		  ) 
		{
			return true;
		}
		
		return false;
	}
	
	public static bool isSceneFile( string url )
	{		
		if( String.IsNullOrEmpty( url ) ) return false;
		
		if( !url.Contains(".unity3d") && !url.Contains(".unitypackage") && url.Contains(".unity") )
			return true;
		else
		if( url.Contains(".") )
		{
//			Debug.Log("VirgoLoader::>> Ha '.' e nao eh nenhum arquivo '.unity', entao nao eh um arquivo de cena");
			return false;
		}
		else
			return true;
		
			
	}
				
	//Private Methods
	static void _SaveLocally (WWW www)
	{
#if !UNITY_WEBPLAYER
		cacheFolder = Application.persistentDataPath;
		
		string file = Path.GetFileName( www.url );
		if( file == null )
		{
			Error("VirgoLoader::>>Error: Path Null");
			return;
		}
		
		string cachePath = cacheFolder + "/" + file;
		
		Debug.Log("VirgoLoader::>>Saving in - " + cachePath );
		
		if( !Directory.Exists( cacheFolder ) )
			Directory.CreateDirectory (cachePath);
				
		File.WriteAllBytes(cachePath, www.bytes);	
#endif
	}
	
	static bool FileExistLocally (string path)
	{
#if !UNITY_WEBPLAYER
		cacheFolder = Application.persistentDataPath;// + "/" + PlayerSettings.productName;		
		
		string file = Path.GetFileName( path );
		if( file == null )
		{
			Error("VirgoLoader::>>Error: Path Null");
			return false;
		}
		
		string cachePath = cacheFolder + "/" + file;
						
		if (File.Exists (cachePath))
		{
			return true;
		}
#endif
		
		return false;
	}
	
	static void Error( string s )
	{
		Debug.LogError( s );
		instance.OnLoadedAssetsError( new EventArgs());
	}
		
	static IEnumerator _Load( string path, bool isScene, LoadContentInfo info )
	{
		AssetBundle bundle = new AssetBundle();
				
		if( !isScene )
		{
#if !UNITY_WEBPLAYER
			if( !FileExistLocally( path ) )
			{		
				Debug.Log("VirgoLoader::>>Downloading : " + path);
				
				WWW www = new WWW( path );
				
				while( www.isDone == false )
				{
					instance.percent = www.progress * 100;
					yield return null;
				}		
				
				instance.percent = 100f;
				
				if( www.error != null )
				{
					instance.OnLoadedAssetsError(new EventArgs());
					Error("VirgoLoader::>>Error: " + www.error );
											
				}
				else
				bundle = www.assetBundle;
				
				if(info.saveLocallyWhenBundle )
					_SaveLocally( www );
			}
			else
			{
				instance.StartCoroutine(LoadLocalFile( path ));
			}
#endif
#if UNITY_WEBPLAYER
			Debug.Log("Loader (WebPlayer)::>>Downloading : " + path);
				
				WWW www = new WWW( path );
				
				while( www.isDone == false )
				{
					instance.percent = www.progress * 100;
					yield return null;
				}		
			
				instance.percent = 100f;
				
				if( www.error != null )
				{
					instance.OnLoadedAssetsError(new EventArgs());
					Error("VirgoLoader::>>Error: " + www.error );
											
				}
				else
				bundle = www.assetBundle;
#endif

		}
		else
		{
			Debug.Log("VirgoLoader::>>Loading scene locally : " + path );
					
			AsyncOperation operation;
			
			if(!info.destroyAssetsInCurrentScene )
			{
				operation = Application.LoadLevelAdditiveAsync( path );
			}	
			else
			{
				operation = Application.LoadLevelAsync( path );
			}
			
			operation.allowSceneActivation = info.placeInSceneWhenLoaded;
			
			while( operation.progress < 1 )
			{
				instance.percent = operation.progress * 100;
				yield return null;
			}
			
			instance.percent = 100f;
		}
		
		if( info.placeInSceneWhenLoaded && bundle != null)
		{
			if(!isScene)
			{
				Instantiate( bundle.mainAsset );
				bundle.Unload(false);
			}
		}		
		
		instance.OnLoadedAssetsComplete( new EventArgs());
	}
	
	static IEnumerator LoadLocalFile (string path)
	{
		cacheFolder = Application.persistentDataPath;
		
		string file = Path.GetFileName( path );
		if( file == null )
		{
			Error("VirgoLoader::>>Path Null");
			yield break;
		}
		
		string cachePath = cacheFolder + "/" + file;
		
		Debug.Log("VirgoLoader::>>Loading file locally in :" + cachePath );
		
		if (File.Exists (cachePath))
		{
			WWW www = new WWW( "file://" + cachePath );
			
			while( www.progress < 1 )
			{
				instance.percent = www.progress * 100;
				yield return null;
			}
			
			instance.percent = 100f;
			
			if( www.error != null )
			{
				instance.OnLoadedAssetsError(new EventArgs());
				Error("VirgoLoader::>>Error : " + www.error );
				yield break;
			}
			else						
			if( www.assetBundle != null )
			{
				Instantiate( www.assetBundle.mainAsset );
			}
			
			www.assetBundle.Unload(false);
		}
		else
		{
			Error("VirgoLoader::>>File not exists");
			
			yield break;
		}
		
		
	}
	
	// Calling Events
	public void OnLoadedAssetsComplete( EventArgs e )
	{
		if( LoadComplete != null )
			LoadComplete( this, e );
	}
	
	public void OnLoadedAssetsBegin( EventArgs e )
	{
		if( LoadBegin != null )
			LoadBegin( this, e );
	}
	
	public void OnLoadedAssetsError( EventArgs e )
	{
		if( LoadError != null )
			LoadError( this, e );
	}
	
}
