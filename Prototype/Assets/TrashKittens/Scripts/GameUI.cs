using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A script that manages the main game ui
// currently just reads resource value from drill

public class GameUI : MonoBehaviour 
{
	// Public Variables
	public Text mainResourceCounter;
	public Text upgradeResourceCounter;
	public GameObject drillObject;

	// PrivateVariables
	float drillResource;

	// Update is called once per frame
	void Update () 
	{
		drillResource = drillObject.GetComponent<ResourceCollection>().resources;
		mainResourceCounter.text = drillResource.ToString();
		upgradeResourceCounter.text = drillResource.ToString();
	}
}
