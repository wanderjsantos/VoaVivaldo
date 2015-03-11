using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class MusicaData 
{
	public string				nome;
	public int 					BPM;
	public string 				audioBase;
	public List<Instrumento> 	instrumentos;
	public List<NotaInfo>		todasAsNotas;

	public MusicaData()
	{
		instrumentos = new List<Instrumento> ();
	}
}

[System.Serializable]
public class Instrumento
{
	public string			audioInstrumentos;
	public List<Trecho>		trechos;

	public Instrumento()
	{
		trechos = new List<Trecho>();
	}
	
	public List<NotaInfo> TodasAsNotas()
	{
		List<NotaInfo> ret = new List<NotaInfo>();
		
		foreach( Trecho trecho in trechos )
		{
			foreach( Compasso compasso in trecho.compassos )
			{
				ret.AddRange( compasso.notas );
			}
		}
		
		return ret;
	}
}
