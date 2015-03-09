using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class _LinhaDeNotasEditorInfo
{
	public List<NotaEditor> notas;
	
	public _LinhaDeNotasEditorInfo()
	{
		Init();
	}
	
	public void Init()
	{
		notas = new List<NotaEditor>();
	}
	
	public void Draw()
	{
		EditorGUILayout.BeginHorizontal();
			for( int i =0; i < notas.Count; i++ )
			{
				notas[i].Draw();
			}
		EditorGUILayout.EndHorizontal();
	}
	
	public int GetDuracaoDeNota( int index )
	{
		if( index <= 0 || index > notas.Count ) return -1 ;
		
		return (int)notas[index].notaInfo.duracao;
	}
	
	public void SetNotas ( int notasIniciais )
	{
		if( notas == null ) Init();
		
		for( int i = 0; i < notasIniciais; i++ )
		{
			NotaEditor nE = new NotaEditor();
			nE.SetDuracao( Duracao.SEMINIMA );
			notas.Add(nE);
		}
	}
}

public class NotaEditor 
{
	public NotaInfo notaInfo;
	public vNotaEditor vNota;
	public float 	width;
	
	public NotaEditor()
	{
		notaInfo = new NotaInfo();
		vNota = new vNotaEditor();
		vNota.mNotaEditor = this;
		Init ();
		
	}
	
	public void Init()
	{
		width = (float) (Vivaldos.WIDTH_COMPASSO / (int) notaInfo.duracao);
		
	}
	
	public void Draw()
	{
		vNota.Draw();
		if( vNota.Pressed() ) 
		{
			OnClick();
		}
	}

	void OnClick ()
	{		
		int dur = (int)	notaInfo.duracao;
		
		if( dur == (int)Duracao.SEMIFUSA ) dur = 1;
		
		dur = (dur * 2);
		SetDuracao( dur );		
	}
	
	public void SetColor( Color c )
	{
		vNota.mColor = c;
	}
	
	public void SetDuracao( int duracao )
	{
		SetDuracao( (Duracao) duracao );
	}
	
	public void SetDuracao( Duracao duracao )
	{
		notaInfo.duracao = duracao;
	}
}




/// <summary>
/// View da nota no editor
/// </summary>

public class vNotaEditor 
{
	public NotaEditor mNotaEditor;

	public Color mColor = Color.white;
	
	public bool pressed = false;
	
	public void Draw()
	{
		GUI.color = mColor;
		
		pressed = GUILayout.Button( mNotaEditor.notaInfo.duracao.ToString(), GUILayout.Width( mNotaEditor.width), GUILayout.Height(20f) );
		
		GUI.color = Color.white;
	}

	public bool Pressed ()
	{
		return pressed;
	}
}




