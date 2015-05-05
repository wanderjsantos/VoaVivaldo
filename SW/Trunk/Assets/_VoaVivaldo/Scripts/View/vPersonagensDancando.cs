using UnityEngine;
using System.Collections;

public class vPersonagensDancando : MonoBehaviour 
{
	public GameObject trumpet;
	public GameObject horn;
	public GameObject flauta;
	public GameObject sanfona;
	
	public void DesativarTodos()
	{
		trumpet	.SetActive(false);
		horn	.SetActive (false);
		flauta	.SetActive(false);
		sanfona	.SetActive(false);
	}	
	
	public void AtivarTodos(bool forcarAtivacao)
	{
		if( trumpet.activeInHierarchy == false && forcarAtivacao )
			trumpet	.SetActive(true);
		if( horn.activeInHierarchy == false && forcarAtivacao )
		horn	.SetActive (true);
		
		if( flauta.activeInHierarchy == false && forcarAtivacao )
			flauta	.SetActive(true);
		
		if( sanfona.activeInHierarchy == false && forcarAtivacao )
			sanfona	.SetActive(true);
		
		trumpet.GetComponent<UIToggle>().value = true;
		horn.GetComponent<UIToggle>().value = true;
		flauta.GetComponent<UIToggle>().value = true;
		sanfona.GetComponent<UIToggle>().value = true;
		
		trumpet	.GetComponent<UI2DSpriteAnimation>().Play("idle");
		horn	.GetComponent<UI2DSpriteAnimation>().Play("idle");
		flauta	.GetComponent<UI2DSpriteAnimation>().Play("idle");
		sanfona	.GetComponent<UI2DSpriteAnimation>().Play("idle");
	}	
	
	public GameObject GetInstrumento(QualPersonagem personagem)
	{
		switch( personagem )
		{
			case QualPersonagem.TRUMPET:
				return trumpet;
			case QualPersonagem.HORNET:
				return horn;
			case QualPersonagem.FLAUTA:
				return flauta;
			case QualPersonagem.SANFONA:
				return sanfona;
			default :
				return null;
		}	
	}
	
	public QualPersonagem GetPersonagem(GameObject go )
	{
		if( go == trumpet ) return QualPersonagem.TRUMPET;
		if( go == horn )	return QualPersonagem.HORNET;
		if( go == flauta )	return QualPersonagem.FLAUTA;
		if( go == sanfona ) return QualPersonagem.SANFONA;
		
		 return QualPersonagem.TRUMPET; 
	}
	
	public void Ativar( QualPersonagem qual )
	{
		switch( qual )
		{
			case QualPersonagem.TRUMPET:
				trumpet.SetActive(true);
				break;
			case QualPersonagem.HORNET:
				horn.SetActive(true);
				break;
			case QualPersonagem.FLAUTA:
				flauta.SetActive(true);
				break;
			case QualPersonagem.SANFONA:
				sanfona.SetActive(true);
				break;
			default :
				break;
		}
	}
	
}
