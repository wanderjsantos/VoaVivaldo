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
		
		if( gPontuacao.s.estrelasGanhas == 0 ) return;
		
		if( currentPartituraIndex >= currentLevel.info.partituras.Length -1 )
			LiberarProximoLevel();
		else
		{			
			LiberarProximaFase();
			LiberarFesta();
		}
		
	}
	
	public void LiberarProximaFase( )
	{
		if( currentPartituraIndex >= currentLevel.savedInfo.partiturasConcluidas.Count ){ Debug.LogWarning("Todas as fases desbloqueadas"); return;}
		
		Debug.LogWarning("PROXIMA FASE: " + (currentPartituraIndex+1));	
		currentLevel.savedInfo.partiturasConcluidas[currentPartituraIndex + 1].liberado = true;
	}
	
	public void LiberarFesta()
	{
		if( currentPartituraIndex >= currentLevel.savedInfo.partiturasConcluidas.Count - 1 )
			currentLevel.savedInfo.festaLiberada = true;
		
				
			Debug.LogWarning("FESTA LIBERADA: " + (currentPartituraIndex >= currentLevel.savedInfo.partiturasConcluidas.Count - 1) );
		
	}
	
	public void LiberarProximoLevel()
	{
		if( currentLevelIndex >= allLevels.Count ){ Debug.LogWarning( "Todos os levels desbloqueados"); return; }
		
		Debug.LogWarning("PROXIMO LEVEL: " + (currentLevelIndex+1));
		
		allLevels[currentLevelIndex + 1 ].savedInfo.liberado = true;
		allLevels[currentLevelIndex + 1 ].savedInfo.partiturasConcluidas[0].liberado = true;
	}

	public void ProximaFase (int faseAtual)
	{
		int level = currentLevelIndex;
		int fase = currentPartituraIndex;
		
		Debug.LogWarning("PROXIMA FASE (atual): " + faseAtual );
		
		if( fase >= allLevels[level].info.partituras.Length - 1 ) 
		{
			Debug.LogWarning( "Todos as fases jogadas, indo para o proximo level");
			level += 1;
			fase = 0;
		}
		else
			fase += 1;
		
		if( level >= allLevels.Count -1 ){ Debug.LogWarning( "Todos os levels desbloqueados"); gMenus.s.ShowMenu("Principal"); return; }
		
		gMusica.s.Set( level, fase );
//		gMusica.s.SetFase (numero);		
		SetLevel( level, fase );		
		gGame.s.IniciarJogo();
	}

	public int ClampIndex( int currentIndex )
	{
		return Mathf.Clamp (currentIndex, 0, allLevels.Count);
	}
}
