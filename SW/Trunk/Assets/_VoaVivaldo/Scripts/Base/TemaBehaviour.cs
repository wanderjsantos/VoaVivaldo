using UnityEngine;
using System.Collections;

public enum QualCorDoTema{ BACKGROUND, CLARO, ESCURO, TEXTO }
public class TemaBehaviour : MonoBehaviour 
{
	UIWidget mWidget;
	
	public QualCorDoTema aplicar;
	
	void Awake()
	{
		mWidget = GetComponent<UIWidget>();
		
	}
	
	public void OnEnable()
	{
		gTemas.onChange += MudarCor;
		
		MudarCor();
	}
	
	public void Disable()
	{
		gTemas.onChange -= MudarCor;
	}

	void MudarCor ()
	{
		if( gTemas.s.usarTemas == false ) return;
		mWidget.color = gTemas.s.GetCor( aplicar );
	}
}
