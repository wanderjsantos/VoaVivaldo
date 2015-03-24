using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vEstrelas : MonoBehaviour 
{	
	public int estrelasGanhas = 0;

	public List<GameObject> estrelas;	
	
	public void OnEnable()
	{
		gPontuacao.onUpdateEstrelas += UpdateStars;
	}
	
	public void OnDisable()
	{
		gPontuacao.onUpdateEstrelas -= UpdateStars;
	}
	
	public void SetNewEstrela( int quantas )
	{	
		for( int i = 0; i < estrelas.Count; i++ ) {estrelas[i].SetActive(false);}
		
		for( int i = 0; i < quantas; i++ )
		{
			estrelas[i].SetActive( true );
		}
		
		
	}

	public void UpdateStars (int estrelas)
	{
//		Debug.Log("Update Estrelas");
	
		if( estrelasGanhas == estrelas ) return;
		
		estrelasGanhas = estrelas;
		
		SetNewEstrela( estrelasGanhas );
		gAudio.s.PlayNovaEstrelaClip();
	}
}
