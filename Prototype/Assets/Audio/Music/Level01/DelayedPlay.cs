using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedPlay : MonoBehaviour {

	AudioSource myAudio;

	// Use this for initialization
	void Start () {

		myAudio = GetComponent<AudioSource> ();
		Invoke ("playAudio", 1.0f);

	}

	void playAudio()
	{
		myAudio.Play ();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
