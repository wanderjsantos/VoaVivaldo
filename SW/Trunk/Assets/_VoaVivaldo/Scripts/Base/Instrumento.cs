using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InstrumentosInfo 
{
	public string		nome;
	public int			instrumentoAtual;
	public AudioClip	baseMusica;
	public AudioClip 	instrumento;
	public List<AudioClip> todosOsInstrumentos;

	public InstrumentosInfo()
	{
		todosOsInstrumentos = new List<AudioClip> ();
	}



}
