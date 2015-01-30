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
	}

	public void Salvar(MusicaData data)
	{
		Debug.LogWarning ("Salvando");
		XmlSerializer serializer = new XmlSerializer (typeof(MusicaData));
		FileStream stream = new FileStream ("Assets/Musica.xml", FileMode.Create);
		serializer.Serialize (stream, data);
		stream.Close ();
	}

	public MusicaData Load()
	{
		Debug.LogWarning ("Carregando");
		XmlSerializer serializer = new XmlSerializer (typeof(MusicaData));
		FileStream stream = new FileStream ("Assets/Musica.xml", FileMode.Open);
		MusicaData ret = serializer.Deserialize (stream) as MusicaData;
		stream.Close ();
		return ret;
	}
	
}
