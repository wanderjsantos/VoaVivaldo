using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public PlayerInfo 	mInfo;
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
}

[System.Serializable]
public class PlayerInfo
{
	public int pontuacao;
}
