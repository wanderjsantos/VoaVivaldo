using UnityEngine;
using System.Collections;

[RequireComponent( typeof(UIGrid), typeof(UICenterOnChild) )]
public class UIPaginator : MonoBehaviour {

			bool 				repositionar = false;
	public 	UICenterOnChild 	centerOnChild;
	
	void Awake()
	{
		centerOnChild = GetComponent<UICenterOnChild>();
	}

	[ContextMenu("Ordenar Paginas")]
	public void OrdenarPaginas()
	{		
		repositionar = true;
		Reposicionar();
	}
	
	public void OrdenarPaginas(Vector2 size)
	{		
		repositionar = true;
		Reposicionar(size.x, size.y);
		
	}
	
	public void Reposicionar(float x = 0f, float y = 0f)
	{
		if( x == 0 ) x =Screen.width;
		if( y == 0 ) x =Screen.height;
	
		UIGrid grid = GetComponent<UIGrid>();
		grid.cellWidth =  x;		
		grid.cellHeight = y;
		
		grid.Reposition();
		repositionar = false;
	}
	
//	public void Update()
//	{
//		if( repositionar )
//		{
//			Debug.Log("w:" +Screen.width + " h:" + Screen.height );
//			
//			Reposicionar();			
//		}
//		
//	}
}
