using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSelecaoDeInstrumento : Menu 
{
	public List<GameObject> listaDeBotoes;

	public override void Show ()
	{
		base.Show ();
		
		DesabilitarBotoes();
		HabilitarBotoes( gMusica.s.indiceMusica );
	}

	void DesabilitarBotoes ()
	{
		listaDeBotoes.ForEach( delegate( GameObject e )
		{
			e.SetActive(false);
		});
	
	}
	
	void HabilitarBotoes( int index )
	{
		listaDeBotoes[index].SetActive(true);
	}
	
	public void SelectInstrumento(int numero)
	{
		gMusica.s.SetFase (numero);
		
		gLevels.s.SetLevel( gMusica.s.indiceMusica, gMusica.s.indiceFase );
		
		IniciarPartida();
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
