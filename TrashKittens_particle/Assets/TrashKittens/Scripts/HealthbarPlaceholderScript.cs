using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Placeholder script that changes a slider based on the "health value" of a quad
// Be careful with scale of slider

public class HealthbarPlaceholderScript : MonoBehaviour 
{
	// public variables
	public float health = 100;
	public Slider healthbarSlider;
	
	// Update is called once per frame
	void Update () 
	{
		if (health < 1) {
			health = 100;
		} else 
		{
			health--;
		}
		healthbarSlider.value = health;
	}
}
