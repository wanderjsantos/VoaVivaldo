using UnityEngine;	
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class _CompassoEditor : VivaldoEditor
{
	public List<_NotaEditor> 	notas;
	
	public Compasso				mCompasso;
	public NotaInfo notaDebug;
	
	public _CompassoEditor()
	{
		mCompasso = new Compasso();	
		mCompasso.info = new CompassoInfo();
		notas = new List<_NotaEditor>();
	
		notaDebug = new NotaInfo();
	}
	
	public Compasso GetCompasso()
	{		
		mCompasso.info.notas = new List<NotaInfo>();
		
		notas.ForEach( delegate( _NotaEditor e )
		{
			mCompasso.info.notas.Add( e.notaInfo );
		});
		
		return mCompasso;
	}
	
	public _CompassoEditor GetCompassoEditor(Compasso compasso)
	{
		_CompassoEditor ret = new _CompassoEditor();
		
		compasso.info.notas.ForEach( delegate(NotaInfo e) 
		{
			_NotaEditor notaEditor = new _NotaEditor();
			notaEditor.notaInfo = e;
			ret.notas.Add(notaEditor);
		});
		
		ret.mCompasso = compasso;
				
		return ret;
	}
	
	int compasso;
	public void Draw(int mIndice)
	{
		compasso = mIndice;
		DrawCompasso();		
		DrawComandos();
	}
	
	void DrawCompasso()
	{
		EditorGUILayout.BeginHorizontal();
		
		GUI.color = VerificarValorDoCompasso();		
		GUILayout.Button( "", GUILayout.Width(30f), GUILayout.Height(30f) ) ;
		
		GUI.color = Color.white;
		
		for( int i = 0; i < notas.Count; i++ )
		{
			_NotaEditor n = notas[i];
			
			switch( n.notaInfo.tipo )
			{
			case TipoDeNota.NOTA:
				DrawNotaComum( n );
				break;
			case TipoDeNota.PAUSA:
				DrawPausa( n );
				break;
			case TipoDeNota.NOTA_X2:
				DrawNOTA_X2(n);
				break;
			case TipoDeNota.NOTA_X3:
				DrawNOTA_X3(n);
				break;
			case TipoDeNota.NOTA_X4:
				DrawNOTA_X4(n);
				break;
			default:
				DrawNotaComum(n);
				break;
			}
			
		}
		
		GUI.color = Color.white;
		
		EditorGUILayout.EndHorizontal();
	}
	
	void DrawNotaComum( _NotaEditor n )
	{
		float width = Vivaldos.WIDTH_COMPASSO/ (float)n.notaInfo.duracao;
		
		GUI.color = Color.white;		
		if( GUILayout.Button( ((int)n.notaInfo.timbre).ToString() + "-" +  n.notaInfo.duracao.ToString(), GUILayout.Width(width), GUILayout.Height(20f) ) )
		{				
			EditNota( n );
		}
	}
	
	void DrawPausa( _NotaEditor n )
	{
		float width = Vivaldos.WIDTH_COMPASSO/ (float)n.notaInfo.duracao;
		
		GUI.color = Color.gray;
		
		if( GUILayout.Button( ((int)n.notaInfo.timbre).ToString() + "-" +  n.notaInfo.duracao.ToString(), GUILayout.Width(width), GUILayout.Height(20f) ) )
		{				
			EditNota( n );
		}
	}
	
	void DrawNOTA_X2( _NotaEditor n )
	{
		float width = Vivaldos.WIDTH_COMPASSO/ (float)n.notaInfo.duracao;
		
		GUI.color = Color.green;
		
		if( GUILayout.Button( ((int)n.notaInfo.timbre).ToString() + "-" +  n.notaInfo.duracao.ToString(), GUILayout.Width(width), GUILayout.Height(20f) ) )
		{				
			EditNota( n );
		}
	}
	
	void DrawNOTA_X3( _NotaEditor n )
	{
		float width = Vivaldos.WIDTH_COMPASSO/ (float)n.notaInfo.duracao;
		
		GUI.color = Color.red;
		
		if( GUILayout.Button( ((int)n.notaInfo.timbre).ToString() + "-" +  n.notaInfo.duracao.ToString(), GUILayout.Width(width), GUILayout.Height(20f) ) )
		{				
			EditNota( n );
		}
	}
	
	void DrawNOTA_X4( _NotaEditor n )
	{
		float width = Vivaldos.WIDTH_COMPASSO/ (float)n.notaInfo.duracao;
		
		GUI.color = Color.cyan;
		
		if( GUILayout.Button( ((int)n.notaInfo.timbre).ToString() + "-" +  n.notaInfo.duracao.ToString(), GUILayout.Width(width), GUILayout.Height(20f) ) )
		{				
			EditNota( n );
		}
	}
	
	
	void EditNota( _NotaEditor n )
	{
		if( Event.current.control && Event.current.alt && Event.current.shift )
		{
			RemoverNota(n);
			return;
		}
	
	
		if( Event.current.control )
		{
			if ( !Event.current.shift )
					AumentarTimbre( n );
			else
					AumentarTimbre( n , -1 );
			return;
		}
		
		if( Event.current.alt )
		{
			TrocarTipo( n );
			return;
		}
	
		int dur = (int) n.notaInfo.duracao;
		
		if( dur <= (int) Duracao.SEMIBREVE ) dur = 128;
		
		if( dur >= (int) Duracao.SEMIFUSA ) dur = 1;
		
		if( !Event.current.shift )
			dur = dur * 2;
		else
			dur = dur / 2;
		
		n.notaInfo.duracao = (Duracao) dur;
	}	

	void AumentarTimbre (_NotaEditor n, int mult = 1)
	{
		int timbre = (int)n.notaInfo.timbre;
		
		if( timbre == (int)Timbre.QUATORZE ) timbre = 0;
		
		timbre += mult;
		
		n.notaInfo.timbre = (Timbre) timbre;
	
	}

	void TrocarTipo (_NotaEditor n)
	{
		int t = (int) n.notaInfo.tipo;
		
		if( t == 4 ) t = -1;
		
		t += 1;
		
		n.notaInfo.tipo = (TipoDeNota) t;
	}

	void RemoverNota (_NotaEditor n)
	{
		if( notas.Contains( n ) == false ) return;
		
		notas.Remove( n );
	}	
	
	float GetAllWidths ()
	{
		float ret = 0f;
		foreach( _NotaEditor nota in notas )
		{
			ret += Vivaldos.WIDTH_COMPASSO/ (float)nota.notaInfo.duracao;
		}
		return ret;
	}

 
	
	void DrawComandos()
	{
		if( notaDebug == null ) notaDebug = new NotaInfo();
	
		EditorGUILayout.BeginHorizontal();
						
							
			GUILayout.Space( Vivaldos.WIDTH_COMPASSO - GetAllWidths());
		    GUILayout.Label("Nova:");			
			notaDebug.tipo = (TipoDeNota)	EditorGUILayout.EnumPopup( "", notaDebug.tipo, GUILayout.Width(100f));
			notaDebug.timbre = (Timbre) 	EditorGUILayout.EnumPopup( "", notaDebug.timbre, GUILayout.Width(100f));
			notaDebug.duracao = (Duracao) 	EditorGUILayout.EnumPopup( "", notaDebug.duracao, GUILayout.Width(100f));
			
			notaDebug.compasso = compasso ;
			GUILayout.Label("No compasso: " + notaDebug.compasso.ToString() );
			
			
			
			GUI.color = Color.green;
			if( GUILayout.Button( "+" , GUILayout.Width(100f)) && VerificarValorDoCompasso() != Color.red ) AdicionarNovaNota();
			
			GUI.color = Color.cyan;
			if( GUILayout.Button( "||" , GUILayout.Width(100f) )) DuplicarUltimaNota();
		
			GUI.color = Color.red;
			if( GUILayout.Button( "-" , GUILayout.Width(100f) )) RemoverUltimaNota();
			GUI.color = Color.white;
		
		EditorGUILayout.EndHorizontal();
	}
	
	Color VerificarValorDoCompasso()
	{
		float soma = 0f;
		Color ret	= Color.white;
		
		for( int i = 0; i < notas.Count; i ++ )
		{
			soma += 1f/ (float) notas[i].notaInfo.duracao;
		}
		
		if( soma == 1f ) ret = Color.green;
		if( soma < 1f ) ret = Color.yellow;
		if( soma > 1f ) ret = Color.red;
		
		return ret;
		
	}

	void DuplicarUltimaNota ()
	{
		if( notas.Count == 0 ) return;
		
			_NotaEditor novaNota = new _NotaEditor();
			NotaInfo novoInfo = new NotaInfo();
			_NotaEditor qualDuplicar = notas[notas.Count-1];
		
			novaNota.notaInfo = novoInfo;
			
			novoInfo.tipo 		= qualDuplicar.notaInfo.tipo;
			novoInfo.batida 	= qualDuplicar.notaInfo.batida;
			novoInfo.compasso 	= qualDuplicar.notaInfo.compasso;
			novoInfo.duracao 	= qualDuplicar.notaInfo.duracao;
			novoInfo.timbre 	= qualDuplicar.notaInfo.timbre;
			
			notas.Add( novaNota );
	}

	
	public void AdicionarNovaNota()
	{
		_NotaEditor n = new _NotaEditor();
		n.notaInfo = notaDebug;
		notaDebug = new NotaInfo();
		notas.Add( n );
	}
	
	public void RemoverUltimaNota()
	{
		if( notas.Count == 0 ) return;
		notas.Remove( notas[ notas.Count-1] );
	}
	
	
}




