using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _TrechoEditor 
{
	public 	string				nome		= "Trecho";
	
	public 	bool				foldout 	= false;
	public	Vector2				scroll		= Vector2.zero;
	
	public _NotaEditor[,,] 		grid;
	public List<_NotaEditor> 	notas		= new List<_NotaEditor>();
	
	public int					compassos 	= Vivaldos.COMPASSOS_DEFAULT;
	public int					linhas		= Vivaldos.LINHAS;
	public int					colunas		= Vivaldos.COLUNAS;
	
	public void AddCompasso()
	{
		compassos ++;
		Init();
		
	}
	
	public void RemoveCompasso()
	{
		compassos = Mathf.Clamp(compassos--, Vivaldos.COMPASSOS_DEFAULT, 100 );
		Init();
		
	}
	
	public _TrechoEditor()
	{
		Init();
	
	}
	public void Init()
	{
		grid = new _NotaEditor[compassos, linhas , colunas];
		notas = new List<_NotaEditor> ();
		for (int x = 0; x < compassos; x ++)
		{
			for (int i =0; i< linhas; i++) 
			{
				for (int j = 0; j < colunas; j++) 
				{
					_NotaEditor button = new _NotaEditor (i, j, null);
					notas.Add (button);
					grid [x,i, j] = button;
				}
			}
		}
	}
			
}
