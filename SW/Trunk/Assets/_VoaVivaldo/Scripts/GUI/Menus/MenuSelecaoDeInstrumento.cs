using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuSelecaoDeInstrumento : Menu 
{
	public List<GameObject> listaDeBotoesPorFase;
	public List<vLevel>		listaDeFasesPorLevel;

	public override void Show ()
	{
		base.Show ();
		
		DesabilitarBotoes();
		HabilitarBotoes( gMusica.s.indiceMusica );
	}

	void DesabilitarBotoes ()
	{
		listaDeBotoesPorFase.ForEach( delegate( GameObject e )
		{
			e.SetActive(false);
		});
	
	}
	
	void HabilitarBotoes( int indiceDoLevel )
	{
		listaDeBotoesPorFase[indiceDoLevel].SetActive(true);
		
		listaDeFasesPorLevel = new List<vLevel>();
		listaDeFasesPorLevel.AddRange( listaDeBotoesPorFase[indiceDoLevel].GetComponentsInChildren<vLevel>(true));
	
		foreach(vLevel v in listaDeFasesPorLevel )
		{ v.gameObject.SetActive(false); }
	
		foreach( PartituraSaveInfo partitura in gLevels.s.allLevels[indiceDoLevel].savedInfo.partiturasConcluidas )
		{
//			Debug.Log("Partitura liberada: " + partitura.liberado);
		
			if( partitura.liberado == false ) continue;
		
			vLevel level = listaDeFasesPorLevel.Find( e => e.minhaFase == partitura.meuIndice );
			if( level == null ) continue;
			
			level.SetActive( true, partitura.estrelasGanhas );
		}
		
	
	}
	
	public void SelectInstrumento(int numero)
	{
		gMusica.s.SetFase(numero );
		gLevels.s.SetLevel( gMusica.s.indiceMusica, gMusica.s.indiceFase );		
		gGame.s.IniciarJogo();
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
