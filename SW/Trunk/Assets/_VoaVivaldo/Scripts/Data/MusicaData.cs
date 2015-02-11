using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class MusicaData 
{
	public int 				BPM;
	public string 			audioBase;

	public List<string>		audioInstrumentos;
//	public string 			audioInstrumento;

	public List<NotaInfo> 	notas;
}
