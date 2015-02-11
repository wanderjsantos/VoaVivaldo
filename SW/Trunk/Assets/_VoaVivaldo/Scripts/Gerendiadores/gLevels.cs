using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gLevels : MonoBehaviour 
{
	public static gLevels s;

	public int currentLevelIndex = 0;

	public Level currentLevel;
	public List<Level>allLevels;

	void Awake()
	{
		s = this;
	}

	public Level SetLevel( int index )
	{
		currentLevelIndex = ClampIndex (index);
		currentLevel = allLevels [currentLevelIndex];

		return currentLevel;
	}

	public Level GetLevel( int index = 0, bool set = false )
	{
		if (set)
						SetLevel (index);
		return allLevels [index];
	}

	public void NextLevel()
	{
		SetLevel (currentLevelIndex++);
	}

	public void PreviousLevel()
	{
		SetLevel (currentLevelIndex--);
	}

	public int ClampIndex( int currentIndex )
	{
		return Mathf.Clamp (currentIndex, 0, allLevels.Count);
	}
}
