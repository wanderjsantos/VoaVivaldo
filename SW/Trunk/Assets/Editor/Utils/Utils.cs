using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Utils : Editor
{
	public static 	UI2DSprite 			currentSprite;
	public static 	UI2DSpriteAnimation currentAnimation;
	
	public static	gSave				save;

	[MenuItem( "Utils/Adicionar nova animaçao a seleçao" )]
	public static void InitFillSprites()
	{
		SetAnimations();
	}
	
	[MenuItem( "Utils/Liberar Todas as Musicas e Levels" )]
	public static void LiberarLevels()
	{
		LiberarTudo(true);
	}
	
	[MenuItem( "Utils/Bloquear Todas as Musicas e Levels" )]
	public static void BloquearLevels()
	{
		LiberarTudo(false);
	}
	
	public static void SetAnimations()
	{		
		currentSprite =  Selection.activeGameObject.GetComponent<UI2DSprite>();
		currentAnimation = Selection.activeGameObject.GetComponent<UI2DSpriteAnimation>();
		
		if( currentSprite == null || currentAnimation == null )
		{
			Debug.Log("Sprite ou animacao nula");
			 return;
		} 
		
		string spriteSheetPath = AssetDatabase.GetAssetPath( currentSprite.sprite2D );
		Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(spriteSheetPath).OfType<Sprite>().ToArray();
		
		currentAnimation.AddNewAnimation( "newAnimation", sprites );
			
	}
	
	static void LiberarTudo (bool estado)
	{
		save = GameObject.FindObjectOfType<gSave>();
		
		if( save == null ){ Debug.LogWarning("gSave nao encontrado"); return; }
		
		foreach( LevelSaveInfo savedLevel in save.defaultSavedGame.savedLevels )
		{
			savedLevel.liberado = estado;
			savedLevel.festaLiberada = estado;
			foreach( PartituraSaveInfo savedPartitura in savedLevel.partiturasConcluidas )
			{
				savedPartitura.liberado = estado;
			}
		}
		
		save.defaultSavedGame.savedLevels[0].liberado = true;
		save.defaultSavedGame.savedLevels[0].partiturasConcluidas[0].liberado = true;
		
		if( estado == false )
		{
			if( File.Exists( Vivaldos.SAVE_PATH + "Save.xml" ) ) File.Delete( Vivaldos.SAVE_PATH + "Save.xml");
		}
		
	}
	
	
	
	
}
