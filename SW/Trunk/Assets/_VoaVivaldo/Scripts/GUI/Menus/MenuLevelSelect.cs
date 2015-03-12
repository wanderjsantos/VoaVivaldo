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

	public void SelectFase( int numero )
	{
		Debug.Log ("Select Fase: " + numero);
		gMusica.s.SetMusica (numero);
		gMenus.s.ShowMenu ("Instrumento");
	}
	
	public void OnClickHome()
	{
		gMenus.s.ShowMenu("Principal");
	}

	void AtualizarTema (GameObject centeredObject)
	{
//  		BotaoFase botao = centeredObject.GetComponent<BotaoFase>();
  		
  		gTemas.s.Aplicar( );
	}
}
