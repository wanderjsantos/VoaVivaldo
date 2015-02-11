using UnityEngine;
using System.Collections;

public class gPontuacao : MonoBehaviour {

	public static gPontuacao s;

	public 	int pontosPorNota = 100;
	public	int	multiplicarPor = 2;
	public 	int aCadaXNotas = 5;

	public 	int	multiplicador = 1;
	public 	int	vezesMultiplicado = 1;

	public	int notasAcertadasNaSequencia = 0;

	void Awake()
	{
		s = this;
	}

	public bool VerificarPontuacao( Nota n, Player p )
	{
//		Debug.Log ("nota: " + ((int)n.mInfo.timbre).ToString () + " - pista: " + p.mController.pista);
		if( (int) n.mInfo.timbre == p.mController.pista )
		{

			p.mInfo.pontuacao += pontosPorNota * multiplicador ;
			notasAcertadasNaSequencia ++;
			if( notasAcertadasNaSequencia >= (aCadaXNotas * vezesMultiplicado) )
			{
				vezesMultiplicado ++;
				multiplicador = multiplicador * multiplicarPor;
			}
			return true;
		}

		notasAcertadasNaSequencia = 0;
		vezesMultiplicado = 1;
		multiplicador = 1;

		return false;
	}


}
