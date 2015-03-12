using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class _InstrumentoEditor : VivaldoEditor
{
	public InstrumentoInfo 			info;	
	public	List<_TrechoEditor>		trechos;
	
	public _InstrumentoEditor()
	{
		info = new InstrumentoInfo();
		trechos = new List<_TrechoEditor>();
		trechos.Add( NewTrecho() );
	}
	_TrechoEditor NewTrecho ()
	{
		return new _TrechoEditor();
	}
	
	
	public void Draw ()
	{
		foldout = EditorGUILayout.Foldout( foldout, info.nome );
		if( foldout == false ) return;
		
		info.nome = DrawNome ( info.nome );
		info.clipAudioBase 		= DrawAudioClip("Base:", info.clipAudioBase	 );
		info.clipAudioInstrumento = DrawAudioClip("Instrumento:", info.clipAudioInstrumento );
		
		info.personagem = (QualPersonagem) EditorGUILayout.EnumPopup("Personagem:", info.personagem );
		
		scroll = EditorGUILayout.BeginScrollView( scroll );
		foreach( _TrechoEditor trecho in trechos )
		{
			trecho.Draw();
		}
		EditorGUILayout.EndScrollView();
		
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Trecho: ");
		GUI.color = Color.green;
		if( GUILayout.Button("+") )
		{
			AddTrecho();
		}
		
		GUI.color = Color.cyan;
		if( GUILayout.Button("||") )
		{
			DuplicarTrecho();
		}
		
		GUI.color = Color.red;
		if( GUILayout.Button("-") )
		{
			RemoveTrecho();
		}
		GUI.color = Color.white;
		EditorGUILayout.EndHorizontal();
		
	}
	
	AudioClip DrawAudioClip (string label, AudioClip clip)
	{
		return EditorGUILayout.ObjectField (label,clip, typeof(AudioClip), false) as AudioClip;
	}
	
	string DrawNome (string nome)
	{
		return EditorGUILayout.TextField ("Nome da Musica:",nome);
	}
	
	int DrawBPM (int valor)
	{
		return EditorGUILayout.IntField ("Batidas por minuto (BPM):", valor);
	}
	
	
	
	public void AddTrecho()
	{
		trechos.Add( NewTrecho() );
	}
	
	public void RemoveTrecho()
	{
		if( trechos.Count > 1 )
			trechos.RemoveAt( trechos.Count-1 );
	}

	_CompassoEditor[] c ;
	public void DuplicarTrecho ()
	{
		if( trechos.Count == 0 ) return;
		
		_TrechoEditor t = NewTrecho();
		_TrechoEditor aSerCopiado = trechos[trechos.Count-1];
		
		t.info.compassos = aSerCopiado.info.compassos;
		c = new _CompassoEditor[aSerCopiado._compassos.Count];
		aSerCopiado._compassos.CopyTo( c );
		t._compassos.AddRange( c );
		t.info.linhas = aSerCopiado.info.linhas;
		
		trechos.Add( t );
		
	}
	
	public List<NotaInfo> ConverterTrechosParaNotas()
	{
		List<NotaInfo> ret = new List<NotaInfo>();
		foreach( _TrechoEditor trecho in trechos )
		{
			foreach( _CompassoEditor compasso in trecho._compassos )
			{
				foreach( _NotaEditor nota in compasso.notas )
				{
					if( (int) nota.notaInfo.timbre <= 0 ) continue;	
					
					NotaInfo info = new NotaInfo() ;
					info = nota.notaInfo;
					
					ret.Add( info );				
				}
			}
		}
		
		Debug.Log("Adicionando " + ret.Count + " notas");
		
		return ret;
	}
	
}
