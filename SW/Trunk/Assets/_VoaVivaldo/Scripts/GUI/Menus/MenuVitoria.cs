using UnityEngine;
using System.Collections;

public class MenuVitoria : Menu {

	public UI2DSprite spritePersonagem;
	
	public GameObject	botaoContinuar;
	public GameObject	botaoFesta;
	
	public string		textoTV;
	public UILabel		codigoTV;
	
	vPersonagem personagem;

	public override void Show ()
	{
		base.Show ();
		
		personagem = spritePersonagem.gameObject.GetComponent<vPersonagem>();
		personagem.meuPersonagem = gGame.s.player.vPlayer.meuPersonagem;
		
		personagem.gameObject.GetComponent<UI2DSpriteAnimation>().Play( personagem.meuPersonagem.ToString().ToLower() );
//		spritePersonagem.MakePixelPerfect();
//		spritePersonagem.MakePixelPerfect();
		
		if( gPontuacao.s.estrelasGanhas == 0 ) botaoContinuar.SetActive(false);
		else botaoContinuar.SetActive(true);
		
		gPontuacao.s.ForcarAtualizarPontosEstrelas();
		
		if( gLevels.s.currentLevel.info.partituras.Length-1 == gLevels.s.currentPartituraIndex ) 
		{
			codigoTV.text = textoTV + "\n" +" [b]990" + (gLevels.s.currentLevelIndex + 1).ToString() + "[/b]";			
			botaoFesta.SetActive(true);
		}
		else
		{
			codigoTV.text = "";
			botaoFesta.SetActive(false);
		}
	}
	
	public void OnClickFestinha()
	{
		gMenus.s.ShowMenu("Festa");
	}
	
	public void OnClickHome()
	{
		gMenus.s.ShowMenu("Principal");
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
