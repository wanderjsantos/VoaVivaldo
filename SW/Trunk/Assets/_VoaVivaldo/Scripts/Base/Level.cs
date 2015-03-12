using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour 
{
	public LevelInfo mInfo;		
}

[System.Serializable]
public class LevelInfo
{
	public string 				nome = "Level";
	public List<MusicaData> 	dadosDaMusica;
	public Tema					tema;
	
	public LevelInfo()
	{
		tema = new Tema();
	}
}
