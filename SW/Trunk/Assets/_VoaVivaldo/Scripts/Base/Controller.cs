using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public Vector3 	input;
	public Vector2 	minMaxInput = new Vector2(-1f,1f);
	public Vector2 	minMaxInputOnDevice = new Vector2(-1f,1f);

	Vector3	initialPosition;

	public Vector3  pos;

	int 			faixasTotais = 5;

	int		faixaAnterior;
	int		faixaAtual = 3;
	float	porFaixa = 0f;

	public float	speed = 30f;

	public int 		pista;

	void Start()
	{

		initialPosition = transform.localPosition;
		initialPosition.y = (faixaAtual * gPista.s.tamanhoDeCadaPista) - (gPista.s.tamanhoDeCadaPista/2);
		faixasTotais 	= gPista.s.faixasTotais;
		porFaixa = (Mathf.Abs (minMaxInput.y) + Mathf.Abs (minMaxInput.x))/faixasTotais;
		faixaAnterior = -1;
	}
	
	void OnGUI()
	{
//		GUI.Box( new Rect( 0f, 0f, 100f, 100f ), Input.acceleration.ToString() );
//		GUI.Box( new Rect( 0f, 0f, 100f, 100f ), input.ToString() );
//		GUI.Box( new Rect( 0f, 0f, 100f, 100f ), faixaAtual.ToString() );
	}

	public void Update()
	{


#if UNITY_IOS || UNITY_ANDROID
		float y = Mathf.Clamp (Input.acceleration.y, minMaxInputOnDevice.x, minMaxInputOnDevice.y); 
		input.y = y / Mathf.Abs (minMaxInputOnDevice.x);
#endif

		if( Application.isEditor )
		{
			y = Mathf.Clamp (Input.GetAxis ("Vertical"), minMaxInput.x, minMaxInput.y); 
			input.y = y / Mathf.Abs (minMaxInput.x);
		}

		faixaAtual = Mathf.RoundToInt((input.y / porFaixa) - (input.y % porFaixa));

		if (faixaAnterior != faixaAtual) 
		{
			gameObject.SendMessage("AnimarPersonagem",SendMessageOptions.DontRequireReceiver);
			faixaAnterior = faixaAtual;
		}

		
//		pista = 
		pista = Mathf.Clamp(faixaAtual + ((int)(faixasTotais/2)), 1, faixasTotais );
		
		pos = transform.localPosition ;
		pos.y = gPista.s.GetPositionDaPista (pista).y;
	}


}
