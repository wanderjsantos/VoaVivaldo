﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LevelInfo
{
	public string 				nome = "Level";
	public int					meuIndice = -1;
	public Partitura[]			partituras;
	public Tema					tema;
		
	
	public LevelInfo()
	{
		tema = new Tema();
		partituras = new Partitura[0];
	}
}
