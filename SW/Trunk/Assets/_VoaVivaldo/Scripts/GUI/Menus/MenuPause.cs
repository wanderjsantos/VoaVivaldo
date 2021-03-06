﻿using UnityEngine;
using System.Collections;

public class MenuPause : Menu {

	public void OnClickReiniciar()
	{
		gGame.s.Pause( false);
		gGame.s.IniciarJogo();		
	}
	
	public void OnClickContinuar()
	{
		gGame.s.Pause(false);
	}
	
	public void OnClickMusicas()
	{
		gComandosDeMusica.s.Stop();
		gGame.s.Pause( false );
		gMusica.s.musicaAtual.Stop();
		gGame.s.FimDeJogo();
		
		gMusica.s.Resetar();
		gNotas.s.Resetar();
		
		gMenus.s.ShowMenu("Musica");
	}
	
	public void OnClickHome()
	{
		gComandosDeMusica.s.Stop();
		gGame.s.Pause( false );
		gMusica.s.musicaAtual.Stop();
		gGame.s.FimDeJogo();
		
		gMusica.s.Resetar();
		gNotas.s.Resetar();
		
		gMenus.s.ShowMenu("Principal");
	}
}
