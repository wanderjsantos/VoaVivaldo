using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class gSave : MonoBehaviour {

	public static gSave s;
	
	public VivaldoSave saveSettings;
	public VivaldoSave defaultSavedGame;

	public void Awake()
	{
		s = this;
	}
	
	public void Start()
	{
		saveSettings = CarregarArquivo();
		AplicarSaveGame();
	}
	
	public void GravarInformacoes()
	{
		SalvarEmArquivo();
	}

	void SalvarEmArquivo()
	{
		Debug.LogWarning("Saving Settings");
		XmlSerializer 	serializer = new XmlSerializer( typeof(VivaldoSave ) );
		FileStream		writer = new FileStream( "Assets/_VoaVivaldo/SaveGames/Save.xml", FileMode.Create );
		serializer.Serialize( writer, saveSettings );
		writer.Close();

	}

	VivaldoSave CarregarArquivo ()
	{
		if( File.Exists( "Assets/_VoaVivaldo/SaveGames/Save.xml" ) == false )
		{
			Debug.LogWarning("Usando save default");
			return defaultSavedGame;
		}
	
		Debug.LogWarning ("Loading Settings");
		XmlSerializer serializer = new XmlSerializer (typeof(VivaldoSave));
		FileStream stream = new FileStream ("Assets/_VoaVivaldo/SaveGames/Save.xml", FileMode.Open);
		VivaldoSave ret = serializer.Deserialize (stream) as VivaldoSave;
		stream.Close ();
		return ret;
	}
	
	public void AplicarSaveGame()
	{
		AplicarLevels();
	}

	void AplicarLevels ()
	{
		if( saveSettings == null ) saveSettings = CarregarArquivo();
		foreach( Level level in gLevels.s.allLevels )
		{
			level.savedInfo = saveSettings.savedLevels.Find( e => e.meuLevel == level.info.meuIndice );
		}
	}
}
