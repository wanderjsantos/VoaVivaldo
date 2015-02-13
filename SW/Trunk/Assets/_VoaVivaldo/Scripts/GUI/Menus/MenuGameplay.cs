using UnityEngine;
using System.Collections;


public class MenuGameplay : Menu 
{
//	public float		tamanho = 0f;
	public UISprite		pistaBase;
	public float		velocidadeDaPista = 30f;
	public GameObject	rootDasNotas;
	public GameObject	rootCompassos;
	Vector3 posTemp;
	public bool			rodando = false;
	public float		valorParaResetar = 7.5f;

	public UIRoot		uiRoot;

	public UILabel		labelContagemRegressiva;
	public GameObject	goContagemRegressiva;

	public override void Show()
	{
		base.Show ();
		gGame.onPlayGame += ResetContagemRegressiva;
		InitContagemRegressiva();
	}
	
	public override void Resetar ()
	{
		base.Resetar ();
		labelContagemRegressiva.text = "00";
		rodando = false;
		
	}

	public override void Hide ()
	{
		base.Hide ();
		Resetar();
		gGame.onPlayGame -= ResetContagemRegressiva;
	}

	public void InitContagemRegressiva()
	{
		goContagemRegressiva.SetActive (true);
		labelContagemRegressiva.text = gGame.s.contagemRegressiva.ToString ("00");
	}

	public void ResetContagemRegressiva()
	{
		goContagemRegressiva.SetActive (false);
		labelContagemRegressiva.text = gGame.s.contagemRegressiva.ToString ("00");
	}

	public void Update()
	{
		if (goContagemRegressiva.activeInHierarchy == true) 
		{
			labelContagemRegressiva.text = gGame.s.tempoParaIniciar.ToString ("00");
		}

		if (gMusica.s.musicaAtual.isPlaying == false)
		return;

		velocidadeDaPista = - ((gPista.s.tamanhoDoCompasso * uiRoot.transform.localScale.x) / (gRitmo.s.intervalo * gRitmo.s.batidasPorCompasso));
		posTemp = rootDasNotas.transform.position;
		posTemp.x += velocidadeDaPista * Time.deltaTime;

		rootDasNotas.transform.position = posTemp;

	}
}
