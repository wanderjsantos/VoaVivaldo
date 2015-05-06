using UnityEngine;
using System.Collections;
using System;

public class LoadGUI : MonoBehaviour {
	
	public Texture2D background;
	public Texture2D loadBarBackground;
	public Texture2D loadBarContent;
	public Rect groupRect;
	
	public bool drawGUI = true;
	
	public void Start()
	{
		drawGUI = false;
		
		//Implement events
		Loader.LoadComplete += new LoadingAssetsCompleteEventHandler( LoadComplete );
				
		
	}
	
	void Update()
	{
		if( Input.GetKeyUp(KeyCode.E) )
		{
			BeginLoad();
		}
	}
			
	public void OnGUI()
	{				
		if( !drawGUI ) return;
						
		GUI.DrawTexture( new Rect( 0,0, Screen.width, Screen.height ), background );
		
		GUI.DrawTexture( new Rect( (Screen.width - loadBarBackground.width)/2, (Screen.height - loadBarBackground.height)/2, loadBarBackground.width, loadBarBackground.height), loadBarBackground );
		
		groupRect.x = (Screen.width - loadBarContent.width)/2;
		groupRect.y =  (Screen.height - loadBarContent.height)/2;
		groupRect.width = loadBarContent.width * ( Loader.Percent/100 );
		groupRect.height = loadBarContent.height;
		
		GUI.BeginGroup( groupRect );
		
			GUI.DrawTexture( new Rect( 0 , 0 , loadBarContent.width , loadBarContent.height), loadBarContent );
		
		GUI.EndGroup();
	}
	
	public void BeginLoad()
	{
		drawGUI = true;
		Loader.Load();
	}
	
	public void LoadComplete( object sender, EventArgs e )
	{
		Reset();
	}
	
	void Reset()
	{
		drawGUI = false;
	}
	
}
