
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
		
	public FX trigger = FX.AVANCAR;
	
	[Range(0f, 1f)] public float volume = 1f;
	[Range(0f, 2f)] public float pitch = 1f;
	
	bool mIsOver = false;
	
	bool canPlay
	{
		get
		{
			if (!enabled) return false;
			UIButton btn = GetComponent<UIButton>();
			return (btn == null || btn.isEnabled);
		}
	}
	
	
	void OnClick ()
	{
		if (canPlay )
			gFX.s.Play( trigger );
	}
	
	
	public void Play ()
	{
			gFX.s.Play( trigger );
	}
}