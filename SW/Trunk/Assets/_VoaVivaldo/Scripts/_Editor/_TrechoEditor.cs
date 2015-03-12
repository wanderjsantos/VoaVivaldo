using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class _TrechoEditor : VivaldoEditor
{
	public TrechoInfo				info;
	public List<_CompassoEditor> 	_compassos;	
	public NotaInfo 				adicionarNota;
	
	public _TrechoEditor()
	{
		info = new TrechoInfo();
		Init();
	}
			
	public void AddCompasso()
	{
		_CompassoEditor c = new _CompassoEditor( info.linhas, info.colunas, this );
		_compassos.Add( c );
		
	}
	
	public void RemoveCompasso()
	{
		if( _compassos.Count == 0 ) return;
		
		_compassos.RemoveAt( _compassos.Count-1);
		
	}
	
	public void RemoveCompasso(_CompassoEditor compasso)
	{
		if( _compassos.Contains( compasso ) == false ) return;
		
		_compassos.Remove(compasso);
		
	}

	public void DuplicarCompasso (_CompassoEditor aSerDuplicado)
	{
		if( _compassos.Count == 0 )return;
		_CompassoEditor c = new _CompassoEditor(info.linhas, info.colunas, this);
		
		foreach( _NotaEditor n in aSerDuplicado.notas )
		{
			_NotaEditor n1 = new _NotaEditor();
			NotaInfo ni = new NotaInfo();
			
			n1.notaInfo = ni;
			ni.batida = n.notaInfo.batida;
			ni.compasso = n.notaInfo.compasso +1;
			ni.duracao = n.notaInfo.duracao;
			ni.timbre = n.notaInfo.timbre;
			
			c.notas.Add( n1 );
		}
		
		_compassos.Add(c);
		
	}
	
	
	public void Draw()
	{
		foldout = EditorGUILayout.Foldout( foldout, info.nome );
		if( foldout == false ) return;		
		
		info.nome = DrawNome( info.nome );
	
		scroll = EditorGUILayout.BeginScrollView( scroll );
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.HelpBox("Shift + Click ou Click: Aumenta/diminui o valor da nota. \n " +
		                                "Control + Click: Exclui a nota \n" +
		                                "Alt + Click: Muda o tipo da nota", MessageType.Info );
		EditorGUILayout.EndHorizontal();
		
		for( int i =0 ; i < _compassos.Count; i++ )
		{		
			EditorGUILayout.BeginHorizontal();
			if( _compassos[i].trecho == null ) _compassos[i].trecho = this;					
			_compassos[i].Draw();
			_compassos[i].DrawComandos();
			EditorGUILayout.EndHorizontal();
		}
		
		EditorGUILayout.EndScrollView();
		
		DrawComandos();
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
	
	string DrawNome (string nome)
	{
		return EditorGUILayout.TextField ("Nome da Musica:",nome);
	}
	
	public void Init()
	{
		adicionarNota = new NotaInfo();
	
		_compassos = new List<_CompassoEditor>( );
		
		for( int i =0; i < info.compassos; i++) 
		{
			_CompassoEditor c = new _CompassoEditor(info.linhas, info.colunas, this);
			_compassos.Add(c);
		}
	}
			
}
