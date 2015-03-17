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
		
//		LevelSaveInfo savedInfo;
		
//		if( gLevels.s.currentLevel.info.savedInfo == null )
//			gLevels.s.currentLevel.info.savedInfo = new LevelSaveInfo();
//		
//		savedInfo = gLevels.s.currentLevel.info.savedInfo;
//		
//		savedInfo.estrelasGanhas = gPontuacao.s.estrelasGanhas;
//		savedInfo.pontosMarcados = gGame.s.player.mInfo.pontuacao;
//		
		
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
