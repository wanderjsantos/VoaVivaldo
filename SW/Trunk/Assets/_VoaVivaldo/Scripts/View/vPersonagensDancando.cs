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
