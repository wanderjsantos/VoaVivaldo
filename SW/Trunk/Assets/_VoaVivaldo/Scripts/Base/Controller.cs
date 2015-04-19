using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public Vector3 	input;
	public Vector2 	minMaxInput = new Vector2(-1f,1f);
	public Vector2 	minMaxInputOnDevice = new Vector2(-1f,1f);
	public Vector2 init;
	public Vector2 	minMaxTemp = new Vector2(-1f,1f);

	Vector3	initialPosition;

	public Vector3  pos;

	int 			faixasTotais = 5;

	int		faixaAnterior;
	int		faixaAtual = 3;
	float	porFaixa = 0f;

	public float	speed = 30f;

	public int 		pista;
	
	public float	aceleracaoInicial;
	public float	aceleracaoAtual;
	float			direcao;
	void Start()
	{
		init = minMaxInputOnDevice;
		initialPosition = transform.localPosition;
		initialPosition.y = (faixaAtual * gPista.s.tamanhoDeCadaPista) - (gPista.s.tamanhoDeCadaPista/2);
		faixasTotais 	= gPista.s.faixasTotais;
		porFaixa = (Mathf.Abs (minMaxInput.y) + Mathf.Abs (minMaxInput.x))/faixasTotais;
		faixaAnterior = -1;
	}

	public void SetInitialPosition ()
	{
		minMaxInputOnDevice = init;
	
#if UNITY_IOS || UNITY_ANDROID
		aceleracaoInicial = Input.acceleration.y;
		
		minMaxTemp.x = minMaxInputOnDevice.x - Mathf.Abs(aceleracaoInicial);
		minMaxTemp.y = minMaxInputOnDevice.y - Mathf.Abs(aceleracaoInicial);
		
#endif
		if( Application.isEditor )
		{
				aceleracaoInicial = Input.GetAxis ("Vertical");
				minMaxTemp.x = minMaxInput.x + aceleracaoInicial;
				minMaxTemp.y = minMaxInput.y - aceleracaoInicial;
		 }
		
		
		Vector2 extra = new Vector2(0f,0f);
		if( Mathf.Abs( minMaxTemp.x ) > 1f )
		{
			minMaxTemp.x = -1f;
			extra.x = Mathf.Abs( minMaxTemp.x ) - 1f;
			minMaxTemp.y = minMaxTemp.y - extra.x;
		}
		if( Mathf.Abs( minMaxTemp.y ) > 1f )
		{
			minMaxTemp.y = 1f;
			extra.y = Mathf.Abs( minMaxTemp.y ) - 1f;
			minMaxTemp.x = minMaxTemp.x + extra.y;
		}
#if UNITY_IOS || UNITY_ANDROID

		minMaxInputOnDevice = minMaxTemp;

#endif
		if( Application.isEditor ) minMaxInput = minMaxTemp;
		
		
	}
	
	void OnGUI()
	{
		GUI.Box( new Rect( 0f, 0, 400f, 60f ), "Input.acceleration" + Input.acceleration.ToString() );
		GUI.Box( new Rect( 0f, 60f, 400f, 60f ), "minMaxInput" + minMaxInputOnDevice.ToString() );
		GUI.Box( new Rect( 0f, 120f, 400f, 60f ), "faixaAtual" +faixaAtual.ToString() );
		GUI.Box( new Rect( 0f, 180f, 400f, 60f ), "direcao" +direcao.ToString() );
	}

	public void Update()
	{

		float y =0f;
#if UNITY_IOS || UNITY_ANDROID
		aceleracaoAtual = Input.acceleration.y;
		direcao = aceleracaoInicial + aceleracaoAtual;
		y = Mathf.Clamp (direcao, minMaxInputOnDevice.x, minMaxInputOnDevice.y); 
		input.y = y / Mathf.Abs (minMaxInputOnDevice.x);
#endif

		if( Application.isEditor )
		{
			aceleracaoAtual = Input.GetAxis ("Vertical");
			direcao = aceleracaoInicial + aceleracaoAtual;
			
			y = Mathf.Clamp (direcao, minMaxInput.x, minMaxInput.y); 
			input.y = y / Mathf.Abs (minMaxInput.x);
		}

		faixaAtual = Mathf.RoundToInt((input.y / porFaixa) - (input.y % porFaixa));

		if (faixaAnterior != faixaAtual) 
		{
			gameObject.SendMessage("AnimarPersonagem",SendMessageOptions.DontRequireReceiver);
			faixaAnterior = faixaAtual;
		}

		
		pista = Mathf.Clamp(faixaAtual + ((int)(faixasTotais/2)), 1, faixasTotais );
		
		pos = transform.localPosition ;
		pos.y = gPista.s.GetPositionDaPista (pista).y;
	}


}
