using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A script that manages the main game ui
// currently just reads resource value from drill

public class GameUI : MonoBehaviour 
{
	// Public Variables
	public Text resourceCounter;
	public Text upgradeResourceCounter;
	public DrillController drillController;

	// Update is called once per frame
	void Update () 
	{
		resourceCounter.text = Mathf.FloorToInt(drillController.resources).ToString();
		upgradeResourceCounter.text = drillController.resources.ToString ();
	}
}
