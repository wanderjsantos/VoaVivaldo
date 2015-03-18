using UnityEngine;
using System.Collections;

public class BotaoFase : MonoBehaviour {

	public int 			selecionarLevel = 0;
	
	public GameObject 	botaoFesta;

	public void SetValue (bool liberado, bool festaLiberada)
	{
		Debug.Log(gameObject.name + " liberado: " + liberado );
		
		
		if( liberado ) 	gameObject.GetComponent<UI2DSpriteAnimation>().Play("idle");// = liberado;
		else 			gameObject.GetComponent<UI2DSpriteAnimation>().Pause();// = liberado;
		
		botaoFesta.SetActive( festaLiberada );
		
		gameObject.GetComponent<UIButton>().isEnabled = liberado;
	}
}
