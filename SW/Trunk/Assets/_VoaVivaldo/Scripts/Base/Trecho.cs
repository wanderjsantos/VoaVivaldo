using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Trecho
{
	public int				compassoInicial;
	public List<Compasso>	compassos;
	
	public Trecho()
	{
		compassos = new List<Compasso>();
	}
}

[System.Serializable]
public class Compasso
{
	public List<NotaInfo> 	notas;
	
	public Compasso()
	{
		notas = new List<NotaInfo>();
	}
}

