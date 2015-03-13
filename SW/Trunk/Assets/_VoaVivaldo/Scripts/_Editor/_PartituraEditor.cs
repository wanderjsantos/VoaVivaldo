using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class _PartituraEditor : VivaldoEditor
{
	
	public PartituraInfo 				partituraInfo;
	public List<_CompassoEditor> 		compassos;
	
	public _PartituraEditor()
	{
		partituraInfo = new PartituraInfo();
		compassos = new List<_CompassoEditor>();
	}

				
	void AddCompasso()
	{
		_CompassoEditor c = new _CompassoEditor(  );
		compassos.Add( c );		
	}
	
	void RemoveCompasso()
	{
		if( compassos.Count == 0 ) return;		
		compassos.RemoveAt( compassos.Count-1);		
	}

	void DuplicarCompasso (_CompassoEditor aSerDuplicado)
	{
		if( compassos.Count == 0 )return;
		_CompassoEditor c = new _CompassoEditor();
		
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
		compassos.Add(c);
		
	}
	
	
	public void Draw()
	{
		DrawNormal();		
		DrawComandos();
	}

	void DrawNormal ()
	{
		foldout = EditorGUILayout.Foldout( foldout, partituraInfo.nome );
		if( foldout == false ) return;		
		
		partituraInfo.nome = DrawNome( partituraInfo.nome );
		
		///BPM
		partituraInfo.BPM = EditorGUILayout.IntField( "BPM:", partituraInfo.BPM );
		
		//PERSONAGEM
		partituraInfo.personagem = (QualPersonagem) EditorGUILayout.EnumPopup( "Personagem:", partituraInfo.personagem );
		
		//////AUDIOS
		partituraInfo.clipAudioBase = (AudioClip) EditorGUILayout.ObjectField("Base:", partituraInfo.clipAudioBase, typeof(AudioClip), false );
		if(partituraInfo.clipAudioBase != null )
			partituraInfo.nomeAudioBase = partituraInfo.clipAudioBase.name;	
		
		
		partituraInfo.clipAudioInstrumento = (AudioClip) EditorGUILayout.ObjectField("Instrumento:", partituraInfo.clipAudioInstrumento, typeof(AudioClip), false );
		if(partituraInfo.clipAudioInstrumento != null )
			partituraInfo.nomeAudioInstrumento = partituraInfo.clipAudioInstrumento.name;
		
		
		scroll = EditorGUILayout.BeginScrollView( scroll );
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.HelpBox("Shift + Click ou Click: Aumenta/diminui o valor da nota. \n " +
		                        "Control + Click: Exclui a nota \n" +
		                        "Alt + Click: Muda o tipo da nota", MessageType.Info );
		EditorGUILayout.EndHorizontal();
		
		for( int i =0 ; i < compassos.Count; i++ )
		{		
			EditorGUILayout.BeginHorizontal();
				compassos[i].Draw();
			EditorGUILayout.EndHorizontal();
		}
		
		EditorGUILayout.EndScrollView();
	}
	
	void DrawComandos ()
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
			DuplicarCompasso(compassos[compassos.Count-1]);
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
	
	
			
}
