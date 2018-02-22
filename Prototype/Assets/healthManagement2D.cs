using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class healthManagement2D : MonoBehaviour {

	public float currentHealth = 100;
	public float healthWarningThreshold = 50;
	public float maxHealth = 100;
	public float upgradePrice = 5;
	public Slider healthBar;
	public Text upgradePriceDisplay;
	public Image fill;
	public Sprite greenFill;
	public Sprite redFill;

	private GameObject drill;
	private bool damaged;
	float lastUpdate = 0.0f;

	// Use this for initialization
	void Start () 
	{
		drill = this.gameObject;
		damaged = false;
	}

	// Update is called once per frame
	void Update () 
	{
		healthBar.value = currentHealth;
		if (currentHealth <= 0)
		{
			DrillDead ();
		}
		// Change healthbar colour dependant on health
		if (currentHealth > 25.0f) 
		{
			fill.sprite = greenFill;
		}
		if ((currentHealth <= 25.0f) || (damaged == true))
		{
			fill.sprite = redFill;
		}
		damaged = false;
		upgradePriceDisplay.text = upgradePrice.ToString();

	}


	// Check if there is an object colliding with the drill
	// if its an enemy then do damage to the drill
	void OnCollisionStay2D(Collision2D coll)
	{
		if ((coll.gameObject.tag == "Enemy") && ((Time.time - lastUpdate) >= 1.0f))
		{
			currentHealth -= 1.0f;
			damaged = true;
			//Debug.Log ("Drill is taking damage");
			if (coll.gameObject.GetComponent<EnemyController2D> ().dieOnAttack == true) 
			{
				coll.gameObject.SetActive (false);

			}
			lastUpdate = Time.time;
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
