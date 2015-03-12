using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Trecho
{
	public TrechoInfo		info;
	
	public Trecho()
	{
		info = new TrechoInfo();
	}
}


[System.Serializable]
public class Compasso
{
	public CompassoInfo info;
	
	public Compasso()
	{
		info = new CompassoInfo();
	}
}

