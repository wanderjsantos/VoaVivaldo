using UnityEngine;	
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class _CompassoEditor : VivaldoEditor
{
	public _TrechoEditor trecho;
	public List<_NotaEditor> notas;
	public NotaInfo notaDebug;
	
	public _CompassoEditor()
	{
		new _CompassoEditor( Vivaldos.LINHAS, Vivaldos.COLUNAS, null );
	}
	
	public _CompassoEditor( int linhas, int colunas, _TrechoEditor _trecho)
	{
		trecho = _trecho;
		notas = new List<_NotaEditor>();
		notaDebug = new NotaInfo();
	}	
	
	
	public void Draw()
	{
		EditorGUILayout.BeginHorizontal();
		
		GUI.color = VerificarValorDoCompasso();		
		GUILayout.Button( "", GUILayout.Width(30f), GUILayout.Height(30f) ) ;
		
		GUI.color = Color.red;
		if( GUILayout.Button("X", GUILayout.Width(20f), GUILayout.Height(20f) ) ) trecho.RemoveCompasso( this );
		
		GUI.color = Color.white;
						
		for( int i = 0; i < notas.Count; i++ )
		{
			_NotaEditor n = notas[i];
							
			switch( n.notaInfo.tipo )
			{
				case TipoDeNota.NOTA:
					DrawNotaComum( n );
					break;
				case TipoDeNota.PAUSA:
					DrawPausa( n );
					break;
				case TipoDeNota.NOTA_LONGA:
					DrawNotaLonga(n);
					break;
				default:
					DrawNotaComum(n);
					break;
			}
			
		}
		
		GUI.color = Color.white;
		
		EditorGUILayout.EndHorizontal();
		
	}
	
	public void DrawNotaComum( _NotaEditor n )
	{
		float width = Vivaldos.WIDTH_COMPASSO/ (float)n.notaInfo.duracao;
		
		GUI.color = Color.white;		
		if( GUILayout.Button( ((int)n.notaInfo.timbre).ToString() + "-" +  n.notaInfo.duracao.ToString(), GUILayout.Width(width), GUILayout.Height(20f) ) )
		{				
			EditNota( n );
		}
	}
	
	public void DrawPausa( _NotaEditor n )
	{
		float width = Vivaldos.WIDTH_COMPASSO/ (float)n.notaInfo.duracao;
		
		GUI.color = Color.gray;
		
		if( GUILayout.Button( ((int)n.notaInfo.timbre).ToString() + "-" +  n.notaInfo.duracao.ToString(), GUILayout.Width(width), GUILayout.Height(20f) ) )
		{				
			EditNota( n );
		}
	}
	
	public void DrawNotaLonga( _NotaEditor n )
	{
		float width = Vivaldos.WIDTH_COMPASSO/ (float)n.notaInfo.duracao;
		
		GUI.color = Color.green;
		
		if( GUILayout.Button( ((int)n.notaInfo.timbre).ToString() + "-" +  n.notaInfo.duracao.ToString(), GUILayout.Width(width), GUILayout.Height(20f) ) )
		{				
			EditNota( n );
		}
	}
	
	public void EditNota( _NotaEditor n )
	{
		if( Event.current.control )
		{			
			RemoverNota( n );
			return;
		}
		
		if( Event.current.alt )
		{
			TrocarTipo( n );
			return;
		}
	
		int dur = (int) n.notaInfo.duracao;
		
		if( dur <= (int) Duracao.SEMIBREVE ) dur = 128;
		
		if( dur >= (int) Duracao.SEMIFUSA ) dur = 1;
		
		if( !Event.current.shift )
			dur = dur * 2;
		else
			dur = dur / 2;
		
		n.notaInfo.duracao = (Duracao) dur;
	}	

	void TrocarTipo (_NotaEditor n)
	{
		int t = (int) n.notaInfo.tipo;
		
		if( t == 2 ) t = -1;
		
		t += 1;
		
		n.notaInfo.tipo = (TipoDeNota) t;
		
//		Debug.Log(n.notaInfo.tipo.ToString());
	}

	void RemoverNota (_NotaEditor n)
	{
		if( notas.Contains( n ) == false ) return;
		
		notas.Remove( n );
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
		if( notaDebug == null ) notaDebug = new NotaInfo();
	
		EditorGUILayout.BeginHorizontal();
						
							
			GUILayout.Space( Vivaldos.WIDTH_COMPASSO - GetAllWidths());
		    GUILayout.Label("Nova:");			
			notaDebug.tipo = (TipoDeNota)	EditorGUILayout.EnumPopup( "", notaDebug.tipo, GUILayout.Width(100f));
			notaDebug.timbre = (Timbre) 	EditorGUILayout.EnumPopup( "", notaDebug.timbre, GUILayout.Width(100f));
			notaDebug.duracao = (Duracao) 	EditorGUILayout.EnumPopup( "", notaDebug.duracao, GUILayout.Width(100f));
			
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
			
			novoInfo.tipo 		= qualDuplicar.notaInfo.tipo;
			novoInfo.batida 	= qualDuplicar.notaInfo.batida;
			novoInfo.compasso 	= qualDuplicar.notaInfo.compasso;
			novoInfo.duracao 	= qualDuplicar.notaInfo.duracao;
			novoInfo.timbre 	= qualDuplicar.notaInfo.timbre;
			
			notas.Add( novaNota );
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




