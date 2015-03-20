using UnityEngine;
using System.Collections;

public class MenuVitoria : Menu {

	public UI2DSprite spritePersonagem;
	
	public GameObject	botaoContinuar;
	
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
		
		if( gPontuacao.s.estrelasGanhas == 0 ) botaoContinuar.SetActive(false);
		else botaoContinuar.SetActive(true);
		
	}
	
	public void OnClickFestinha()
	{
		gMenus.s.ShowMenu("Festa");
	}
	
	public void OnClickMusicas()
	{
		gMenus.s.ShowMenu("Musica");
	}

	public void OnClickContinuar()
	{
//		gMenus.s.ShowMenu("Instrumento");
		gLevels.s.ProximaFase( gLevels.s.currentPartituraIndex );
	}
	
	public void OnClickReiniciar()
	{
		gGame.s.IniciarJogo();
	}
}
