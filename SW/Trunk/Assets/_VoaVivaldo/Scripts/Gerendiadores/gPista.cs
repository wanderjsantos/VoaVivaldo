using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gPista : MonoBehaviour {

	public GameObject rootPista;
	public GameObject pistaBase;

	public int		faixasTotais = 5;

	public float	tamanhoYPista = 120f;
	public float	tamanhoDeCadaPista = 10f;
	
	int		ultimaPista = -1;

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
		gGame.onPauseGame += OnPause;
	}
	
	void OnDisable()
	{
		gComandosDeMusica.onPlay -= PosicionarTodasAsNotas;
		gGame.onReset-= Resetar;
		gGame.onPauseGame -= OnPause;
	}

	void OnPause (bool pausado)
	{
		
	}

	Vector3 posInicialPista;
	void Resetar ()
	{
		rootPista.transform.localPosition = posInicialPista;
		ultimaPista = -1;
		ApagarTodasAsPistas();
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
	
	public Transform GetPista(int qualPista )
	{
		Transform ret;
		
		switch (qualPista)
		{
		case 1:
			ret = pista1.transform ;
			break;
		case 2:
			ret = pista2.transform ;
			break;
		case 3:
			ret = pista3.transform ;
			break;
		case 4:
			ret = pista4.transform ;
			break;
		case 5:
			ret = pista5.transform ;
			break;	
		case 6:
			ret = pista6.transform ;
			break;
		case 7:
			ret = pista7.transform ;
			break;
		case 8:
			ret = pista8.transform ;
			break;
		case 9:
			ret = pista9.transform ;
			break;
		case 10:
			ret = pista10.transform ;
			break;
		case 11:
			ret = pista11.transform ;
			break;
		case 12:
			ret = pista12.transform ;
			break;
		case 13:
			ret = pista13.transform ;
			break;
		case 14:
			ret = pista14.transform ;
			break;
		default:
			ret = pista14.transform ;
			break;
		}
		
		return ret;
	}
	

	public void FeedbackPista(int qualPista)
	{		
		UISprite sp = GetPista( qualPista ).GetComponent<UISprite>();
		
		ApagarTodasAsPistas();
		
		if( gGame.s.player.mController.pista == qualPista )
		{
			sp.alpha = .8f;			
			return;
		}
		
		sp.alpha = .1f;
	}
	
	void ApagarTodasAsPistas ()
	{
		foreach(UISprite sp in pistaBase.GetComponentsInChildren<UISprite>(true) as UISprite[] )
		{
			sp.alpha = .1f;
		}
	}


	public float tamanhoDoCompasso = 500f;
	public void PosicionarTodasAsNotas()
	{
		posInicialPista = rootPista.transform.localPosition;

		List<Trecho> currentTrecho = gMusica.s.musicaAtual.mInfo.mData.instrumentos[gMusica.s.instrumentoIndice].trechos;
							
		foreach( Trecho trecho in currentTrecho )
		{
			foreach( Compasso compasso in trecho.compassos )
			{
				float tamanhoTotal = 0f;					
				for( int i = 0; i < compasso.notas.Count; i ++  )
				{	
									
					float posXCompasso 	= tamanhoDoCompasso * compasso.notas[i].compasso;
					float posicaoX 		= posXCompasso + tamanhoTotal;
					
					float tamanhoX 		= tamanhoDoCompasso / (float)compasso.notas[i].duracao;
					tamanhoTotal 		+= tamanhoX;
					
					Vector3 v = new Vector3 (posicaoX,0,0);//, posicaoY-(tamanhoYPista/2) + pistaBase.transform.localPosition.y, 0);
					switch (compasso.notas[i].timbre )
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
					
					Nota n = gNotas.s.NovaNota (compasso.notas[i]);
					n.gameObject.transform.localScale = Vector3.one;
					n.gameObject.transform.localPosition = v;
					n.gameObject.transform.parent = rootPista.transform;
				
				}
				
						
				
			}
		}
		Debug.Break();

	}
}
