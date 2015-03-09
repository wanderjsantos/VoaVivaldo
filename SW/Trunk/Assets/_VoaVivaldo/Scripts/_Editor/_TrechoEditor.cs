using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _TrechoEditor 
{
	public 	string				nome		= "Trecho";
	
	public 	bool				foldout 	= false;
	public	Vector2				scroll		= Vector2.zero;
	
	public _LinhaDeNotasEditor[,] 	grid2;
	
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
		
	public void Draw( int compasso, int linha )
	{
		grid2[compasso,linha].Draw();
	}
	
	public void Init()
	{
		grid2 = new _LinhaDeNotasEditor[compassos,linhas];
		
		for (int x = 0; x < compassos; x ++)
		{
			for (int i =0; i< linhas; i++) 
			{
				_LinhaDeNotasEditor nota = new _LinhaDeNotasEditor ();
				grid2[x,i] = nota;
			}
		}
	}
			
}
