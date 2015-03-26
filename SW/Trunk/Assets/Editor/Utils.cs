using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Utils : Editor
{
	public static	bool				init = false;

	public static 	UI2DSprite 			currentSprite;
	public static 	UI2DSpriteAnimation currentAnimation;

	[MenuItem( "Utils/Adicionar nova animaçao a seleçao" )]
	public static void InitFillSprites()
	{
		init = true;
		SetAnimations();
	}
	
	public static void SetAnimations()
	{
//		if( init == false ) return;
		
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
		init = false;
			
	}
	
}
