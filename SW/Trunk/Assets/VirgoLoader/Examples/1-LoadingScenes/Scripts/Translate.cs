using UnityEngine;
using System.Collections;

public class Translate : MonoBehaviour {
	
	
	public float speed = 30f;
	public float direction = -1;
	public float timeToChange = 2f;
	
	void Start()
	{
		InvokeRepeating( "ChangeDirection", timeToChange, timeToChange );
	}
	
	public void Update()
	{
		transform.Translate( (Vector3.up * direction) * ( speed * Time.deltaTime ) );	
	}
	
	void ChangeDirection()
	{
		direction = direction * -1;
	}
}
