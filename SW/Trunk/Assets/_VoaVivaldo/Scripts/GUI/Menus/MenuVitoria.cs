using UnityEngine;
using System.Collections;

public class MenuVitoria : Menu {


	public UISprite  sptEstrelas;
	public UILabel txtPontuacao;
	
	public override void Show ()
	{
		base.Show ();
		
		float fill = 0.35f;
		switch( gPontuacao.s.estrelasGanhas )
		{
		case 1:
			fill = .35f;
			break;
		case 2:
			fill = .65f;
			break;
		case 3:
			fill = 1f;
			break;
		default :
			break;
		}
		sptEstrelas.fillAmount = fill;
		
		txtPontuacao.text = gGame.s.player.mInfo.pontuacao.ToString();
	}

	public void OnClickContinuar()
	{
		gMenus.s.ShowMenu("Principal");
	}
	
	public void OnClickReiniciar()
	{
		gGame.s.IniciarJogo();
	}
}
