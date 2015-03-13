using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public PlayerInfo 	mInfo;
	
	public vPersonagem 	vPlayer;
	
	public Controller 	mController;
	public bool			anim = false;
	
	Vector3 refVel;	
	public float smoothAnimation  = .7f;

	void Awake()
	{
		if (mController == null)
						mController = GetComponent<Controller> ();

	}

	void Start()
	{
		gGame.s.player = this;
	}

	public void AnimarPersonagem()
	{
		anim = true;
	}

	void Update()
	{
		if (anim)
						DoAnimation ();
	}
	public void DoAnimation()
	{
		transform.localPosition = Vector3.SmoothDamp( transform.localPosition, mController.pos, ref refVel, smoothAnimation );
		if( (mController.pos - transform.localPosition).magnitude <= .01f )
			anim = false;
	}

	public void Disable ()
	{
		mController.enabled = false;
	}

	public void Enable()
	{
		mController.enabled = true;
	}
}

public enum QualPersonagem{ TRUMPET, HORNET, SANFONA, FLAUTA }

[System.Serializable]
public class PlayerInfo
{
	public int 				pontuacao;
	public QualPersonagem 	meuPersonagem;
}
