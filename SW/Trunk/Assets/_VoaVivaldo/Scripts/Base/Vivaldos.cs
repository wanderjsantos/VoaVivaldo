using UnityEngine;
using System.Collections;

public static class Vivaldos  
{
	public static int LINHAS = 14;
	public static int COLUNAS = 4;
	public static int COMPASSOS_DEFAULT = 3;
	public static float WIDTH_COMPASSO = 600f;
	
	public static bool VIBRAR = true;
	
	public static MusicaData TransformEditorToMusicaData(_MusicaEditor mus)
	{
		MusicaData ret = new MusicaData();
		
		ret.BPM 		= mus.BPM;
		ret.audioBase	= mus.audioBase.name;
		
		foreach( _InstrumentoEditor instrumento in mus.banda )
		{
			Instrumento i = new Instrumento();
			i.audioInstrumentos = instrumento.audio.name;
			
			i.notas = instrumento.ConverterTrechosParaNotas();
			
			foreach( _TrechoEditor t in instrumento.trechos )
			{
				Trecho trecho = new Trecho();
				
				foreach( _CompassoEditor compasso in t._compassos )
				{
					foreach( _NotaEditor nota in compasso.notas )
					{
						if( (int) nota.notaInfo.timbre <= 0 ) continue;	
						
						NotaInfo info = new NotaInfo() ;
						info = nota.notaInfo;
						
						trecho.notasDoTrecho.Add( info );
					}
				
				}
				
				ret.trechos.Add( trecho );
				
			}
			
			ret.instrumentos.Add(i);
		}
		
		return ret;
	}
}
