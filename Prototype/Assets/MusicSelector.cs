using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script which hold a reference to music gameobjects and can interate through them sequentially as needed;

public class MusicSelector : MonoBehaviour {

	[SerializeField] private GameObject[] musicObjects;
	private AudioSource currentAudioSource;
	private int selectionIndex = 0;


	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
		
	}

	public void NextTrack()
	{
		musicObjects [selectionIndex].SetActive (false);
		if (selectionIndex < musicObjects.Length - 1) {
			selectionIndex++;
		} else {
			selectionIndex = 0;
		}
		musicObjects [selectionIndex].SetActive (true);
		currentAudioSource = musicObjects [selectionIndex].GetComponent<AudioSource> ();
		currentAudioSource.Play ();
	}

	public void StopCurrentTrack()
	{
		musicObjects [selectionIndex].SetActive (false);
	}
}
