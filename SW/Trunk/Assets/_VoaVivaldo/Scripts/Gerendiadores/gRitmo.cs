using UnityEngine;
using System.Collections;

public class gRitmo : MonoBehaviour 
{
	public static gRitmo s;
	public bool useBeep = false;
	public int BPM = 60;

	public int batidas;
	public int contagem;

	public float intervalo;
	float tempoDeInicioDaMusica;
	float tempoAtual;

	public int batidasPorCompasso 	= 4;
	public int seminima				= 4;
	public int compassoAtual		= 1;

	bool beating = false;

	void Awake()
	{
		s = this;
	}
	void OnEnable()
	{
		gComandosDeMusica.onPlay += OnPlayAudio;
		gComandosDeMusica.onStop += OnStopAudio;
	}
	void OnDisable()
	{
		gComandosDeMusica.onPlay -= OnPlayAudio;
		gComandosDeMusica.onStop -= OnStopAudio;
	}
	void Iniciar()
	{
		tempoDeInicioDaMusica = Time.realtimeSinceStartup;
		batidas = 0;
		beating = true;
		contagem = 1;
		compassoAtual = 1;
		Debug.Log (tempoDeInicioDaMusica + " " + Time.realtimeSinceStartup);
	}

	void Parar()
	{
		tempoDeInicioDaMusica = Time.realtimeSinceStartup;
		batidas = 0;
		beating = false;
	}

	void Update()
	{
		if (beating == false)
						return;

		intervalo = 60f / BPM;
		tempoAtual = Time.realtimeSinceStartup;

		if ((tempoDeInicioDaMusica + (intervalo * batidas)) <= tempoAtual) 
		{
			batidas ++;
			contagem ++;

			if( batidas > 0 && batidas % batidasPorCompasso == 0 )
			{
				contagem = 1;
				compassoAtual++;
				gComandosDeMusica.s.OnNovoCompasso(compassoAtual);
			}



			if( useBeep )
			{
				if( audio.isPlaying ) audio.Stop();
				audio.Play ();
			}
		}
	}


	void OnPlayAudio()
	{
		Iniciar ();
	}

	void OnStopAudio ()
	{
		Parar ();
	}
}
