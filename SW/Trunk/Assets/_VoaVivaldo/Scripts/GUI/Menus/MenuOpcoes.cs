﻿using UnityEngine;
using System.Collections;

public class MenuOpcoes : Menu {

	public override void Hide ()
	{
		base.Hide ();
		gSave.s.GravarInformacoes();
	}

	public void OnClickHome()
	{
		gMenus.s.ShowMenu("Principal");
	}
	
	public void OnChangeValueVibrar( bool valor )
	{	
			
		Vivaldos.VIBRAR = !valor;
		
	}
	
	public void OnChangeValueAudio( bool valor )
	{
		Vivaldos.AUDIO = !valor;
	}
}
