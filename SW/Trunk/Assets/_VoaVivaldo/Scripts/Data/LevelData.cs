using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;

public class LevelData
{
	public string nome;
	public List<MusicaData> dadosDeMusicas;
	
	public LevelData()
	{
		dadosDeMusicas =new List<MusicaData>();
	}
}








