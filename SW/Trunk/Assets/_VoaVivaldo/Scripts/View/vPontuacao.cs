using UnityEngine;
using System.Collections;

public class vPontuacao : MonoBehaviour 
{	
	public UILabel labelPontuacao;
	
	public void OnEnable()
	{
		gPontuacao.onUpdatePontuacao += UpdatePontuacao;
	}
	
	public void OnDisable()
	{
		gPontuacao.onUpdatePontuacao -= UpdatePontuacao;
	}

	void UpdatePontuacao (int quantidade)
	{
		labelPontuacao.text = "pts " + quantidade.ToString();
	}
}


