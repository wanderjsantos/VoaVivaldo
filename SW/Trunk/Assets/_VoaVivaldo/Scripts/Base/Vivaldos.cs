using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Vivaldos  
{
	public delegate void OnChangeAudioSettings(float volumeBase, float volumeInstrumentos, float volumeGeral);
	public static event OnChangeAudioSettings onChangeAudioSettings;

	public static int LINHAS = 14;
	public static int COLUNAS = 4;
	public static int COMPASSOS_DEFAULT = 1;
	public static float WIDTH_COMPASSO = 600f;
	
	static bool	vibrar = false;
	public static bool VIBRAR
	{
		get{return vibrar;}
		set
		{	
			vibrar =value;
			gSave.s.saveSettings.settings.vibrar = vibrar;
		}
	}
	


	static bool audio = true;
	public static bool AUDIO 
	{
		get{	return audio;	}
		set
		{
			audio = value;	
			if( value == false )
			{
				gSave.s.Mudo();
				if( onChangeAudioSettings != null ) onChangeAudioSettings( 0f, 0f,0f );	
			}
			else
			{
				gSave.s.RestaurarVolumes();
				if( onChangeAudioSettings != null ) onChangeAudioSettings(gSave.s.GetCurrentBaseVolume(), gSave.s.GetCurrentInstrumentosVolume(), gSave.s.saveSettings.settings.volumeGeral);
			}
			
		}
	}
	
	public static AudioClip NameToAudioClip( string name )
	{
		return (AudioClip) Resources.Load(name);
	}
	
	public static string AudioclipToName( AudioClip clip )
	{
		return clip.name;
	}
	
	
}
