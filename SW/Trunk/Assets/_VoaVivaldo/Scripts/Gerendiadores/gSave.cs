using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class gSave : MonoBehaviour {

	public static gSave s;

	public void Awake()
	{
		s = this;
		Application.runInBackground = false;
	}

	public void Salvar(MusicaData data)
	{
		Debug.LogWarning ("Salvando");
		XmlSerializer serializer = new XmlSerializer (typeof(MusicaData));
		FileStream stream = new FileStream (Application.persistentDataPath + "/Musica.xml", FileMode.Create);
		serializer.Serialize (stream, data);
		stream.Close ();
	}

	public MusicaData Load()
	{
		Debug.LogWarning ("Carregando");
//		XmlSerializer serializer = new XmlSerializer (typeof(MusicaData));
//		FileStream stream = new FileStream (Application.persistentDataPath + "/Musica.xml", FileMode.Open);
//		MusicaData ret = serializer.Deserialize (stream) as MusicaData;
//		stream.Close ();
		
		XmlSerializer serializer = new XmlSerializer(typeof(MusicaData));
		
		TextAsset text = Resources.Load( "Musica") as TextAsset;
		
		StringReader reader = new StringReader( text.text );
		
		return serializer.Deserialize(reader) as MusicaData;

	}
	
}
