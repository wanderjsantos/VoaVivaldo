using UnityEngine;
using System.Collections;

public class PainelDeControle : MonoBehaviour {

	public Menu 	irParaMenu;
	public bool 	trocarMenu			= false;

	public bool 	posicionarNotas = false;

	public string 	audioBase 			= "Base1";
	public string 	audioInstrumento 	= "Instrumento1";
	public bool 	novaMusica 			= false;

	public bool 	play	 			= false;
	public bool		playAndRecord		= false;

	public int		carregarLevel		= 0;
	public bool		loadXML				= false;
	
	public bool		drawGyro			= false;
	
	public void OnGUI()
	{
		if( drawGyro ) 
		{
			Input.gyro.enabled = true;
		
			GUI.Box( new Rect( 30f, 30f, 400f, 30f  ), "Gyro.Attitude        :  " + Input.gyro.attitude);
			GUI.Box( new Rect( 30f, 60f, 400f, 30f  ), "Gyro.Gravity         :  " + Input.gyro.gravity);
			GUI.Box( new Rect( 30f, 90f, 400f, 30f  ), "Gyro.RotationRate    :  " + Input.gyro.rotationRate);
			GUI.Box( new Rect( 30f, 120f, 400f, 30f ), "Gyro.RotationRate2   :  " + Input.gyro.rotationRateUnbiased);
			GUI.Box( new Rect( 30f, 150f, 400f, 30f ), "Gyro.userAcceleration:  " + Input.gyro.userAcceleration);
		}
	}
	

	void Update()
	{
		if (trocarMenu) 
		{
			trocarMenu = false;
			gMenus.s.ShowMenu (irParaMenu);
		}

		if (posicionarNotas)
		{
			posicionarNotas = false;
			gPista.s.PosicionarTodasAsNotas();
		}

		if (play) 
		{
			play = false;
			gRecord.s.recordOnPlayMusic = false;
			gComandosDeMusica.s.Play();
			trocarMenu = true;
		}
		if (novaMusica)
		{
			novaMusica = false;
//			gMusica.s.NovaMusica( audioBase, audioInstrumento );
		}

		if (playAndRecord)
		{
			playAndRecord = false;
			gRecord.s.recordOnPlayMusic = true;
			gComandosDeMusica.s.Play();
			trocarMenu = true;
		}

		if (loadXML)
		{
			loadXML = false;
//			gMusica.s.CarregarMusica( carregarLevel );
		}
	}
}
