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
			gMusica.s.NovaMusica( audioBase, audioInstrumento );
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
			gMusica.s.CarregarMusica( carregarLevel );
		}
	}
}
