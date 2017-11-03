using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnDrill : MonoBehaviour {

	public int price = 5;
	public GameObject drill;

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	// When the drill is clicked
	void OnMouseDown()
	{
		ResourceCollection resourcesScript = drill.GetComponent<ResourceCollection>();
		HealthbarPlaceholderScript healthScript = drill.GetComponent<HealthbarPlaceholderScript> ();
		float resources = resourcesScript.resources;
		// Do something
		//Debug.Log ("Click");
		if (resources >= price) 
		{
			resources -= price;
			resourcesScript.resourceModifier *= 2;
			price *= 2;
			healthScript.health = 100;
		} 
		else 
		{
			Debug.Log ("Need more Money");
		}
	}

}