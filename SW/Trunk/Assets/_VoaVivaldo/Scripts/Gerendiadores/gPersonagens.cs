using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gPersonagens : MonoBehaviour {

	public static gPersonagens s;
	
	public List<vPersonagem> personagens;
	
	vPersonagem personagemAtual;
			
	public void Awake()
	{
		s = this;
	}

	public vPersonagem GetPersonagem (QualPersonagem personagem)
	{
		vPersonagem ret = personagens.Find( e => e.meuPersonagem == personagem );
		
		personagemAtual = Instantiate( ret ) as vPersonagem;		
		return personagemAtual;
	}
	
	
	
}
