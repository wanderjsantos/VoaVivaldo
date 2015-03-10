using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class _TrechoEditor 
{
	public 	string				nome		= "Trecho";
	
	public 	bool				foldout 	= false;
	public	Vector2				scroll		= Vector2.zero;
	
	public List<_CompassoEditor> _compassos;	
	
	public NotaInfo 			adicionarNota;
	
	public int					compassos 	= Vivaldos.COMPASSOS_DEFAULT;
	public int					linhas		= Vivaldos.LINHAS;
	public int					colunas		= Vivaldos.COLUNAS;
	
	
	public void AddCompasso()
	{
		_CompassoEditor c = new _CompassoEditor( linhas, colunas, this );
		_compassos.Add( c );
		
	}
	
	public void RemoveCompasso()
	{
		if( _compassos.Count == 0 ) return;
		
		_compassos.RemoveAt( _compassos.Count-1);
		
	}

	public void DuplicarCompasso (_CompassoEditor aSerDuplicado)
	{
		if( _compassos.Count == 0 )return;
		_CompassoEditor c = new _CompassoEditor(linhas, colunas, this);
		
		foreach( _NotaEditor n in aSerDuplicado.notas )
		{
			_NotaEditor n1 = new _NotaEditor();
			NotaInfo ni = new NotaInfo();
			
			n1.notaInfo = ni;
			ni.batida = n.notaInfo.batida;
			ni.compasso = n.notaInfo.compasso;
			ni.duracao = n.notaInfo.duracao;
			ni.timbre = n.notaInfo.timbre;
			
			c.notas.Add( n1 );
		}
		
		_compassos.Add(c);
		
	}
	
	public _TrechoEditor()
	{
		Init();
	}
	
	public void Draw()
	{
		
		scroll = EditorGUILayout.BeginScrollView( scroll );
		
		for( int i =0 ; i < _compassos.Count; i++ )
		{
			EditorGUILayout.BeginHorizontal();
			_compassos[i].DrawCompasso();
			_compassos[i].DrawComandos();
			EditorGUILayout.EndHorizontal();
		}
		
		EditorGUILayout.EndScrollView();
	}
	
	public void DrawNotas()
	{
	}

	public void DrawComandos ()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Compasso:");
		GUI.color = Color.green;
		if( GUILayout.Button("+") )
		{
			AddCompasso();
		}
		GUI.color = Color.cyan;
		if( GUILayout.Button("||") )
		{
			DuplicarCompasso(_compassos[_compassos.Count-1]);
		}
		GUI.color = Color.red;
		if( GUILayout.Button("-") )
		{
			RemoveCompasso();
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
	}	
	
	
	public void Init()
	{
		adicionarNota = new NotaInfo();
	
		_compassos = new List<_CompassoEditor>( );
		
		for( int i =0; i < compassos; i++) 
		{
			_CompassoEditor c = new _CompassoEditor(linhas, colunas, this);
			_compassos.Add(c);
		}
	}
			
}
