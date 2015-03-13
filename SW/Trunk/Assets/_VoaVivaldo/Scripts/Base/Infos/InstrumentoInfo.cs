using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InstrumentoInfo
{
	public	string					nome 	= "Instrumento";	
	public 	QualPersonagem			personagem = QualPersonagem.TRUMPET;	
	public 	string					audioBase;
	public 	AudioClip				clipAudioBase;
	public 	string	 				audioInstrumento;
	public 	AudioClip				clipAudioInstrumento;
	public 	List<Partitura>			trechos;

}