using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public enum EstadoDeNotas{ MENOS, CORRETO, MAIS }

public class _LinhaDeNotasEditor 
{
	public _LinhaDeNotasEditorInfo mInfo;
	
	public _LinhaDeNotasEditor()
	{		
		Init();
	}
		
	public void Init()
	{
		mInfo = new _LinhaDeNotasEditorInfo();		
		mInfo.SetNotas((int) Duracao.SEMINIMA);
	}
	
	public void Draw()
	{	
		EditorGUILayout.BeginHorizontal();
		for(int i = 0; i < mInfo.notas.Count; i++ )
		{
			mInfo.notas[i].Draw();
		}
		EditorGUILayout.EndHorizontal();
	}
	
	public void VerificarCores()
	{
		Color c = Color.white;
		switch( NotasCompletas() )
		{
			case EstadoDeNotas.CORRETO:
				c = Color.green;
				break;
			case EstadoDeNotas.MAIS:
				c = Color.red;
				break;
			case EstadoDeNotas.MENOS:
				c = Color.yellow;
				break;
			default :
				break;
		}
		
		SetColors( c );
	}

	void SetColors (Color c)
	{
		for(int i = 0; i < mInfo.notas.Count; i++ )
		{
			mInfo.notas[i].SetColor( c );
		}
	}
	
	
	public EstadoDeNotas NotasCompletas()
	{
		EstadoDeNotas ret = EstadoDeNotas.CORRETO;
		
		float soma = 0f;
		for(int i = 0; i < mInfo.notas.Count; i++ )
		{
			if( mInfo.GetDuracaoDeNota( i ) != -1 )
			{
				soma += 1f / (float) mInfo.GetDuracaoDeNota( i );
			}
		}
		
		if( soma == 1f ) ret = EstadoDeNotas.CORRETO;
		if( soma < 1f ) ret = EstadoDeNotas.MENOS;
		if( soma > 1f ) ret = EstadoDeNotas.MAIS;
		
		return ret;
		
	}
	
	public void ChangeNotas()
	{}

}




