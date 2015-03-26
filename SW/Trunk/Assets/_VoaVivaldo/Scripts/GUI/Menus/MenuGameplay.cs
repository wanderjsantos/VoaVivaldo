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

	public UILabel				labelContagemRegressiva;
	public GameObject			goContagemRegressiva;
	public vPersonagensDancando	personagensDancantes;

	public override void Show()
	{
		base.Show ();
		gGame.onPlayGame += ResetContagemRegressiva;
		InitContagemRegressiva();
		VerificarPersonagensDancando();
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
		labelContagemRegressiva.text = (gGame.s.contagemRegressiva + 1).ToString ("00");
	}

	public void ResetContagemRegressiva()
	{
		goContagemRegressiva.SetActive (false);
		labelContagemRegressiva.text = (gGame.s.contagemRegressiva + 1).ToString ("00");
	}

	void VerificarPersonagensDancando ()
	{
		personagensDancantes.DesativarTodos();
		
		foreach(QualPersonagem personagem in gLevels.s.GetPersonagensDoLevelAtual() )
		{
			if( personagem == gGame.s.player.mInfo.meuPersonagem ) continue;
			
			personagensDancantes.Ativar( personagem );
		}
	}
	
	public void OnClickPause()
	{
		gGame.s.Pause( true );
	}

	public void Update()
	{
		if (goContagemRegressiva.activeInHierarchy == true) 
		{
			int tempo = gGame.s.tempoParaIniciar +1;
			labelContagemRegressiva.text = Mathf.Clamp( tempo, 1, 100).ToString ("00");
		}

		if (gMusica.s.musicaAtual.isPlaying == false)
		return;
//		txtPontuacao.text = "pts " + gGame.s.player.mInfo.pontuacao;
		
		velocidadeDaPista = - ((gPista.s.tamanhoDoCompasso * uiRoot.transform.localScale.x) / (gRitmo.s.intervalo * gRitmo.s.batidasPorCompasso));
		posTemp = rootDasNotas.transform.position;
		posTemp.x += velocidadeDaPista * Time.deltaTime;

		rootDasNotas.transform.position = posTemp;		

	}
}
