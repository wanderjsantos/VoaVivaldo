using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UICenterOnChild))]
public class UIPageScroll : MonoBehaviour 
{
	public UICenterOnChild 	centerOnChild;
	
	public UIPaginator 	paginator;
	
	public Transform	lastParent;
	
	void OnEnable()
	{
		if( centerOnChild == null ) centerOnChild = GetComponent<UICenterOnChild>();	
		centerOnChild.onFinished 	+= OnFinished;
		centerOnChild.onCenter 		+= OnCenter;
	}
	
	void OnDisable()
	{
		if( centerOnChild == null ) centerOnChild = GetComponent<UICenterOnChild>();			
		centerOnChild.onFinished 	-= OnFinished;
		centerOnChild.onCenter 		-= OnCenter;
	}
	
	public void OnCenter(GameObject go)
	{
//		Debug.Log("OnCenter: " + go.name);
		lastParent = go.transform.parent;
		UpdateMarkers(go);
	}

	void UpdateMarkers (GameObject selecionado)
	{
		if( lastParent == null || selecionado.transform.IsChildOf(lastParent) == false ) return;
		int childIndex = -1;
		
		childIndex = selecionado.transform.GetSiblingIndex();
//		Debug.Log("Child : " + childIndex );
		
		if( childIndex == -1 ) return;
		
		Select( childIndex );
		
	}

	void Select (int childIndex)
	{
		transform.GetChild( childIndex ).GetComponent<UIToggle>().value = true;
	}
	
	public void OnFinished()
	{
//		Debug.Log("OnFinished");
	}
	
	public void Center( GameObject go )
	{
		paginator.centerOnChild.CenterOn( go.transform );
	}
}
