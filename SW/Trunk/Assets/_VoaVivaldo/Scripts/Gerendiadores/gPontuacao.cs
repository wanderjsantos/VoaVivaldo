using UnityEngine;
using System.Collections;

public class gPontuacao : MonoBehaviour {

	public static gPontuacao s;

	public int pontosPorNota = 100;

	void Awake()
	{
		s = this;
	}

	public bool VerificarPontuacao( Nota n, Player p )
	{
//		Debug.Log ("Timbre - " + n.mInfo.timbre + "(" + (int)n.mInfo.timbre + ") - player- " + p.mController.pista);
		if( (int) n.mInfo.timbre == p.mController.pista )
		{
			p.mInfo.pontuacao += pontosPorNota;
			return true;
		}

		return false;
	}


}
