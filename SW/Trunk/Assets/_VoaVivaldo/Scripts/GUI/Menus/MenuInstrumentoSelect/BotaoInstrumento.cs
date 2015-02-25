using UnityEngine;
using System.Collections;


public class BotaoInstrumento : MonoBehaviour 
{
	public enum Estado{ DISPONIVEL, INDISPONIVEL, BLOQUEADO }

	public Estado mEstado = Estado.BLOQUEADO;
	
	public UIToggle	mToggle;
	 
	
	public int selecionarInstrumento = 0;
	
	public void ChangeEstado( BotaoInstrumento.Estado novoEstado )
	{
		mEstado = novoEstado;
		switch( mEstado )
		{
			case Estado.BLOQUEADO:
				Bloquear();
				break;
			case Estado.INDISPONIVEL:
				Indisponibilizar();
				break;
			case Estado.DISPONIVEL:
				Disponibilizar();
				break;
			default:
				Disponibilizar();
				break;
		}
	}	

	void Bloquear ()
	{
		mToggle.GetComponent<UIButton>().isEnabled = false;
		
	}	

	void Indisponibilizar ()
	{
		mToggle.GetComponent<UIButton>().isEnabled = true;
		mToggle.Set(false);
	}

	void Disponibilizar ()
	{
		mToggle.GetComponent<UIButton>().isEnabled = true;
		mToggle.Set(true);
	}

}
