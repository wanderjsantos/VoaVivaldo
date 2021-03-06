﻿using UnityEngine;
using System.Collections;

public class MenuDerrota : Menu {

	public vPersonagensDancando personagens;

	public override void Show ()
	{
		base.Show ();
		
		gFX.s.Play( FX.DERROTA );
		
		personagens.DesativarTodos();
		personagens.Ativar( gGame.s.player.mInfo.meuPersonagem );
		gPontuacao.s.ForcarAtualizarPontosEstrelas();
	}
	
	public override void Hide ()
	{
		base.Hide ();
	}
	
	public void OnClickReiniciar()
	{
		gGame.s.IniciarJogo();
	}
	
	public void OnClickHome()
	{
		gComandosDeMusica.s.Stop();
		gGame.s.Pause( false );
		gMusica.s.musicaAtual.Stop();
		gGame.s.FimDeJogo();
		gMenus.s.ShowMenu("Principal");
		
	}
	
	
}
