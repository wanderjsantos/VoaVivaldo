using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gLevels : MonoBehaviour 
{
	public static gLevels s;

	public int currentLevelIndex = 0;

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
		return currentLevel;
	}
	
	public Level NewLevel( int index )
	{
		Level l = Instantiate( allLevels [index] ) as Level;
		l.transform.parent = transform;
		l.gameObject.SetActive(false);
		return l;
	}

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
		Partitura p = Instantiate( currentLevel.info.partituras[indice] ) as Partitura;
		p.transform.parent = transform;
		p.gameObject.SetActive(false);
		return p;
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
