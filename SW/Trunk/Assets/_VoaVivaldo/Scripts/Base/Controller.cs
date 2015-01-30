using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{

	public Vector3 	input;
	Vector2 	minMaxInput = new Vector2(-1f,1f);
	public Vector2 	minMaxInputOnDevice = new Vector2(-1f,1f);
	public float multiplicadorDeAceleracao = 1.3f;

	Vector3	initialPosition;

	public Vector3  pos;

	int 			faixasTotais = 5;

	int		faixaAnterior;
	int		faixaAtual = 3;
	float	porFaixa = 0f;

	public Vector2	areaDeAtuacao = new Vector2 (60f, -60f);

	public float	speed = 30f;

	public int 		pista;

	void Start()
	{
		initialPosition = transform.localPosition;
		faixasTotais 	= gPista.s.faixasTotais;
		porFaixa = (Mathf.Abs (minMaxInput.y) + Mathf.Abs (minMaxInput.x))/faixasTotais;
	}

//	public void OnGUI()
//	{
//		GUI.Box (new Rect (0, 0, 200, 50), Input.acceleration.ToString());
//		GUI.Box (new Rect (0, 50, 200,50), (Input.acceleration * multiplicadorDeAceleracao).ToString());
//	}

	public void Update()
	{
#if UNITY_EDITOR
		input.y = Mathf.Clamp( Input.GetAxis ("Vertical"), minMaxInput.x, minMaxInput.y ); 

#else
		float y = Mathf.Clamp (Input.acceleration.y * multiplicadorDeAceleracao, minMaxInputOnDevice.x, minMaxInputOnDevice.y); 
		input.y = y / Mathf.Abs (minMaxInputOnDevice.x);
#endif


		faixaAtual = (int)((input.y / porFaixa) - (input.y % porFaixa));

		if (faixaAnterior != faixaAtual) 
		{
			gameObject.SendMessage("AnimarPersonagem",SendMessageOptions.DontRequireReceiver);
			faixaAnterior = faixaAtual;
		}

		pos = transform.localPosition ;
		pos.y = initialPosition.y + (faixaAtual * 30f);

		//a faixa ( de -2 a 2 ) + 2 (o numero extra ai) + 1 ( pra nao ir de 0 a 4 )
		pista = faixaAtual + 2 + 1;
//		transform.localPosition = pos;
		
	}


}
