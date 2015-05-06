using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Loader))]
public class LoaderInspector : Editor 
{
	public bool eventsFilled = false;
	void SetEvents()
	{
		Loader.LoadComplete += new LoadingAssetsCompleteEventHandler( LoadComplete );
	}

	void LoadComplete (object sender, System.EventArgs e)
	{
		if( EditorApplication.isPlaying ) return;
		
		Debug.Log("Inspector - LoadComplete");
		SceneView.RepaintAll();
		EditorApplication.SaveScene();
		
		
		
	}
	
	
	public override void OnInspectorGUI ()
	{
		if( eventsFilled == false )
		{
			eventsFilled = true;
			SetEvents();
		}
		
		
		Loader loader = target as Loader;
				
		ProgressBar( loader.percent, "Percent loaded: " + loader.percent.ToString() + "%" );
						
		loader.currentLoadingInfo.pathOrUrlToAsset = EditorGUILayout.TextField("Path/Url: ", loader.currentLoadingInfo.pathOrUrlToAsset );
						
		EditorGUILayout.LabelField("Depending on the variable 'Path/Url', complete below", EditorStyles.whiteMiniLabel);
		
		
		EditorGUILayout.BeginToggleGroup( "Path:", Loader.isSceneFile( loader.currentLoadingInfo.pathOrUrlToAsset ) );
			
			loader.currentLoadingInfo.placeInSceneWhenLoaded = EditorGUILayout.Toggle( "Place in scene", loader.currentLoadingInfo.placeInSceneWhenLoaded );
			loader.currentLoadingInfo.destroyAssetsInCurrentScene = EditorGUILayout.Toggle( "Destroy all objects when load new scene", loader.currentLoadingInfo.destroyAssetsInCurrentScene );
		EditorGUILayout.EndToggleGroup();
		
		EditorGUILayout.BeginToggleGroup( "URL:", Loader.isURL( loader.currentLoadingInfo.pathOrUrlToAsset ) );
			
			loader.currentLoadingInfo.placeInSceneWhenLoaded = EditorGUILayout.Toggle( "Place in scene", loader.currentLoadingInfo.placeInSceneWhenLoaded );
			loader.currentLoadingInfo.saveLocallyWhenBundle = EditorGUILayout.Toggle( "Save new bundle in disk", loader.currentLoadingInfo.saveLocallyWhenBundle );
		
			if( GUILayout.Button( "Clean Cache", EditorStyles.miniButton  ) )
			{
				Caching.CleanCache();
			}
		
		EditorGUILayout.EndToggleGroup();				
		
		EditorGUILayout.Space();
		
		if( Loader.isURL( loader.currentLoadingInfo.pathOrUrlToAsset ) || Loader.isSceneFile( loader.currentLoadingInfo.pathOrUrlToAsset )  )
		{
			if( Application.isPlaying == true && GUILayout.Button( "Download AssetBundle/Scene", EditorStyles.miniButton  ) )
			{
				
				if( Loader.instance != null )
				{
					Loader.Load( Loader.instance.currentLoadingInfo );
				}
				else
				{
					Debug.LogWarning("Logger::>> There's no instance on Scene");
				}
				
			}
		}		
		
	}
	
	
	void ProgressBar (float value, string label )
	{
		Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
		EditorGUI.ProgressBar(rect, value, label);
		EditorGUILayout.Space();
	}
		
	Rect GetRect()
	{
			return GUILayoutUtility.GetRect( 18,18 );
	}
	
}