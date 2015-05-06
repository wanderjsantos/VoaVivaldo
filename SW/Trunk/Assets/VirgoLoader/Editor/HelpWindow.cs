using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class HelpWindow : EditorWindow {
	
	public static string aboutText;
	static HelpWindow helpWindow;
	
	[MenuItem ("Virgo/Loader/Help|About...")]
	static void ShowAbout()
	{
		helpWindow = EditorWindow.GetWindow<HelpWindow>("Help/About");
		
		aboutText = File.ReadAllText("Assets/VirgoLoader/readme.txt");
		
		helpWindow.maxSize = new Vector2( 600, 700 );
		helpWindow.minSize = new Vector2( 600, 400 );	
	}
		
	Vector2 scrollPosition;
	void OnGUI()
	{
		
		scrollPosition = GUILayout.BeginScrollView( scrollPosition,false, true );
		
			GUILayout.Label(aboutText);				
			
			GUILayout.Space( 30 );
			
			if( GUILayout.Button("For other cool stuffs, visit: virgogames.com", EditorStyles.boldLabel ) )
				Application.OpenURL("http://www.virgogames.com");
			
		GUILayout.EndScrollView();
		
		
	}
	
}
