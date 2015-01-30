using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gRecord : MonoBehaviour
{
	public Gravador gravador;
	public bool recordOnPlayMusic = false;
	public static gRecord s;
	void Awake()
	{
		s = this;
	}
	void OnEnable()
	{
		gComandosDeMusica.onPlay += PlayRecord;
		gComandosDeMusica.onStop += StopRecord;
	}
	
	void OnDisable()
	{
		gComandosDeMusica.onPlay -= PlayRecord;
		gComandosDeMusica.onStop -= StopRecord;
	}

	void PlayRecord ()
	{
		if (recordOnPlayMusic == false)
						return;
		gravador.Gravar ();
	}

	void StopRecord ()
	{

		gravador.Parar ();
	}
}
