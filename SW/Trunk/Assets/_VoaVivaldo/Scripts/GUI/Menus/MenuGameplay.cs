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

	public UIRoot		uiRoot;
	
	public UILabel		txtPontuacao;
	public string		pontuacao;

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
		if ( gGame.s.gameStarted == false)
		return;

		velocidadeDaPista = - ((gPista.s.tamanhoDoCompasso * uiRoot.transform.localScale.x) / (gRitmo.s.intervalo * gRitmo.s.batidasPorCompasso));
		posTemp = rootDasNotas.transform.position;
		posTemp.x += velocidadeDaPista * Time.deltaTime;

		rootDasNotas.transform.position = posTemp;
		
		txtPontuacao.text = pontuacao + gGame.s.player.mInfo.pontuacao.ToString();

	}


}
