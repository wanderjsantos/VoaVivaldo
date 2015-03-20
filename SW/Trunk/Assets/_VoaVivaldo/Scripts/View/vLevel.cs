using UnityEngine;
using System.Collections;

public class vLevel : MonoBehaviour {

	public int selecionarInstrumento = 0;
	public int	minhaFase = -1;
	
	public vEstrelas estrelas;
	
	public void Awake()
	{
		if( estrelas == null ) estrelas = gameObject.GetComponentInChildren<vEstrelas>();
		if( minhaFase < 0 ) minhaFase = selecionarInstrumento;
	}
	
	public void SetActive( bool estado )
	{
		gameObject.SetActive( estado );	
	}
	

	public void SetActive( bool estado, int quantidadeDeEstrelas )
	{
		gameObject.SetActive( estado );	
		if( estrelas )
			estrelas.SetNewEstrela( quantidadeDeEstrelas );
	}
}
