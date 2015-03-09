using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _MusicaEditor  
{
	public 	string 				nome 		= "Musica sem nome";
	public 	int					BPM			= 90;
	public	AudioClip			audioBase;
	
	public 	bool				foldout 	= false;

	public 	List<_InstrumentoEditor>	banda;
		
	public _MusicaEditor()
	{
		
		banda = new List<_InstrumentoEditor>();
		banda.Add(new _InstrumentoEditor());
	}
	
	
	_InstrumentoEditor NewInstrumento()
	{
		return new _InstrumentoEditor();
	}
	
	public void AddInstrumento()
	{
		banda.Add( NewInstrumento() );
	}
	
	public void RemoveInstrumento()
	{
		if( banda.Count > 1 )
			banda.RemoveAt( banda.Count-1 );
	}
	
}
