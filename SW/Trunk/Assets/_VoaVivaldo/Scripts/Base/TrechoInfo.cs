using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrechoInfo
{
	public 	string				nome		= "Trecho";
	public 	List<Compasso>		lCompassos;
	
	public int					compassos 	= Vivaldos.COMPASSOS_DEFAULT;
	public int					linhas		= Vivaldos.LINHAS;
	public int					colunas		= Vivaldos.COLUNAS;
	
	public TrechoInfo()
	{
		lCompassos = new List<Compasso>();
	}
}





