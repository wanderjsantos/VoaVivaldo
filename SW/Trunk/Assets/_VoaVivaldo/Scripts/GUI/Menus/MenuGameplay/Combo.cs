using UnityEngine;
using System.Collections;

public class Combo : MonoBehaviour 
{
	public GameObject goCombo;
	public UILabel lbl_multiplicador;
	
	public TweenScale tween;
	
	public EventDelegate nguiEvent;
	
	void Start()
	{
//		nguiEvent = new EventDelegate( this, "Saida" );
//		tween = GetComponentInChildren<TweenScale>();
		tween.AddOnFinished( nguiEvent );
	}
		
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
		if( multiplicador < 0 )
		{
			lbl_multiplicador.text = "x"+multiplicador.ToString();
			ForcarSaida();
			return;
		}
		else
		{
			if( multiplicador <= 1 ) 
			{
				Saida();
				return;
			}
		}
		
		lbl_multiplicador.text = "x"+multiplicador.ToString();
		Entrada();
	}

	void ForcarSaida ()
	{
		goCombo.SetActive(false);
	}
	
	public bool ativo = false;
	
	public void OnFinishAnimation()
	{
//		Debug.Log("OnFinishAnimation");
		goCombo.SetActive(ativo);
		
	}

	void Entrada ()
	{
		ativo = true;
		goCombo.SetActive(true);
		tween.PlayForward();
	}

	void Saida ()
	{
		ativo = false;
		tween.PlayReverse();
		
	}
}
