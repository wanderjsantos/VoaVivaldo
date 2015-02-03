using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gPista : MonoBehaviour {

	public GameObject rootPista;
	public GameObject pistaBase;

	public int		faixasTotais = 5;

	public float	tamanhoYPista = 120f;

	public GameObject	pista1;
	public GameObject	pista2;
	public GameObject	pista3;
	public GameObject	pista4;
	public GameObject	pista5;

	public static gPista s;

	void Awake()
	{
		s = this;

	}

	void OnEnable()
	{
		gComandosDeMusica.onPlay += PosicionarTodasAsNotas;
		EncontrarPistaBase ();
	}


	
	GameObject EncontrarPistaBase()
	{
		if (rootPista != null)
			return rootPista;
		
		GameObject ret = GameObject.FindGameObjectWithTag ("RootPista");
		if (ret == null)
			ret = new GameObject ("RootPista");
		return ret;
	}

//	void InitMusica ()
//	{
//		UpdatePista (0);
//		UpdatePista (1);
//		UpdatePista (2);
//	}

	public void UpdatePista (int compassoAtual)
	{
		List<NotaInfo> notasAtuais = gMusica.s.TodasAsNotasNoCompasso (compassoAtual+1);

		for( int i = 0; i < notasAtuais.Count; i++ )
		{
//			float tempoDentro = notasAtuais[i].noTempo - compassoAtual;
//			float posicaoX = (tempoDentro * tamanhoDoCompasso) + (tamanhoDoCompasso * compassoAtual); 
			float posicaoX = (tamanhoDoCompasso) + (tamanhoDoCompasso * compassoAtual); 
			float posicaoY = gNotas.s.PosicionEmY( notasAtuais[i].timbre, tamanhoYPista );

			Debug.Log("Nota: Posicao" + posicaoX + " - " + posicaoY );

			Vector3 v = new Vector3( posicaoX,posicaoY,0);

			Nota n = gNotas.s.NovaNota( notasAtuais[i] );
			n.gameObject.transform.parent = rootPista.transform;
			n.gameObject.transform.localScale = Vector3.one;

			n.gameObject.transform.localPosition = v;
		}
	}

	public float tamanhoDoCompasso = 500f;
	public void PosicionarTodasAsNotas()
	{
		List<NotaInfo> notasAtuais = new List<NotaInfo> ();
		notasAtuais.AddRange( gMusica.s.musicaAtual.mInfo.notas );

		for (int i = 0; i < notasAtuais.Count; i++)
		{
			float extraX = ((float)( (notasAtuais[i].batida - 1f) /gRitmo.s.batidasPorCompasso) * tamanhoDoCompasso );
			float posicaoX = ((tamanhoDoCompasso * (notasAtuais[i].compasso-1)) + extraX) ;
//			float posicaoY = gNotas.s.PosicionEmY (notasAtuais [i].timbre, tamanhoYPista);
//			Debug.Log ("Nota: Posicao" + posicaoX + "Extra x : " + extraX + " - " + posicaoY);
	
			Vector3 v = new Vector3 (posicaoX,0,0);//, posicaoY-(tamanhoYPista/2) + pistaBase.transform.localPosition.y, 0);
			switch (notasAtuais[i].timbre)
			{
			case Timbre.UM:
				v.y = pista1.transform.localPosition.y;
				break;
			case Timbre.DOIS:
				v.y = pista2.transform.localPosition.y;
				break;
			case Timbre.TRES:
				v.y = pista3.transform.localPosition.y;
				break;
			case Timbre.QUATRO:
				v.y = pista4.transform.localPosition.y;
				break;
			case Timbre.CINCO:
				v.y = pista5.transform.localPosition.y;
				break;
			default:
				v.y = pista1.transform.localPosition.y;
				break;
			}

			Nota n = gNotas.s.NovaNota (notasAtuais [i]);
			n.gameObject.transform.localScale = Vector3.one;
			n.gameObject.transform.localPosition = v;
			n.gameObject.transform.parent = rootPista.transform;
		}

	}
}
