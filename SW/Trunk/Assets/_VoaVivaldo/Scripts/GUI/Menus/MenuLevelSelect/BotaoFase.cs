using UnityEngine;
using System.Collections;

public class BotaoFase : MonoBehaviour {

	public int selecionarLevel = 0;

	public void SetValue (bool liberado)
	{
		Debug.Log(gameObject.name + " liberado: " + liberado );
		gameObject.GetComponent<UIButton>().isEnabled = liberado;
	}
}
