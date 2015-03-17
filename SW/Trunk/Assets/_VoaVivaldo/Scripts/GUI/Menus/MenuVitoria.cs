using UnityEngine;
using System.Collections;

public class MenuVitoria : Menu {

	public UI2DSprite spritePersonagem;
	
	vPersonagem personagem;

	public override void Show ()
	{
		base.Show ();
		
		gPontuacao.s.ForcarAtualizarPontosEstrelas();
		
		personagem = spritePersonagem.gameObject.GetComponent<vPersonagem>();
		personagem.meuPersonagem = gGame.s.player.vPlayer.meuPersonagem;
		
		personagem.gameObject.GetComponent<UI2DSpriteAnimation>().Play( personagem.meuPersonagem.ToString().ToLower() );
		spritePersonagem.MakePixelPerfect();
		spritePersonagem.MakePixelPerfect();
		
	}
	
	public void OnClickFestinha()
	{
		gMenus.s.ShowMenu("Festa");
	}
	
	public void OnClickMusicas()
	{
		gMenus.s.ShowMenu("Select");
	}

	public void OnClickContinuar()
	{
		gMenus.s.ShowMenu("Instrumento");
	}
	
	public void OnClickReiniciar()
	{
		gGame.s.IniciarJogo();
	}
}
