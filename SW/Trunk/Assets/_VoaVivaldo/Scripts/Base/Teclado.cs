using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tecla
{
	public KeyCode	key;
	public Timbre	nota;

	public int compasso 	= -1;
	public int batida	 	= -1;

	[HideInInspector]
	public float	inicio;
	[HideInInspector]
	public float	fim;
	[HideInInspector]
	public float	duracao;

}

