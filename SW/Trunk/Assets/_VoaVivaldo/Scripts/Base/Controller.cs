using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public float 	InputY;
	
	public float	inputAccelerationY;	
	public float	rotacaoDoDevice;
	
	public float 	multiplicadorRotacao = .5f;
	
	//MAximo e minimo atuais
	public Vector2 MinMax;
	
	public Vector3  pos;
	int 			faixasTotais = 5;

	int		faixaAnterior;
	int		faixaAtual = 3;
	float	porFaixa = 0f;
	public 	int 		pista;
	
	void Start()
	{
		
		Init();
	}

	float GetAccelerationY ()
	{
		if( Application.isEditor ) 
			return Input.GetAxis("Vertical");
		else
			return Input.gyro.rotationRate.x;
	}
	
	public void Init()
	{
		if( Application.isEditor == false )
			rotacaoDoDevice = GetAccelerationY();
			
		Input.gyro.enabled = true;
		
		faixaAtual = (int)(gPista.s.faixasTotais/2);
		faixasTotais = gPista.s.faixasTotais;		
		faixaAnterior = -1;
		
		porFaixa = gPista.s.tamanhoDeCadaPista;
		
		pos = Vector3.zero;
		pos.y = gPista.s.GetPista( (int)(gPista.s.faixasTotais/2) ).position.y;
	}
	
	
//	public void OnGUI()
//	{
//		
//		GUI.Box( new Rect( 30f, 30f, 400f, 30f  ), "RotacaoDoDevice        :  " + rotacaoDoDevice);
//		GUI.Box( new Rect( 30f, 60f, 400f, 30f  ), "Inputacceleration      :  " + inputAccelerationY);
//		GUI.Box( new Rect( 30f, 90f, 400f, 30f  ), "FaixaAtual             :  " + faixaAtual);
//		GUI.Box( new Rect( 30f, 120f, 400f, 30f ), "InputY                 :  " + InputY);
//		GUI.Box( new Rect( 30f, 150f, 400f, 30f ), "porcentagemNoInput     :  " + (InputY * 100)  /( Mathf.Abs(MinMax.x) + Mathf.Abs( MinMax.y ) ));
//		GUI.Box( new Rect( 30f, 180f, 400f, 30f ), "faixas Adicionais      :  " + (int) ( faixasTotais * ( porcentagemNoInput / 100f )));
//	}

	public void SetInitialPosition ()
	{
		Init();
	}
	
	float porcentagemNoInput;
	int adicionalAFaixa;
	
	public void Update()
	{
		//		float y =0f;
		//#if UNITY_IOS || UNITY_ANDROID
		//		aceleracaoAtual = Input.acceleration.y;
		//		direcao = aceleracaoAtual - aceleracaoInicial;
		//		y = Mathf.Clamp (direcao, minMaxInputOnDevice.x, minMaxInputOnDevice.y); 
		//		input.y = y ;/// Mathf.Abs (minMaxInputOnDevice.x);
		//#endif
	
		rotacaoDoDevice = GetAccelerationY() * multiplicadorRotacao * Time.deltaTime ;
		inputAccelerationY += rotacaoDoDevice;
		
		InputY = Mathf.Clamp( inputAccelerationY, MinMax.x, MinMax.y );
		porcentagemNoInput = (InputY * 100f)  /( Mathf.Abs(MinMax.x) + Mathf.Abs( MinMax.y ) );
		
		faixaAtual = (-1 * ((int) ( faixasTotais * ( porcentagemNoInput / 100f ) )));
		
//		faixaAtual = Mathf.Clamp( Mathf.RoundToInt((InputY / porFaixa) - (InputY % porFaixa)), -7, 7) ;

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
