using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public Vector3 	input;
	Vector2 	minMaxInput = new Vector2(-1f,1f);
	public Vector2 	minMaxInputOnDevice = new Vector2(-1f,1f);

	Vector3	initialPosition;

	public Vector3  pos;

	int 			faixasTotais = 5;

	int		faixaAnterior;
	int		faixaAtual = 3;
//	float 	faixaAtualFloat;
	float	porFaixa = 0f;

	public float	speed = 30f;

	public int 		pista;

	void Start()
	{
//		initialPosition = transform.localPosition;
		initialPosition = transform.localPosition;
		initialPosition.y = (faixaAtual * gPista.s.tamanhoDeCadaPista) - (gPista.s.tamanhoDeCadaPista/2);
		faixasTotais 	= gPista.s.faixasTotais;
		porFaixa = (Mathf.Abs (minMaxInput.y) + Mathf.Abs (minMaxInput.x))/faixasTotais;
		faixaAnterior = -1;
	}

	public void Update()
	{


#if UNITY_IOS || UNITY_ANDROID
		float y = Mathf.Clamp (Input.acceleration.y, minMaxInputOnDevice.x, minMaxInputOnDevice.y); 
		input.y = y / Mathf.Abs (minMaxInputOnDevice.x);
#endif
#if !UNITY_IOS && !UNITY_ANDROID
		float y = Mathf.Clamp (Input.GetAxis ("Vertical"), minMaxInput.x, minMaxInput.y); 
		input.y = y / Mathf.Abs (minMaxInput.x);
#endif
		faixaAtual = (int)((input.y / porFaixa) - (input.y % porFaixa));
//		faixaAtualFloat = ((input.y / porFaixa) - (input.y % porFaixa));

		if (faixaAnterior != faixaAtual) 
		{
			gameObject.SendMessage("AnimarPersonagem",SendMessageOptions.DontRequireReceiver);
			faixaAnterior = faixaAtual;
		}

		pos = transform.localPosition ;
		pos.y = gPista.s.GetPositionDaPista (faixaAtual).y;

		//a faixa ( de -2 a 2 ) + 2 (o numero extra ai) + 1 ( pra nao ir de 0 a 4 )
		pista = faixaAtual + ((int)(faixasTotais/2));
//		transform.localPosition = pos;
	}


}
