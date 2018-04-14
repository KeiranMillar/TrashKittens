using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrillTextEditor : MonoBehaviour {

	public GameObject turret;
	bool defaultText = true;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(defaultText)
		{
			if(turret.activeInHierarchy)
			{
				Text UIText = this.GetComponent<Text>();
				UIText.text = "Upgrade The Turret's Damage";
			}
		}
	}
}
