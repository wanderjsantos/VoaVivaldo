using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MusicaDataInfo
{
	public string				nome = "Musica sem nome";
	public int 					BPM = 120;
	public List<Instrumento> 	instrumentos;
	
	public MusicaDataInfo()
	{
		instrumentos = new List<Instrumento> ();
	}
}
