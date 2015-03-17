using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VivaldoSave 
{
	public static List<LevelSaveInfo> 		savedLevels;
	public static List<SettingsSaveInfo> 	settings;
}

[System.Serializable]
public class LevelSaveInfo
{
	public string 				nome;
	public int					meuLevel;
	public bool					liberado 			= false; 
	public List<PartituraSaveInfo> 	partiturasConcluidas;
}

[System.Serializable]
public class PartituraSaveInfo
{
	public int		meuIndice 			= -1;
	public int 		estrelasGanhas;
	public int 		pontosMarcados;
	public bool		festaLiberada		= false;
}

public class SettingsSaveInfo
{
	public bool		audio;
	public bool		vibrar;
}

