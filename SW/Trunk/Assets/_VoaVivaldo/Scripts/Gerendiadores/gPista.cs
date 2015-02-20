using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gPista : MonoBehaviour {

	public GameObject rootPista;
	public GameObject pistaBase;

	public int		faixasTotais = 5;

	public float	tamanhoYPista = 120f;
	public float	tamanhoDeCadaPista = 10f;

	public GameObject	pista1;
	public GameObject	pista2;
	public GameObject	pista3;
	public GameObject	pista4;
	public GameObject	pista5;
	public GameObject	pista6;
	public GameObject	pista7;
	public GameObject	pista8;
	public GameObject	pista9;
	public GameObject	pista10;
	public GameObject	pista11;
	public GameObject	pista12;
	public GameObject	pista13;
	public GameObject	pista14;

	public static gPista s;

	void Awake()
	{
		s = this;

	}

	void OnEnable()
	{
		gComandosDeMusica.onPlay += PosicionarTodasAsNotas;
		gGame.onReset += Resetar;
		EncontrarPistaBase ();
	}
	
	void OnDisable()
	{
		gComandosDeMusica.onPlay -= PosicionarTodasAsNotas;
		gGame.onReset-= Resetar;
	}

	Vector3 posInicialPista;
	void Resetar ()
	{
		rootPista.transform.localPosition = posInicialPista;
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

	public Vector3 GetPositionDaPista (int faixaAtual)
	{
		int pAtual = faixaAtual;// + ((int)(faixasTotais/2));

		Vector3 v = new Vector3 ();
		switch (pAtual)
		{
		case 1:
			v.y = pista1.transform.localPosition.y;
			break;
		case 2:
			v.y = pista2.transform.localPosition.y;
			break;
		case 3:
			v.y = pista3.transform.localPosition.y;
			break;
		case 4:
			v.y = pista4.transform.localPosition.y;
			break;
		case 5:
			v.y = pista5.transform.localPosition.y;
			break;	
		case 6:
			v.y = pista6.transform.localPosition.y;
			break;
		case 7:
			v.y = pista7.transform.localPosition.y;
			break;
		case 8:
			v.y = pista8.transform.localPosition.y;
			break;
		case 9:
			v.y = pista9.transform.localPosition.y;
			break;
		case 10:
			v.y = pista10.transform.localPosition.y;
			break;
		case 11:
			v.y = pista11.transform.localPosition.y;
			break;
		case 12:
			v.y = pista12.transform.localPosition.y;
			break;
		case 13:
			v.y = pista13.transform.localPosition.y;
			break;
		case 14:
			v.y = pista14.transform.localPosition.y;
			break;
		default:
			v.y = pista14.transform.localPosition.y;
			break;
		}

		return v;
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
		posInicialPista = rootPista.transform.localPosition;
		
		List<NotaInfo> notasAtuais = new List<NotaInfo> ();
		notasAtuais.AddRange( gMusica.s.musicaAtual.mInfo.mData.instrumentos[gMusica.s.instrumentoIndice].notas );

		for (int i = 0; i < notasAtuais.Count; i++)
		{
			float extraX = ((float)( (notasAtuais[i].batida - 1f) /gRitmo.s.batidasPorCompasso) * tamanhoDoCompasso );
			float posicaoX = ((tamanhoDoCompasso * (notasAtuais[i].compasso)) + extraX) ;
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
			case Timbre.SEIS:
				v.y = pista6.transform.localPosition.y;
				break;
			case Timbre.SETE:
				v.y = pista7.transform.localPosition.y;
				break;
			case Timbre.OITO:
				v.y = pista8.transform.localPosition.y;
				break;
			case Timbre.NOVE:
				v.y = pista9.transform.localPosition.y;
				break;
			case Timbre.DEZ:
				v.y = pista10.transform.localPosition.y;
				break;
			case Timbre.ONZE:
				v.y = pista11.transform.localPosition.y;
				break;
			case Timbre.DOZE:
				v.y = pista12.transform.localPosition.y;
				break;
			case Timbre.TREZE:
				v.y = pista13.transform.localPosition.y;
				break;
			case Timbre.QUATORZE:
				v.y = pista14.transform.localPosition.y;
				break;
			default:
				v.y = pista1.transform.localPosition.y;
				break;
			}

			Nota n = gNotas.s.NovaNota (notasAtuais [i]);
			n.gameObject.transform.localScale = Vector3.one;
			n.gameObject.transform.localPosition = v;
			n.gameObject.transform.parent = rootPista.transform;
			
//			Debug.Break();
		}

	}
}
