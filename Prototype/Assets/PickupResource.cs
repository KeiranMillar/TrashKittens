using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to control the behaviour of collectable resource pickups.

public class PickupResource : MonoBehaviour {

	[SerializeField] private float pickupValue = 10;
	[SerializeField] private float rotationIncrement = 1;
	[SerializeField] private GameObject displayObject;
	//public ResourceCollection resourceManager;

	public ResourceCollection resourceManager;

	// Use this for initialization
	void Start () 
	{
		GameObject drillObject = GameObject.Find ("DrillObject");
		resourceManager = drillObject.GetComponent<ResourceCollection> ();
	}

	// Mouse Control
	void OnMouseDown()
	{
		gameObject.SetActive (false);
		resourceManager.resources += pickupValue;
	}

	// Update is called once per frame
	void Update () 
	{
		displayObject.transform.RotateAround (displayObject.transform.position, Vector3.up, rotationIncrement * Time.deltaTime);
	}
}
