using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Vivaldos  
{
	public static int LINHAS = 14;
	public static int COLUNAS = 4;
	public static int COMPASSOS_DEFAULT = 1;
	public static float WIDTH_COMPASSO = 600f;
	
	public static bool VIBRAR = true;
	
	public static MusicaData TransformEditorToMusicaData(_MusicaEditor mus)
	{
		MusicaData ret = new MusicaData();
		
		ret.BPM 		= mus.BPM;
		ret.audioBase	= mus.audioBase.name;
		
		foreach( _InstrumentoEditor eInstrumento in mus.banda )
		{
			Instrumento instrumento 		= new Instrumento();
			instrumento.audioInstrumentos 	= eInstrumento.audio.name;
			
			List<Trecho> trechosDesseInstrumento = new List<Trecho>();
			foreach( _TrechoEditor eTrecho in eInstrumento.trechos )
			{
				trechosDesseInstrumento.Add( TransformTrechoEditorToTrecho( eTrecho ) );
			}
			instrumento.trechos.AddRange( trechosDesseInstrumento );
												
			ret.instrumentos.Add( instrumento );
			
		}
				
		return ret;
	}
	
	public static Trecho TransformTrechoEditorToTrecho( _TrechoEditor trecho )
	{
		Trecho ret =  new Trecho();
		
		foreach( _CompassoEditor eCompasso in trecho._compassos )
		{
			ret.compassos.Add( TransformCompassoEditorToCompasso( eCompasso ) );
		}
		
		return ret;
	}
	
	public static Compasso TransformCompassoEditorToCompasso( _CompassoEditor compasso )
	{
		Compasso ret = new Compasso();
		ret.notas = GetAllNotasFromCompasso( compasso );
		
		return ret;
	}
	
	public static List<NotaInfo> GetAllNotasFromCompasso (_CompassoEditor compasso )
	{
		List<NotaInfo> ret = new List<NotaInfo>();
		foreach( _NotaEditor eNota in compasso.notas )
		{
			ret.Add( eNota.notaInfo );
		}
		
		return ret;
	}
	
//	public static _MusicaEditor TransformDataToEditor( MusicaData musicaData )
//	{
//		
//	}
}
