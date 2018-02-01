using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour {

	/*
		Audio Fade In
 
	*/

	AudioSource myAudio;

	void Start () {

		myAudio = GetComponent<AudioSource> ();

	}
	public int approxSecondsToFade = 5;

	void FixedUpdate ()
	{
		if (myAudio.volume < 1)
		{
			myAudio.volume = myAudio.volume + (Time.deltaTime / (approxSecondsToFade + 5));
		}
		else
		{
			Destroy (this);
		}
	}
}
