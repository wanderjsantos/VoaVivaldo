using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BotaoFase : MonoBehaviour {

	public int 			selecionarLevel = 0;
	
	
	public BoxCollider		mCollider;
	
	public GameObject 	botaoFesta;

	public void SetValue (bool liberado, bool festaLiberada)
	{
//		Debug.Log(gameObject.name + " liberado: " + liberado );
		
		
		if( liberado ) 	gameObject.GetComponentInChildren<UI2DSpriteAnimation>().Play("idle");// = liberado;
		else 			gameObject.GetComponentInChildren<UI2DSpriteAnimation>().Pause();// = liberado;
		
		botaoFesta.SetActive( festaLiberada );
		
		gameObject.GetComponent<UIButton>().isEnabled = liberado;
	}
	
	public void Update()
	{
//		if( Application.isPlaying == false )
			UpdateCollider();
	}
	
	Vector3 size;
	Vector3 center;
	public Vector3 centerOffset;
	public void UpdateCollider()
	{
		if( mCollider == null ) return;
	
		Bounds b = new Bounds();
		size = new Vector3( Screen.width, Screen.height, gameObject.collider.bounds.size.z );
		center = centerOffset;
		
		mCollider.size = size;
		mCollider.center = center;
		
//		Debug.Log("Min: " + b.min + " Max: " + b.max);
//		mCollider.bounds.Encapsulate( b );
	}
	
}
