using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour 
{
	public LevelInfo mInfo;		
}

[System.Serializable]
public class LevelInfo
{
	public string 		nome;
	public MusicaData 	dadosDaMusica;
	public Color		corBackground;
	public Color		corElementosClaros;
	public Color		corElementosEscuros;
	public Color		corTextos;
}
