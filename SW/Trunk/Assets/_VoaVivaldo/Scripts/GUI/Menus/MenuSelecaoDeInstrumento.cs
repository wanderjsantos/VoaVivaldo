using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSelecaoDeInstrumento : Menu 
{
	public bool 			iniciarGame = false;
	public UIButton			buttonIniciarGame;
	
	public UIGrid			grid;
	public GameObject		botoesEscondidos;
//	public UIButton			botaoInstrumento;
	public List<UIButton>	botoesDeInstrumentos;

	public override void Show ()
	{
		base.Show ();
		buttonIniciarGame.isEnabled = false;
		PrepararBotoes();
	}
	
	public override void Resetar ()
	{
		base.Resetar ();
		buttonIniciarGame.isEnabled = false;
		DescartarBotoes();
		
	}
	
	public void PrepararBotoes()
	{
//		botoesDeInstrumentos = new List<UIButton>();
//		Debug.Log(gLevels.s.GetLevel( gMusica.s.musicaIndice ).mInfo.dadosDaMusica.partituras);
		Level levelAtual = gLevels.s.GetLevel( gMusica.s.musicaIndice );
		
		for( int i = 0 ; i < botoesDeInstrumentos.Count ; i++ )
		{
			botoesDeInstrumentos[i].transform.parent = botoesEscondidos.transform;
		}
		
		foreach( Partitura p in levelAtual.mInfo.dadosDaMusica.partituras )
		{
			if( levelAtual.mInfo.dadosDaMusica.partituras.Count > botoesDeInstrumentos.Count ) break;
			
			botoesDeInstrumentos[ levelAtual.mInfo.dadosDaMusica.partituras.IndexOf(p) ].transform.parent = grid.transform;
			botoesDeInstrumentos[ levelAtual.mInfo.dadosDaMusica.partituras.IndexOf(p) ].GetComponent<BotaoInstrumento>().selecionarInstrumento =
				levelAtual.mInfo.dadosDaMusica.partituras.IndexOf(p);
			
		}		
		
		grid.repositionNow = true;

	}
	
	public void DescartarBotoes()
	{
		for( int i = 0 ; i < botoesDeInstrumentos.Count ; i++ )
		{
			botoesDeInstrumentos[i].transform.parent = botoesEscondidos.transform;
		}
	}

	public void SelectInstrumento(int numero)
	{
		gMusica.s.SetInstrumento (numero);
		buttonIniciarGame.isEnabled = true;
	}

	public void IniciarPartida()
	{
		gGame.s.IniciarJogo ();
	}
}
