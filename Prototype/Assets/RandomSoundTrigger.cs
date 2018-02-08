using UnityEngine;
using System.Collections;

public class RandomSoundTrigger : MonoBehaviour {

	public AudioSource source;
	public AudioClip[] clips;

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter()
	{
		source.clip = clips[Random.Range(0, clips.Length)];
		source.Play();
	}
}
