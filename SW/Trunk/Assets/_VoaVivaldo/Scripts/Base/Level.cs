using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour 
{
	public LevelInfo 		info;
	public LevelSaveInfo 	savedInfo;
	
	public bool FestaLiberada()
	{
		foreach( PartituraSaveInfo save in savedInfo.partiturasConcluidas )
		{
			if( save.estrelasGanhas == 0 ) return false;
		}
		
		savedInfo.festaLiberada = true;
		return true;
	}
	
	public bool SetPontuacao( int partitura, int novaPontuacao )
	{
		if( savedInfo.partiturasConcluidas[partitura].pontosMarcados > novaPontuacao ) return false;		
		savedInfo.partiturasConcluidas[partitura].pontosMarcados = novaPontuacao;		
		return true;
	}
	
	public bool SetEstrelas( int partitura, int novaEstrela )
	{
		if( savedInfo.partiturasConcluidas[partitura].estrelasGanhas > novaEstrela ) return false;		
		savedInfo.partiturasConcluidas[partitura].estrelasGanhas = novaEstrela;		
		return true;
	}
	
	public bool SetPontuacao( int partitura, int novaPontuacao, int estrelas )
	{
		if( SetPontuacao( partitura, novaPontuacao ) && SetEstrelas( partitura, estrelas ) ) return true;
		return false;
	}
	
}

