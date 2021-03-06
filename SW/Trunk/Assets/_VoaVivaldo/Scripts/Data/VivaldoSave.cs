﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class VivaldoSave 
{
	public List<LevelSaveInfo> 		savedLevels;
	public SettingsSaveInfo 		settings;
}

[System.Serializable]
public class LevelSaveInfo
{
	public string 				nome;
	public int					meuLevel;
	public bool					liberado 			= false;
	public bool 				festaLiberada		= false;

 
	public List<PartituraSaveInfo> 	partiturasConcluidas;
}

[System.Serializable]
public class PartituraSaveInfo
{
	public int		meuIndice 			= -1;
	public int 		estrelasGanhas;
	public int 		pontosMarcados;
	public bool		liberado			= false;
//	public bool		festaLiberada		= false;
	
	
}

[System.Serializable]
public class SettingsSaveInfo
{
	public bool		audio;
	public bool		vibrar;
	
	public float	volumeGeral = 1f;
	public float 	volumeBase 	= 1f;
	public float 	volumeInstrumentos = 1f;
	
	//Sao usados temporariamente para guardar os valores antes de 
//	[HideInInspector]
//	public float savedVolumeBase = 1f;
//	[HideInInspector]
//	public float savedVolumeInstrumento = 1f;
//	[HideInInspector]
//	public float savedVolumeGeral = 1f;
}

