 using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class gAmbientacao : MonoBehaviour {

	public AudioClip musicaAmbiente;
	public AudioSource audioSource;
	
	public static gAmbientacao s;
	
	public void Awake()
	{
		s = this;
		audioSource = GetComponent<AudioSource>();
	}
	
	public void PlayAmbienceMusic()
	{
		audioSource.clip = musicaAmbiente;
		audioSource.Play();
	}
	
	public void StopAmbienceMusic()
	{
		audioSource.Stop();
	}
}
