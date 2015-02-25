using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSelecaoDeInstrumento : Menu 
{
	public bool 			iniciarGame = false;
	
	public UIButton			buttonIniciarGame;
	
	public UIGrid					grid;
	public GameObject				botoesEscondidos;
	public List<BotaoInstrumento>	botoesDeInstrumentos;

	public override void Show ()
	{
		base.Show ();
		buttonIniciarGame.gameObject.SetActive(false);
		
		botoesDeInstrumentos = new List<BotaoInstrumento>();
		botoesDeInstrumentos.AddRange( botoesEscondidos.GetComponentsInChildren<BotaoInstrumento>(true) as BotaoInstrumento[]);
		
		PrepararBotoes();
	}
	
	public override void Resetar ()
	{
		base.Resetar ();
		buttonIniciarGame.gameObject.SetActive(false);
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
		
		foreach( Instrumento p in levelAtual.mInfo.dadosDaMusica.instrumentos )
		{
			if( levelAtual.mInfo.dadosDaMusica.instrumentos.Count > botoesDeInstrumentos.Count ) break;
			BotaoInstrumento btn = botoesDeInstrumentos[ levelAtual.mInfo.dadosDaMusica.instrumentos.IndexOf(p) ];
			btn.transform.parent = grid.transform;
			btn.GetComponent<BotaoInstrumento>().selecionarInstrumento = levelAtual.mInfo.dadosDaMusica.instrumentos.IndexOf(p);
			
			btn.ChangeEstado(BotaoInstrumento.Estado.DISPONIVEL);
					
			
			
		}		
		
		grid.repositionNow = true;

	}
	
	public void DescartarBotoes()
	{
		for( int i = 0 ; i < botoesDeInstrumentos.Count ; i++ )
		{
			botoesDeInstrumentos[i].transform.parent = botoesEscondidos.transform;
			botoesDeInstrumentos[i].mToggle.Set(false);
		}
	}

	public void SelectInstrumento(int numero)
	{
		gMusica.s.SetInstrumento (numero);
		buttonIniciarGame.gameObject.SetActive(true);
	}

	public void IniciarPartida()
	{
		gGame.s.IniciarJogo ();
	}
	
	public void OnClickVoltar()
	{
		gMenus.s.ShowMenu("SelecaoDeMusica");
	}
}
