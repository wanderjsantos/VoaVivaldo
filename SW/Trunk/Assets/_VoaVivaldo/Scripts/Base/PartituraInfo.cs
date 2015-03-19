using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;

[System.Serializable]
public class PartituraInfo
{
	public 	string				nome		= "Partitura";
	
	public int					meuNumero 	= -1;
	
	public	int					BPM			= 120;
	
	public 	QualPersonagem		personagem = QualPersonagem.TRUMPET;	
	
	[NonSerialized()]
	public AudioClip			clipAudioBase;
	public string				nomeAudioBase;			
		
	[NonSerialized()]
	public AudioClip			clipAudioInstrumento;
	public string				nomeAudioInstrumento;
			
	public int					_compassos 	= Vivaldos.COMPASSOS_DEFAULT;
	public int					_linhas		= Vivaldos.LINHAS;
	public int					_colunas		= Vivaldos.COLUNAS;
		
	public 	List<Compasso>		compassos;
			
	public PartituraInfo()
	{
		compassos = new List<Compasso>();
	}

}





