using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Partitura : MonoBehaviour
{
	public PartituraInfo		info;
	
	public void Awake()
	{
		info = new PartituraInfo();
	}
	
	void Start()
	{
		info.clipAudioBase 			= Vivaldos.NameToAudioClip( info.nomeAudioBase );
		info.clipAudioInstrumento 	= Vivaldos.NameToAudioClip( info.nomeAudioInstrumento );
	}
}



