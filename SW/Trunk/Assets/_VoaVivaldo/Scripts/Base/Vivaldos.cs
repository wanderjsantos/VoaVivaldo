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
	
	public static AudioClip NameToAudioClip( string name )
	{
		return (AudioClip) Resources.Load(name);
	}
	
	public static string AudioclipToName( AudioClip clip )
	{
		return clip.name;
	}
	
//	public static MusicaData TransformEditorToMusicaData(_MusicaEditor mus)
//	{
//		MusicaData ret = new MusicaData();
//		
//		ret.BPM 		= mus.BPM;
////		ret.audioBase	= mus.audioBase.name;
////		ret.tema		= mus.tema;
//		ret.nome		= mus.nome;
//		foreach( _InstrumentoEditor eInstrumento in mus.banda )
//		{
//			Instrumento instrumento 		= new Instrumento();
//			instrumento.audioBase			= eInstrumento.audioBase.name;
//			instrumento.audioInstrumentos 	= eInstrumento.audio.name;
//			instrumento.personagem			= eInstrumento.personagem;
//			
//			List<Trecho> trechosDesseInstrumento = new List<Trecho>();
//			foreach( _TrechoEditor eTrecho in eInstrumento.trechos )
//			{
//				trechosDesseInstrumento.Add( TransformTrechoEditorToTrecho( eTrecho ) );
//			}
//			instrumento.trechos.AddRange( trechosDesseInstrumento );
//												
//			ret.instrumentos.Add( instrumento );
//			
//		}
//				
//		return ret;
//	}
	
//	public static _MusicaEditor TransformDataToMusicaEditor(MusicaData data)
//	{
//		_MusicaEditor ret = new _MusicaEditor();
//		
//		ret.BPM 		= data.BPM;
////		ret.audioBase	= (AudioClip) Resources.Load( data.audioBase );
//		ret.nome		= data.nome;
//		ret.banda = new List<_InstrumentoEditor>();
//		
////		ret.tema		= data.tema;
//		
//		foreach( Instrumento instrumento in data.instrumentos )
//		{
//			_InstrumentoEditor eInstrumento = new _InstrumentoEditor();
//			eInstrumento.audio 		=(AudioClip) Resources.Load( instrumento.audioInstrumentos );
//			eInstrumento.audioBase 	=(AudioClip) Resources.Load( instrumento.audioBase );
//			
//			eInstrumento.personagem = instrumento.personagem;
//			
//			eInstrumento.trechos = new List<_TrechoEditor>();
//			foreach( Trecho t in instrumento.trechos )
//			{
//				eInstrumento.trechos.Add( TransformTrechoToTrechoEditor( t ) );
//			}
//			
//			ret.banda.Add( eInstrumento );
//		}
//		
//		return ret;
//	}
	
//	public static Trecho TransformTrechoEditorToTrecho( _TrechoEditor trecho )
//	{
//		Trecho ret =  new Trecho();
//		
//		foreach( _CompassoEditor eCompasso in trecho._compassos )
//		{
//			ret.compassos.Add( TransformCompassoEditorToCompasso( eCompasso ) );
//		}
//		
//		return ret;
//	}
//	
//	public static _TrechoEditor TransformTrechoToTrechoEditor( Trecho trecho )
//	{
//		_TrechoEditor ret =  new _TrechoEditor();
//		
//		foreach( Compasso eCompasso in trecho.compassos )
//		{
//			ret._compassos.Add( TransformCompassoToCompassoEditor( eCompasso ) );
//
//		}
//	
//		return ret;
//	}
//	
//	public static Compasso TransformCompassoEditorToCompasso( _CompassoEditor compasso )
//	{
//		Compasso ret = new Compasso();
//		ret.notas = GetAllNotasFromCompasso( compasso );
//		
//		return ret;
//	}
//	
//	public static _CompassoEditor TransformCompassoToCompassoEditor( Compasso compasso )
//	{
//		_CompassoEditor ret = new _CompassoEditor();
//		ret.notas = new List<_NotaEditor>();
//		ret.notas.AddRange( GetAllNotasFromCompasso( compasso ) );
//		
//		return ret;
//	}
//	
//	public static List<NotaInfo> GetAllNotasFromCompasso (_CompassoEditor compasso )
//	{
//		List<NotaInfo> ret = new List<NotaInfo>();
//		foreach( _NotaEditor eNota in compasso.notas )
//		{
//			ret.Add( eNota.notaInfo );
//		}
//		
//		return ret;
//	}
//	
//	public static List<_NotaEditor> GetAllNotasFromCompasso (Compasso compasso )
//	{
//		List<_NotaEditor> ret = new List<_NotaEditor>();
//				
//		foreach( NotaInfo n in compasso.notas )
//		{
//			_NotaEditor nE = new _NotaEditor();
//			nE.notaInfo = n;
//			ret.Add(nE);
//		}
//	
//		return ret;
//			
//	}
//	
//	public static List<LevelData> TransformEditorToLevelData (_LevelManagerEditor levelManager)
//	{
//		List<LevelData> ret = new List<LevelData>();
//		List<MusicaData> mEditor = new List<MusicaData>();
//		
//		foreach( _LevelEditor level in levelManager.levels )
//		{
//			LevelData l = new LevelData();
//			l.nome = level.nome;
//			foreach( _MusicaEditor mus in level.musicas )
//			{
//				mEditor.Add( TransformEditorToMusicaData( mus ) );
//				l.dadosDeMusicas.AddRange(mEditor);
//			}
//			
//			ret.Add(l);
//			
//		}
//		
//		return ret;
//	}
}
