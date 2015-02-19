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
	public List<Trecho>			trechos;

	public MusicaData()
	{
		instrumentos = new List<Instrumento> ();
		trechos = new List<Trecho>();
	}
}

[System.Serializable]
public class Instrumento
{
	public string			audioInstrumentos;
	public List<NotaInfo> 	notas;

	public Instrumento()
	{
		notas = new List<NotaInfo> ();
	}
}

//[System.Serializable]
//public class Trecho
//{
//	
//}


