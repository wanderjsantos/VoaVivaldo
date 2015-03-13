using UnityEngine;
using System.Collections;

public delegate void OnAplicarTema();
public class gTemas : MonoBehaviour 
{	
	public static event OnAplicarTema onChange;
	
	public static gTemas s;
	
	public Tema temaAtual;
	
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
	
	public void Aplicar(  )
	{
		Change();
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
