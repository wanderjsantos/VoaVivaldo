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
	public float			valorParaResetar = 7.5f;



	public UIRoot		uiRoot;

	void IniciarRodagem ()
	{
		rodando = true;
	}

	public override void Show()
	{
		base.Show ();
//		Debug.Break ();
//		tamanho = pistaBase.localSize.y;
	}

	public void Update()
	{
		if (gMusica.s.musicaAtual.isPlaying == false)
		return;

		velocidadeDaPista = - ((gPista.s.tamanhoDoCompasso * uiRoot.transform.localScale.x) / (gRitmo.s.intervalo * gRitmo.s.batidasPorCompasso));
		posTemp = rootDasNotas.transform.position;
		posTemp.x += velocidadeDaPista * Time.deltaTime;

		rootDasNotas.transform.position = posTemp;

	}


}
