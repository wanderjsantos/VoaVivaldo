using UnityEngine;
using UnityEditor;
using System.Collections;

public class GravadorDeFase : EditorWindow
{
	public static 	GravadorDeFase 		mWindow;
	
	public static	Partitura			partitura;
	public static	_PartituraEditor	partituraEditor;

	[MenuItem( "Vivaldo/Criar Partitura")]
	static void Init()
	{
		mWindow = EditorWindow.GetWindow<GravadorDeFase> ();
		
		partituraEditor = new _PartituraEditor();
	}
	
	public void OnGUI()
	{
		partituraEditor.Draw();
		DrawRodaPe();
	}
	
	void DrawRodaPe ()
	{
		EditorGUILayout.BeginHorizontal();
		GUI.color = Color.green;
		if( GUILayout.Button("Salvar", GUILayout.Height(20) ))
		{
			SaveManager.SaveFase( partituraEditor );
		}
		
		GUI.color = Color.yellow;
		if( GUILayout.Button("Selecionar", GUILayout.Height(20)))
		{
			
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
	}
}
