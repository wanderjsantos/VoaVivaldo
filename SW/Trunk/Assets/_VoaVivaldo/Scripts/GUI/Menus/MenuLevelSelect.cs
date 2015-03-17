using UnityEngine;
using System.Collections;

public class MenuLevelSelect : Menu {

	public int levelToSelect = 1;
	
	public UICenterOnChild uiCenter;
	
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
	
	public void OnClickHome()
	{
		gMenus.s.ShowMenu("Principal");
	}
	
	public void OnClickFesta()
	{
		gMenus.s.ShowMenu("Festa");
	}

	void AtualizarTema (GameObject centeredObject)
	{
		BotaoFase level = centeredObject.GetComponent<BotaoFase>();
		gTemas.s.Aplicar( gLevels.s.allLevels[ level.selecionarLevel ].info.tema );
	}
}
