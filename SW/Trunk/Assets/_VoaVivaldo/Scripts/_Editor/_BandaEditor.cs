using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _InstrumentoEditor 
{
	public	string					nome 	= "Instrumento";
	public	bool					foldout = false;
	public 	Vector2					scroll;

	public 	AudioClip 				audio;
	public	List<_TrechoEditor>		trechos;
	
	public _InstrumentoEditor()
	{
		
		trechos = new List<_TrechoEditor>();
		trechos.Add( NewTrecho() );
	}

	_TrechoEditor NewTrecho ()
	{
		return new _TrechoEditor();
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
	
	public List<NotaInfo> ConverterTrechosParaNotas()
	{
		List<NotaInfo> ret = new List<NotaInfo>();
		foreach( _TrechoEditor t in trechos )
		{
//			foreach( _LinhaDeNotasEditor n in t.notas )
//			{
//				if( n.currentTimbre <= 0 ) continue;	
//				
//				NotaInfo info = new NotaInfo();
//				info.batida = n.batida;
//				info.compasso = n.compasso;
////				info.extra		= n.extra;
//				info.timbre = (Timbre) n.currentTimbre;
//				
//				ret.Add( info );
//			}
		}
		
		Debug.Log("Adicionando " + ret.Count + " notas");
		
		return ret;
	}
	
}
