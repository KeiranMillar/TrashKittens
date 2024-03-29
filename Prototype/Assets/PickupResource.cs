﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Script to control the behaviour of collectable resource pickups.

public class PickupResource : MonoBehaviour {


	public float animatedFloatingSpeed = 1f;
	public float floatHeight = 2f;

	[SerializeField] private float pickupValue = 10;
	[SerializeField] private float spawnJumpModifier = 1;
	[SerializeField] private float rotationIncrement = 1;
	[SerializeField] private GameObject displayObject;
	[SerializeField] private AudioClip pickupNoise;
	private DrillController drillController;
	private AudioSource uiAudioSource;
	public AudioMixerGroup output;
	private bool floatingRises;

	// Use this for initialization
	void Start () 
	{
		GameObject drillObject = GameObject.Find ("DrillObject");
		GameObject uiObject = GameObject.Find ("Main Game UI");
		drillController = drillObject.GetComponent<DrillController> ();
		uiAudioSource = uiObject.GetComponent<AudioSource> ();
		floatingRises = true;
	}

	// Mouse Control
	void OnMouseDown()
	{
		Pickup ();
	}

	// Update is called once per frame
	void Update () 
	{
		// rotation
		displayObject.transform.RotateAround (displayObject.transform.position, Vector3.up, rotationIncrement * Time.deltaTime);
		// bobbing animation
		if (floatingRises == true) 
		{
			displayObject.transform.Translate (Vector3.up * animatedFloatingSpeed * Time.deltaTime);
		} 
		else 
		{
			displayObject.transform.Translate (Vector3.down * animatedFloatingSpeed * Time.deltaTime);
		}
		if (gameObject.transform.position.y - displayObject.transform.position.y < -floatHeight) 
		{
			floatingRises = false;
		}
		else if ((gameObject.transform.position.y - displayObject.transform.position.y) > floatHeight)
		{
			floatingRises = true;
		}

	}

	void Pickup()
	{
		gameObject.SetActive (false);
		drillController.resources += pickupValue;
		uiAudioSource.outputAudioMixerGroup = output;
		uiAudioSource.PlayOneShot (pickupNoise);
	}

	public void Spawn(Vector3 spawnPosition, float value)
	{
		pickupValue = value;
		gameObject.transform.position = spawnPosition;
		gameObject.SetActive (true);
		Rigidbody2D objectRigidbody = GetComponent<Rigidbody2D> ();
		objectRigidbody.AddForce (Vector2.up * spawnJumpModifier);
	}
}
