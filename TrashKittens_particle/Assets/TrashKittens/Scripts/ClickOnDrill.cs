using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnDrill : MonoBehaviour {

	public int price = 5;



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
		GameObject drill = GameObject.Find("Quad");
		ResourceCollection resourcesScript = drill.GetComponent<ResourceCollection>();
		float resources = drill.GetComponent<ResourceCollection>().resources;
		// Do something
		Debug.Log ("Click");
		if (drill.GetComponent< ResourceCollection > ().resources >= price) 
		{
			drill.GetComponent< ResourceCollection > ().resources -= price;
			drill.GetComponent< ResourceCollection > ().resourceModifier *= 2;
			price *= 2;
		} 
		else 
		{
			Debug.Log ("Need more Money");
		}
	}

}
