using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Vivaldos  
{
	public static int LINHAS = 14;
	public static int COLUNAS = 4;
	public static int COMPASSOS_DEFAULT = 1;
	public static float WIDTH_COMPASSO = 600f;
	
	public static bool VIBRAR = false;
	
	public static AudioClip NameToAudioClip( string name )
	{
		return (AudioClip) Resources.Load(name);
	}
	
	public static string AudioclipToName( AudioClip clip )
	{
		return clip.name;
	}
	
	
}
