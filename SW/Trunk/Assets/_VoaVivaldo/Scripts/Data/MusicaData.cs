using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class MusicaData 
{
	public MusicaDataInfo info;
	public MusicaData()
	{
		info = new MusicaDataInfo();
	}
}

[System.Serializable]
public class MusicaDataInfo
{
	public string				nome = "Musica sem nome";
	public int 					BPM = 120;
	public List<Instrumento> 	instrumentos;
	public Tema					tema;
	
	public MusicaDataInfo()
	{
		instrumentos = new List<Instrumento> ();
	}
}

[System.Serializable]
public class Instrumento
{
	public InstrumentoInfo 		info;
	public List<Trecho>			trechos;

	public QualPersonagem personagem ;
	public Instrumento()
	{
		
		trechos = new List<Trecho>();
	}
	
	public List<NotaInfo> TodasAsNotas()
	{
		List<NotaInfo> ret = new List<NotaInfo>();
		
		foreach( Trecho trecho in trechos )
		{
			foreach( Compasso compasso in trecho.info.lCompassos )
			{
				ret.AddRange( compasso.info.notas );
			}
		}
		
		return ret;
	}
}
