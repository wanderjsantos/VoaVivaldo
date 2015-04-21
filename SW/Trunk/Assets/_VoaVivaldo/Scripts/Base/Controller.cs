using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public float 	InputY;
	
	public float	inputAccelerationY;
	
	//Quanto do Input.acceleration estarei usando
	//Acceleration vai de 1/-1  entao .5f = .5f/-.5f (metade do uso)
	public float	usoDoEixo = .5f;
	
	//Aceleracao que devo considerar como sendo o ponto inicial (pista central)
	public float	accNeutra;
	
	//MAximo e minimo atuais
	public Vector2 MinMax;
		
//	public Vector2 	minMaxInput = new Vector2(-1f,1f);
//	public Vector2 	minMaxInputOnDevice = new Vector2(-1f,1f);
//	public Vector2 init;
//	public Vector2 	minMaxTemp = new Vector2(-1f,1f);

//	Vector3	initialPosition;
	
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
			return Input.acceleration.y;
	}
	
	public void Init()
	{
		if( Application.isEditor == false )
			accNeutra = GetAccelerationY();
		
		faixasTotais = gPista.s.faixasTotais;
		
		MinMax.x = accNeutra - usoDoEixo;
		MinMax.y = accNeutra + usoDoEixo;
		
		//Pego o tamanho total que eu uso no acelerometro ( 2 = -1/1 possivel )
		//divido pelo total de faixas
		porFaixa = (usoDoEixo*2)/faixasTotais;
		
		faixaAnterior = -1;
	}

	public void SetInitialPosition ()
	{
		Init();
	}
	
//	void OnGUI()
//	{
//		GUI.Box( new Rect( 0f, 0, 400f, 60f ), "inicial: " + aceleracaoInicial.ToString("0.000") + "Atual: " + aceleracaoAtual.ToString("0.000") );
//		GUI.Box( new Rect( 0f, 60f, 400f, 60f ), "minMaxInput" + minMaxInputOnDevice.ToString() + " :: input.y: " + input.y);
//		GUI.Box( new Rect( 0f, 120f, 400f, 60f ), "faixaAtual" +faixaAtual.ToString() + " :: porFaixa: "+ porFaixa.ToString("0.000") );
//		GUI.Box( new Rect( 0f, 180f, 400f, 60f ), "direcao" +direcao.ToString() );
//		GUI.Box( new Rect( 0f, 240f, 400f, 60f ), "Input.y" +input.y.ToString() );
//	}

	
	public void Update()
	{
		//		float y =0f;
		//#if UNITY_IOS || UNITY_ANDROID
		//		aceleracaoAtual = Input.acceleration.y;
		//		direcao = aceleracaoAtual - aceleracaoInicial;
		//		y = Mathf.Clamp (direcao, minMaxInputOnDevice.x, minMaxInputOnDevice.y); 
		//		input.y = y ;/// Mathf.Abs (minMaxInputOnDevice.x);
		//#endif
	
		inputAccelerationY = GetAccelerationY();		
		
		InputY = Mathf.Clamp( inputAccelerationY , MinMax.x, MinMax.y );
		
		faixaAtual = Mathf.RoundToInt((InputY / porFaixa) - (InputY % porFaixa));

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
