﻿using UnityEngine;
using System.Collections;

public class MenuTutorial : Menu {

	public Paginator paginator;

	public override void Show ()
	{
		base.Show ();
		paginator.Init();
		
//		Screen.orientation = ScreenOrientation.Landscape;
//		Screen.autorotateToLandscapeLeft = true;
//		Screen.autorotateToLandscapeRight = true;
//		Screen.autorotateToPortrait = false;
//		Screen.autorotateToPortraitUpsideDown = false;
	}
	
	public override void Hide ()
	{
		base.Hide ();
//		Screen.orientation = ScreenOrientation.AutoRotation;
//		Screen.autorotateToLandscapeLeft = true;
//		Screen.autorotateToLandscapeRight = true;
//		Screen.autorotateToPortrait = true;
//		Screen.autorotateToPortraitUpsideDown = true;
	}
	
	public void Fechar()
	{
		gGame.s.IniciarJogo();
	}
}
