using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class MusicaData 
{
	public int 				BPM;
	public string 			audioBase;
	public List<Partitura> 	partituras;

	public MusicaData()
	{
		partituras = new List<Partitura> ();
	}
}

[System.Serializable]
public class Partitura
{
	public string			audioInstrumentos;
	public List<NotaInfo> 	notas;

	public Partitura()
	{
		notas = new List<NotaInfo> ();
	}
}

