//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2015 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Small script that makes it easy to create looping 2D sprite animations.
/// </summary>

public class UI2DSpriteAnimation : MonoBehaviour
{
	/// <summary>
	/// How many frames there are in the animation per second.
	/// </summary>

	[SerializeField] protected int framerate = 9;
	
	/// <summary>
	/// Should this animation be affected by time scale?
	/// </summary>

	public bool ignoreTimeScale = true;

	/// <summary>
	/// Should this animation be looped?
	/// </summary>

//	public bool loop = true;

	/// <summary>
	/// Actual sprites used for the animation.
	/// </summary>

//	public UnityEngine.Sprite[] frames;
	
	/// <summary>
	/// Lista de Animacoes configuraveis.
	/// </summary>
	public UI2DAnimation[] animations;
	public int currentAnimation = 0;

	UnityEngine.SpriteRenderer mUnitySprite;
	UI2DSprite mNguiSprite;
	int mIndex = 0;
	float mUpdate = 0f;

	/// <summary>
	/// Returns is the animation is still playing or not
	/// </summary>

	public bool isPlaying { get { return enabled; } }

	/// <summary>
	/// Animation framerate.
	/// </summary>

	public int framesPerSecond { get { return framerate; } set { framerate = value; } }

	/// <summary>
	/// Continue playing the animation. If the animation has reached the end, it will restart from beginning
	/// </summary>

	public void Play (int index){ currentAnimation = ( index > 0 )? index : 0; Play (); }
	
	public void Play (string animationName)
	{ 
		Debug.Log("Play:" + animationName);
	
		for( int i =0; i< animations.Length; i++ )
			if( animations[i].name.ToLower() == animationName.ToLower())
			{	Play(i);return;}	
	}

	public void Play ()
	{
	
		if (animations != null && animations.Length > 0 && animations[currentAnimation].frames.Length > 0)
		{
			if (!enabled && !animations[currentAnimation].loop)
			{
				int newIndex = framerate > 0 ? mIndex + 1 : mIndex - 1;
				if (newIndex < 0 || newIndex >= animations[currentAnimation].frames.Length)
					mIndex = framerate < 0 ? animations[currentAnimation].frames.Length - 1 : 0;
			}
			
			enabled = true;
			UpdateSprite();
		}
	}

	/// <summary>
	/// Pause the animation.
	/// </summary>

	public void Pause () { enabled = false; }

	/// <summary>
	/// Reset the animation to the beginning.
	/// </summary>

	public void ResetToBeginning ()
	{
		mIndex = framerate < 0 ? animations[currentAnimation].frames.Length - 1 : 0;
		UpdateSprite();
	}

	/// <summary>
	/// Start playing the animation right away.
	/// </summary>

	void Start () { Play(); }

	/// <summary>
	/// Advance the animation as necessary.
	/// </summary>

	void Update ()
	{
		if (animations == null || animations.Length == 0 || animations[currentAnimation].frames.Length == 0)
		{
			enabled = false;
		}
		else if (framerate != 0)
		{
			float time = ignoreTimeScale ? RealTime.time : Time.time;

			if (mUpdate < time)
			{
				mUpdate = time;
				int newIndex = framerate > 0 ? mIndex + 1 : mIndex - 1;

				if (!animations[currentAnimation].loop && (newIndex < 0 || newIndex >= animations[currentAnimation].frames.Length))
				{
					enabled = false;
					return;
				}

				mIndex = NGUIMath.RepeatIndex(newIndex, animations[currentAnimation].frames.Length);
				UpdateSprite();
			}
		}
	}

	/// <summary>
	/// Immediately update the visible sprite.
	/// </summary>

	void UpdateSprite ()
	{
		if (mUnitySprite == null && mNguiSprite == null)
		{
			mUnitySprite = GetComponent<UnityEngine.SpriteRenderer>();
			mNguiSprite = GetComponent<UI2DSprite>();

			if (mUnitySprite == null && mNguiSprite == null)
			{
				enabled = false;
				return;
			}
		}

		float time = ignoreTimeScale ? RealTime.time : Time.time;
		if (framerate != 0) mUpdate = time + Mathf.Abs(1f / framerate);
		
		mIndex = Mathf.Clamp( mIndex, 0, animations[currentAnimation].frames.Length-1 );

		if (mUnitySprite != null)
		{
			mUnitySprite.sprite = animations[currentAnimation].frames[mIndex];
		}
		else if (mNguiSprite != null)
		{
			
			mNguiSprite.nextSprite = animations[currentAnimation].frames[mIndex];;
		}
	}
	
	public void AddNewAnimation( string name,  Sprite[] sprites, bool loop = true )
	{
		if( animations == null ) animations = new UI2DAnimation[0];
	
		List<UI2DAnimation> temp = new List<UI2DAnimation>();
		temp.AddRange( animations );
		
		UI2DAnimation newAnimation = new UI2DAnimation();
		newAnimation.name = name;
		newAnimation.loop = loop;
		newAnimation.frames = sprites;
		
		temp.Add(newAnimation);
				
		animations = new UI2DAnimation[temp.Count];
		animations = temp.ToArray();
	}
	
	
}

[System.Serializable]
public class UI2DAnimation
{
	public string name;
	public bool loop = true;
	public UnityEngine.Sprite[] frames;
	
	
}