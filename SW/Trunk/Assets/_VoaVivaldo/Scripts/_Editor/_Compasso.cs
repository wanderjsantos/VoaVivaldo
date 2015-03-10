using UnityEngine;	
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class _CompassoEditor 
{
	_TrechoEditor trecho;
	public List<_NotaEditor> notas;
	public NotaInfo notaDebug;
	
	
	public _CompassoEditor( int linhas, int colunas, _TrechoEditor _trecho)
	{
		trecho = _trecho;
		notas = new List<_NotaEditor>();
		notaDebug = new NotaInfo();
	}
	
	public void DrawCompasso()
	{
		EditorGUILayout.BeginHorizontal();
		
		GUI.color = VerificarValorDoCompasso();		
		GUILayout.Button( "", GUILayout.Width(30f), GUILayout.Height(30f) ) ;
		
		GUI.color = Color.white;
						
		foreach( _NotaEditor n in notas )
		{
			float width = Vivaldos.WIDTH_COMPASSO/ (float)n.notaInfo.duracao;
			
			if( n.notaInfo.tipo == TipoDeNota.PAUSA )	GUI.color = Color.gray;
			else 										GUI.color = Color.white;
				
			
			if( GUILayout.Button( ((int)n.notaInfo.timbre).ToString() + "-" +  n.notaInfo.duracao.ToString(), GUILayout.Width(width), GUILayout.Height(20f) ) )
			{				
				EditNota( n );
			}
		}
		GUI.color = Color.white;
		
		EditorGUILayout.EndHorizontal();
		
	}
	
	public void EditNota( _NotaEditor n )
	{
		int dur = (int) n.notaInfo.duracao;
		
		if( dur <= (int) Duracao.SEMIBREVE ) dur = 128;
		
		dur = dur / 2;
		
		n.notaInfo.duracao = (Duracao) dur;
	}	
	
	
	float GetAllWidths ()
	{
		float ret = 0f;
		foreach( _NotaEditor nota in notas )
		{
			ret += Vivaldos.WIDTH_COMPASSO/ (float)nota.notaInfo.duracao;
		}
		return ret;
	}

 
	
	public void DrawComandos()
	{
		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( Vivaldos.WIDTH_COMPASSO - GetAllWidths());
		    GUILayout.Label("Nova nota:");			
			notaDebug.tipo = (TipoDeNota) EditorGUILayout.EnumPopup( "Tipo:", notaDebug.tipo);
			notaDebug.timbre = (Timbre) EditorGUILayout.EnumPopup( "Timbre:", notaDebug.timbre);
			notaDebug.duracao = (Duracao) EditorGUILayout.EnumPopup( "Duracao:", notaDebug.duracao);
			notaDebug.compasso = trecho._compassos.IndexOf(this) ;
			GUILayout.Label("No compasso: " + notaDebug.compasso.ToString() );
			
			
			
			GUI.color = Color.green;
			if( GUILayout.Button( "+" , GUILayout.Width(100f)) && VerificarValorDoCompasso() != Color.red ) AdicionarNovaNota();
			
			GUI.color = Color.cyan;
			if( GUILayout.Button( "||" , GUILayout.Width(100f) )) DuplicarUltimaNota();
		
			GUI.color = Color.red;
			if( GUILayout.Button( "-" , GUILayout.Width(100f) )) RemoverUltimaNota();
			GUI.color = Color.white;
		
		EditorGUILayout.EndHorizontal();
	}
	
	public Color VerificarValorDoCompasso()
	{
		float soma = 0f;
		Color ret	= Color.white;
		
		for( int i = 0; i < notas.Count; i ++ )
		{
			soma += 1f/ (float) notas[i].notaInfo.duracao;
		}
		
		if( soma == 1f ) ret = Color.green;
		if( soma < 1f ) ret = Color.yellow;
		if( soma > 1f ) ret = Color.red;
		
		return ret;
		
	}

	void DuplicarUltimaNota ()
	{
			_NotaEditor novaNota = new _NotaEditor();
			NotaInfo novoInfo = new NotaInfo();
			_NotaEditor qualDuplicar = notas[notas.Count-1];
		
			novaNota.notaInfo = novoInfo;
			
			novoInfo.tipo = qualDuplicar.notaInfo.tipo;
			novoInfo.batida = qualDuplicar.notaInfo.batida;
			novoInfo.compasso = qualDuplicar.notaInfo.compasso;
			novoInfo.duracao = qualDuplicar.notaInfo.duracao;
			novoInfo.timbre = qualDuplicar.notaInfo.timbre;
			
			notas.Add( novaNota );
	}

	void DuplicarEsteCompasso ()
	{
		trecho.DuplicarCompasso( this );
	}
	
	public void AdicionarNovaNota()
	{
		_NotaEditor n = new _NotaEditor();
		n.notaInfo = notaDebug;
		notaDebug = new NotaInfo();
		notas.Add( n );
	}
	
	public void RemoverUltimaNota()
	{
		if( notas.Count == 0 ) return;
		notas.Remove( notas[ notas.Count-1] );
	}
	
	
}




