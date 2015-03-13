using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Instrumento
{
	public InstrumentoInfo 		info;
	
	public Instrumento()
	{
		info = new InstrumentoInfo();
		info.trechos = new List<Partitura>();
	}
	
	public List<NotaInfo> TodasAsNotas()
	{
		List<NotaInfo> ret = new List<NotaInfo>();
		
		foreach( Partitura trecho in info.trechos )
		{
			foreach( Compasso compasso in trecho.info.compassos )
			{
				ret.AddRange( compasso.info.notas );
			}
		}
		
		return ret;
	}
}
