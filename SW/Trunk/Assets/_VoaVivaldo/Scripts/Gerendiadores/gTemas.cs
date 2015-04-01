using UnityEngine;
using System.Collections;

public delegate void OnAplicarTema();
public class gTemas : MonoBehaviour 
{	
	public static event OnAplicarTema onChange;
	
	public static gTemas s;
	
	public Tema temaAtual;
	
	public Tema temaDisabled;
	
	public bool	usarTemas = false;
	
	void Awake()
	{
		s = this;
	}	
	
	public void Aplicar( Tema tema )
	{
		if( temaAtual == tema ) return;
		temaAtual = tema;
		Change();
	}
	
//	public void Aplicar(  )
//	{
//		Aplicar ( gLevels.s.allLevels[0].info.tema );
////		Change();
//	}
	
	 public void AplicarAtual()
	{
		if( gLevels.s.currentLevel != null )
			Aplicar( gLevels.s.currentLevel.info.tema );
		else
			Aplicar ( gLevels.s.allLevels[0].info.tema );
	}
	
	public void Change()
	{
		if( onChange != null ) onChange();
	}

	public Color GetCor (QualCorDoTema aplicar)
	{
		switch( aplicar )
		{
			case QualCorDoTema.BACKGROUND:
				return temaAtual.corBackground;
			case QualCorDoTema.CLARO:
				return temaAtual.corClaro;
			case QualCorDoTema.ESCURO:
				return temaAtual.corEscuro;
			case QualCorDoTema.TEXTO:
				return temaAtual.corTextos;
			default:
				return temaAtual.corBackground;
		}
	}
}
