using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour 
{
	public GameObject goCombo;
	public UILabel lbl_multiplicador;
	
	void OnEnable()
	{
		gPontuacao.onUpdateCombo += UpdateCombo;
	}
	
	void OnDisable()
	{
		gPontuacao.onUpdateCombo -= UpdateCombo;
	}
	
	public void UpdateCombo( int multiplicador )
	{
		if( multiplicador <= 1 ) 
		{
			Saida();
			return;
		}
		
		lbl_multiplicador.text = "x"+multiplicador.ToString();
		Entrada();
	}

	void Entrada ()
	{
		goCombo.SetActive(true);
	}

	void Saida ()
	{
		goCombo.SetActive(false);
	}
}
