using UnityEngine;
using System.Collections;

public class BotaoFase : MonoBehaviour {

	public int 			selecionarLevel = 0;
	
	public GameObject 	botaoFesta;

	public void SetValue (bool liberado, bool festaLiberada)
	{
//		Debug.Log(gameObject.name + " liberado: " + liberado );
		
		
		if( liberado ) 	gameObject.GetComponentInChildren<UI2DSpriteAnimation>().Play("idle");// = liberado;
		else 			gameObject.GetComponentInChildren<UI2DSpriteAnimation>().Pause();// = liberado;
		
		botaoFesta.SetActive( festaLiberada );
		
		gameObject.GetComponent<UIButton>().isEnabled = liberado;
	}
	
}
