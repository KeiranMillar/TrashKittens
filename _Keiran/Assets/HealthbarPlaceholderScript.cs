using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
		if (health <= 0) 
		{
			restartCurrentScene ();
		} else 
		{
			health -= 1 * Time.deltaTime;
		}
		healthbarSlider.value = health;
	}

	public void restartCurrentScene()
	{
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}
}
