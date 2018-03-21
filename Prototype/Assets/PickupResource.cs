using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to control the behaviour of collectable resource pickups.

public class PickupResource : MonoBehaviour {

	[SerializeField] private float pickupValue = 10;
	[SerializeField] private float rotationIncrement = 1;
	[SerializeField] private GameObject displayObject;
	[SerializeField] private AudioClip pickupNoise;

	private ResourceCollection resourceManager;
	private AudioSource uiAudioSource;

	// Use this for initialization
	void Start () 
	{
		GameObject drillObject = GameObject.Find ("DrillObject");
		GameObject uiObject = GameObject.Find ("Main Game UI");
		resourceManager = drillObject.GetComponent<ResourceCollection> ();
		uiAudioSource = uiObject.GetComponent<AudioSource> ();
	}

	// Mouse Control
	void OnMouseDown()
	{
		gameObject.SetActive (false);
		resourceManager.resources += pickupValue;
		uiAudioSource.PlayOneShot (pickupNoise);
	}

	// Update is called once per frame
	void Update () 
	{
		displayObject.transform.RotateAround (displayObject.transform.position, Vector3.up, rotationIncrement * Time.deltaTime);
	}
}
