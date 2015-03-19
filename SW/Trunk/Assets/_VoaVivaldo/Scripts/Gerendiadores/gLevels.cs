using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gLevels : MonoBehaviour 
{
	public static gLevels s;

	public int currentLevelIndex = 0;
	public int currentPartituraIndex = 0;

	public Level currentLevel;
	public Partitura currentPartitura;
	
	public List<Level>allLevels;

	void Awake()
	{
		s = this;
	}
	
	void OnEnable()
	{
		gComandosDeMusica.onPlay += Resetar;
		gGame.onReset += Resetar;
	}
	
	void OnDisable()
	{
		gComandosDeMusica.onPlay -= Resetar;
		gGame.onReset -= Resetar;
	}

	public List<Level> GetLevelsLiberados ()
	{
		List<Level> ret = allLevels.FindAll( e => e.savedInfo.liberado );
		return ret;
	}

	void Resetar ()
	{
//		currentLevel = null;
//		currentLevelIndex = -1;
		
	}

	public Level SetLevel( int index )
	{
		currentLevelIndex = ClampIndex (index);
//		currentLevel = NewLevel(index);
		currentLevel = allLevels[index];
		
		gTemas.s.Aplicar( currentLevel.info.tema );
		
		return currentLevel;
	}
	
//	public Level NewLevel( int index )
//	{
//		Level l = Instantiate( allLevels [index] ) as Level;
//		l.transform.parent = transform;
//		l.gameObject.SetActive(false);
//		
//		return l;
//	}

	public Level GetLevel( int index = 0, bool set = false )
	{
		if (set)		SetLevel (index);
		
		return allLevels [index];
	}
	
	public void SetLevel( int musica, int partitura )
	{
		currentLevel = SetLevel( musica );
		currentPartitura = SetPartitura( partitura );
		
	}

	Partitura SetPartitura (int partitura)
	{
		currentPartituraIndex = partitura;
		currentPartitura = currentLevel.info.partituras[partitura];
		return currentPartitura;
	}

	public PartituraInfo GetPartitura (int indice)
	{
		
		currentPartitura = NewPartitura(indice);
		return currentPartitura.info;
	}
	
	public Partitura NewPartitura(int indice)
	{
		Debug.Log("Indice>" + indice );
		currentPartituraIndex = indice;
		Partitura p = Instantiate( currentLevel.info.partituras[indice] ) as Partitura;
		currentPartitura = p;
		p.transform.parent = transform;
		p.gameObject.SetActive(false);
		return p;
	}

	public void FinalizarLevel ()
	{
		bool pontos = currentLevel.SetPontuacao	( currentPartituraIndex, gGame.s.player.mInfo.pontuacao );
		bool estrelas = currentLevel.SetEstrelas	( currentPartituraIndex, gPontuacao.s.estrelasGanhas );
		
//		if( estrelas || pontos )
//		{
//			if( currentLevel.savedInfo.partiturasConcluidas[currentPartituraIndex].liberado == false )
//				currentLevel.savedInfo.partiturasConcluidas[currentPartituraIndex].liberado = true;
//		}
		
	}
	
	public void LiberarProximaFase( int faseAtual )
	{
	
	}
	
	public void LiberarProximoLevel()
	{
	
	}

//	public void NextLevel()
//	{
//		SetLevel (currentLevelIndex++);
//	}
//
//	public void PreviousLevel()
//	{
//		SetLevel (currentLevelIndex--);
//	}

	public int ClampIndex( int currentIndex )
	{
		return Mathf.Clamp (currentIndex, 0, allLevels.Count);
	}
}
