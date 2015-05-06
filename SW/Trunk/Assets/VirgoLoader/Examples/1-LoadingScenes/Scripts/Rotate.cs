using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	
	public float speed = 30f;
	
	public void Update()
	{
		transform.Rotate( Vector3.up, speed * Time.deltaTime );	
		transform.Rotate( Vector3.forward, speed * Time.deltaTime );	
	}
}
