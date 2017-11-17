using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class healthManagement : MonoBehaviour {

	public float currentHealth = 100;
	public float maxHealth = 100;
	public float upgradePrice = 5;
	public Slider healthBar;
	public bool healthDrain = false;
	private GameObject drill;

	// Use this for initialization
	void Start () 
	{
		drill = this.gameObject;
	}

	// Update is called once per frame
	void Update () 
	{
		healthBar.value = currentHealth;
		if (currentHealth <= 0)
		{
			DrillDead ();
		}
		// drains health at a constant rate for testing purposes
		if (healthDrain == true)
		{
			currentHealth -= 0.25f;
		}
	}

	// called when the player upgrades the maximum health of the drill
	public void Upgrade()
	{
		ResourceCollection resourceScript = drill.GetComponent<ResourceCollection>();

		if (resourceScript.resources >= upgradePrice) 
		{
			resourceScript.resources -= upgradePrice;
			maxHealth += 20;
			healthBar.maxValue = maxHealth;
			currentHealth += 20;
			upgradePrice *= 2;
		} 
		else 
		{
			Debug.Log ("Need more Money");
		}
	}

	// call when the player has lost, currently just reloads game scene
	public void DrillDead()
	{
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}
}
