using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuLevelSelect : Menu {

	public int levelToSelect = 1;
	
	public UICenterOnChild uiCenter;
	
	List<Level> levelsDisponiveis;
	public List<BotaoFase> botoesDeLevel;
	
	public override void Show ()
	{
		base.Show ();
		levelsDisponiveis = new List<Level>();
		
		levelsDisponiveis.AddRange( gLevels.s.GetLevelsLiberados() );
		
		for( int i = 0; i< botoesDeLevel.Count; i++ )
		{
			botoesDeLevel[i].SetValue( false, false );
		}
		
		levelsDisponiveis.ForEach( delegate( Level level)
		{
			botoesDeLevel.Find( e=> e.selecionarLevel == level.savedInfo.meuLevel ).SetValue( level.savedInfo.liberado, level.savedInfo.festaLiberada);
		});

	}
	
	public void OnEnable()
	{
		uiCenter.onCenter += AtualizarTema;
	}
	
	public void OnDisable()
	{
		uiCenter.onCenter -= AtualizarTema;
	}
	
	public override void Resetar ()
	{
		base.Resetar ();
		levelToSelect = 1;
	}

	public void SelectLevel( int numero )
	{
		Debug.Log ("Select Partitura: " + numero);
		gMusica.s.SetMusica( numero );
		gMenus.s.ShowMenu ("Instrumento");
	}
	
	public void OnClickFesta(int level)
	{
		gMusica.s.SetMusica( level );
		gLevels.s.SetLevel( level );
		
		gMenus.s.ShowMenu("Festa");
	}
	
	public void OnClickHome()
	{
		gMenus.s.ShowMenu("Principal");
	}
	

	void AtualizarTema (GameObject centeredObject)
	{
		BotaoFase level = centeredObject.GetComponentInChildren<BotaoFase>();
	
		Debug.Log("AtualizarTema: " + centeredObject.name + " level: "  + level.selecionarLevel);
										
		if( gLevels.s.allLevels[ level.selecionarLevel ].savedInfo.liberado )
			gTemas.s.Aplicar( gLevels.s.allLevels[ level.selecionarLevel ].info.tema );
		else
			gTemas.s.Aplicar( gTemas.s.temaDisabled );
	}
}
