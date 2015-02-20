using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gNotas : MonoBehaviour 
{
	public static gNotas s;

	public List<Nota> todasAsNotas;
	public List<Nota>notasNaPista;

	public bool drawAreaDePontuacao = false;
	public Rect areaDePontuacao;

	public bool drawAreaDeDead = false;
	public Rect areaDeDead;

	public int verificarXNotasPorVez = 1;

	public int currentNota = 0;

	public void Awake()
	{
		s = this;
	}

	void OnEnable()
	{
		gComandosDeMusica.onPlay += Init;
		gGame.onReset += Resetar;
	}
	
	void OnDisable()
	{
		gComandosDeMusica.onPlay -= Init;
		gGame.onReset -= Resetar;
	}

	void Resetar ()
	{
		if( notasNaPista.Count > 0 )
		{
			notasNaPista.ForEach( delegate( Nota n ) 
			{
				Destroy( n.gameObject );
			});
		}
		notasNaPista = new List<Nota>();
		currentNota = 0;
	
	}

	void Init ()
	{
//		notasNaPista = new List<Nota> ();
		currentNota = 0;
	}

	void OnGUI()
	{
		if (drawAreaDePontuacao) 
		{
			GUI.Button( areaDePontuacao, "") ;
		}

		if (drawAreaDeDead) 
		{
			GUI.Button( areaDeDead, "") ;
		}

		if (gGame.s.gameStarted == false)
						return;

		int c = Mathf.Clamp (verificarXNotasPorVez, 1, notasNaPista.Count);

		for (int i = 0; i < c; i++) 
		{
			if( c > notasNaPista.Count )
			{
//				Debug.Log("Acabou as notas");
				return;
			}

			Vector3 posNota = UICamera.mainCamera.WorldToScreenPoint( notasNaPista[i].transform.position );
			if( areaDePontuacao.Contains( posNota )) 
			{
				if( gPontuacao.s.VerificarPontuacao( notasNaPista[i], gGame.s.player ) )
					DestruirNota(notasNaPista[i]);
			}

			if( areaDeDead.Contains( posNota )) 
			{
				DestruirNota( notasNaPista[i] );
			}
		}
	}
	
	public Nota NovaNota( NotaInfo info)
	{
		Nota ret = Instantiate (todasAsNotas.Find (e => e.mInfo.timbre == info.timbre)) as Nota;
		ret.gameObject.transform.parent = gPista.s.rootPista.transform;
		ret.mInfo = info;

		notasNaPista.Add (ret);

		return ret;
	}


	public float PosicionEmY( Timbre t, float tamanhoDaPista = 150f )
	{
		float tamanho = tamanhoDaPista / todasAsNotas.Count;

		int m = (int)todasAsNotas.Find (e => e.mInfo.timbre == t).mInfo.timbre;

		return m * tamanho;
	}

	void DestruirNota (Nota nota)
	{
		if (notasNaPista.Contains (nota))
						notasNaPista.Remove (nota);

		currentNota++;// = Mathf.Clamp(currentNota++,0,notasNaPista.Count);

		Destroy (nota.gameObject);
	}
}
