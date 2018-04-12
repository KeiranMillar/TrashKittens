using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class healthManagement2D : MonoBehaviour {

	public float currentHealth = 100;
	public float healthWarningThreshold = 50;
	public float maxHealth = 100;
	public int upgradeLevel = 1;
	public float upgradePrice = 5;
	public float turretPrice = 5;
	public float turretDamage = 1;
	public float repairPrice = 25;
	public Slider healthBar;
	public Text upgradePriceDisplay;
	public Text repairPriceDisplay;
	public Text turretPriceDisplay;
	public Image fill;
	public Color damageColour;
	public Color warningColour;
	public AudioClip upgradeClip;
	public AudioClip[] upgradeFailClip;
	public AudioSource audioSource;
	public GameObject turret;

	private GameObject drill;
	private bool turretBought = false;
	private bool damaged;
	private Color healthyColour;
	private DrillAnimation animationScript;
	float lastUpdate = 0.0f;

	// Use this for initialization
	void Start () 
	{
		drill = this.gameObject;
		damaged = false;
		healthyColour = fill.color;
		healthBar.maxValue = maxHealth;
		animationScript = gameObject.GetComponent<DrillAnimation>();
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
		// Tint red when taking damage
		if (damaged == true) 
		{
			fill.color = Color.Lerp (fill.color, damageColour, 0.4f);
		} 
		else 
		{
			if (currentHealth > healthWarningThreshold) 
			{
				fill.color = Color.Lerp (fill.color, healthyColour, 0.1f);
			} 
			else 
			{
				fill.color = Color.Lerp (fill.color, warningColour, 0.1f);
			}
		}
		damaged = false;
		upgradePriceDisplay.text = upgradePrice.ToString();
		repairPriceDisplay.text = repairPrice.ToString();
		turretPriceDisplay.text = turretPrice.ToString();
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

		if ((resourceScript.resources >= upgradePrice) && (upgradeLevel < 3))
		{
			resourceScript.resources -= upgradePrice;
			maxHealth += 20;
			healthBar.maxValue = maxHealth;
			currentHealth += 20;
			upgradePrice *= 2;
			upgradeLevel++;
			animationScript.setUpgradeLevel(upgradeLevel);
			audioSource.clip = upgradeClip;
			audioSource.Play();
		} 
		else 
		{
			audioSource.clip = upgradeFailClip[Random.Range(0, upgradeFailClip.Length)];
			audioSource.Play();			
		}
	}

	public void Damage(float damageValue)
	{
		currentHealth -= damageValue;
		damaged = true;
	}

	public void Repair(float repairValue)
	{
		ResourceCollection resourceScript = drill.GetComponent<ResourceCollection>();
		if ((repairPrice <= resourceScript.resources) && (currentHealth < maxHealth))
		{
			currentHealth += repairValue;
			if (currentHealth > maxHealth) {
				currentHealth = maxHealth;
			}
			resourceScript.resources -= repairPrice;
			audioSource.clip = upgradeClip;
			audioSource.Play ();
		}
		else 
		{
			audioSource.clip = upgradeFailClip[Random.Range(0, upgradeFailClip.Length)];
			audioSource.Play();	
		}
	}

	public void TurretPurchase()
	{
		ResourceCollection resourceScript = drill.GetComponent<ResourceCollection>();
		if(turretPrice <= resourceScript.resources && turretBought == false)
		{
			turretBought = true;
			turret.SetActive(true);
			resourceScript.resources -= turretPrice;
			turretPrice *= 2;
			audioSource.clip = upgradeClip;
			audioSource.Play ();
		}
		else if(turretPrice <= resourceScript.resources)
		{
			turretDamage += 1;
			turretPrice *= 2;
			resourceScript.resources -= turretPrice;
			audioSource.clip = upgradeClip;
			audioSource.Play ();
		}
		else 
		{
			audioSource.clip = upgradeFailClip[Random.Range(0, upgradeFailClip.Length)];
			audioSource.Play();	
		}
	}

	// call when the player has lost, currently just reloads game scene
	public void DrillDead()
	{
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}
}
