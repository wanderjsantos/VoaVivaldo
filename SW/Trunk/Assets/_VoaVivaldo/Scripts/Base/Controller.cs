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
		Init();
	}
	
	public void Init()
	{
		initialPosition = transform.localPosition;
//		initialPosition.y = (faixaAtual * gPista.s.tamanhoDeCadaPista) - (gPista.s.tamanhoDeCadaPista/2);
		initialPosition.y = (0 * gPista.s.tamanhoDeCadaPista) - (gPista.s.tamanhoDeCadaPista/2);
		faixasTotais 	= gPista.s.faixasTotais;
		if( Application.isEditor ) porFaixa = (Mathf.Abs (minMaxInput.y) + Mathf.Abs (minMaxInput.x))/faixasTotais;
		faixaAnterior = -1;
	}

	public void SetInitialPosition ()
	{
		Init();
	
		minMaxInputOnDevice = init;
	
#if UNITY_IOS || UNITY_ANDROID
		aceleracaoInicial = Input.acceleration.y;
		//-0.4 -  0.5 = -0.9
		minMaxTemp.x = minMaxInputOnDevice.x + aceleracaoInicial;
		//0.4 - 0.5 = -0.1
		minMaxTemp.y = minMaxInputOnDevice.y + aceleracaoInicial;
		

//		Vector2 extra = new Vector2(0f,0f);
//		if( Mathf.Abs( minMaxTemp.x ) < -1f )
//		{
//			minMaxTemp.x = -1f;
//			extra.x = Mathf.Abs( minMaxTemp.x ) - 1f;
//			minMaxTemp.y = minMaxTemp.y + extra.x;
//		}
//		if( Mathf.Abs( minMaxTemp.y ) > 1f )
//		{
//			minMaxTemp.y = 1f;
//			extra.y = Mathf.Abs( minMaxTemp.y ) - 1f;
//			minMaxTemp.x = minMaxTemp.x - extra.y;
//		}
		
		Debug.LogWarning( "MINMAX TEMP::" + minMaxTemp );
		
		minMaxInputOnDevice = minMaxTemp;
		
		porFaixa = (Mathf.Abs (minMaxInputOnDevice.y) + Mathf.Abs (minMaxInputOnDevice.x))/faixasTotais;

#endif
		
	}
	
	void OnGUI()
	{
		GUI.Box( new Rect( 0f, 0, 400f, 60f ), "inicial: " + aceleracaoInicial.ToString("0.000") + "Atual: " + aceleracaoAtual.ToString("0.000") );
		GUI.Box( new Rect( 0f, 60f, 400f, 60f ), "minMaxInput" + minMaxInputOnDevice.ToString() + " :: input.y: " + input.y);
		GUI.Box( new Rect( 0f, 120f, 400f, 60f ), "faixaAtual" +faixaAtual.ToString() + " :: porFaixa: "+ porFaixa.ToString("0.000") );
		GUI.Box( new Rect( 0f, 180f, 400f, 60f ), "direcao" +direcao.ToString() );
		GUI.Box( new Rect( 0f, 240f, 400f, 60f ), "Input.y" +input.y.ToString() );
	}

	public void Update()
	{

		float y =0f;
#if UNITY_IOS || UNITY_ANDROID
		aceleracaoAtual = Input.acceleration.y;
		direcao = aceleracaoAtual - aceleracaoInicial;
		y = Mathf.Clamp (direcao, minMaxInputOnDevice.x, minMaxInputOnDevice.y); 
		input.y = y ;/// Mathf.Abs (minMaxInputOnDevice.x);
#endif

//		if( Application.isEditor )
//		{
//			direcao = Input.GetAxis ("Vertical");
//			
//			y = Mathf.Clamp (direcao, minMaxInput.x, minMaxInput.y); 
//			input.y = y / Mathf.Abs (minMaxInput.x);
//		}

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
