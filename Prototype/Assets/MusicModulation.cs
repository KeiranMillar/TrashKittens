using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicModulation : MonoBehaviour {

	public AudioMixerSnapshot Default;
	public AudioMixerSnapshot AlienReachedDrill;


	void OnCollisionStay2D ()
	{
        AlienReachedDrill.TransitionTo (0.5f);

	}

	void OnCollisionExit2D ()
	{
		Default.TransitionTo (0.5f);
	}

	void Update () {

	}
}
