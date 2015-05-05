using UnityEngine;
using System.Collections;

public class Paginator : MonoBehaviour {

	public UIPaginator uiPaginator;
	
	public float width;
	
	public void Start()
	{
		width = uiPaginator.transform.GetChild(0).GetChild(0).GetComponent<UI2DSprite>().localSize.x;
	}
	
	public void Init()
	{
//		uiPaginator.OrdenarPaginas( new Vector2(width,Screen.height));
	}
	
}
