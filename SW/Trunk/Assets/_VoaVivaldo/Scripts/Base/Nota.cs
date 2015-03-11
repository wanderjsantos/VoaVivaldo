using UnityEngine;
using System.Collections;

public class Nota : MonoBehaviour
{
	public NotaInfo 	mInfo;
	public NotaView		mView;

	public Color 		cor;

	public bool			kill = false;

	public void Start()
	{
		mView.UpdateColor (cor);
		kill = false;
	}

}

public enum TipoDeNota { NOTA = 1, PAUSA = 0 }
public enum Timbre { ZERO, UM, DOIS, TRES, QUATRO, CINCO,SEIS, SETE, OITO, NOVE, DEZ, ONZE, DOZE, TREZE, QUATORZE }
public enum Duracao{ SEMIBREVE = 1, MINIMA = 2 , SEMINIMA = 4, COLCHEIA = 8, SEMICOLCHEIA = 16, FUSA = 32, SEMIFUSA = 64 }

[System.Serializable]
public class NotaInfo 
{
	public TipoDeNota		tipo 	= TipoDeNota.NOTA;
	public Timbre			timbre = Timbre.SETE;
	public Duracao  		duracao = Duracao.SEMINIMA;
	public int				compasso;	
	public float 				batida;
	
	public NotaInfo()
	{
		timbre = Timbre.SETE;
		duracao = Duracao.SEMINIMA;
		compasso = -1;
		batida = -1;
	}

}

[System.Serializable]
public class NotaView
{
//	public UISprite mSprite;

	public void UpdateColor (Color cor)
	{
//		mSprite.color = cor;
	}
}


