using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DrillController : MonoBehaviour {

	public float resources = 0;
	public float currentHealth = 100;
	public float healthWarningThreshold = 50;
	public float maxHealth = 100;
	public int upgradeLevel = 1;
	public float upgradePrice = 5;
	public float turretPrice = 5;
	public float turretDamage = 0;
	public float repairPrice = 25;
	public float upgradeResourcePrice = 35;
	public Slider healthBar;
	public Text upgradePriceDisplay;
	public Text repairPriceDisplay;
	public Text turretPriceDisplay;
	public Text upgradeResourcePriceDisplay;
	public Text turretDamageDisplay;
	public Text maxHealthDisplay;
	public Text resourceDisplay;
	public Image fill;
	public Color damageColour;
	public Color warningColour;
	public AudioClip upgradeClip;
	public AudioClip[] upgradeFailClip;
	public AudioSource audioSource;
	public GameObject turret;
	public GameObject fireTurretButton;
	public GameStateManager StateManager;

	private bool turretBought = false;
	private bool damaged;
	private Color healthyColour;
	private DrillAnimation animationScript;
	float lastUpdate = 0.0f;

	// Use this for initialization
	void Start () 
	{
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
		upgradeResourcePriceDisplay.text = upgradeResourcePrice.ToString();
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

		if ((resources >= upgradePrice) && (upgradeLevel < 3))
		{
			resources -= upgradePrice;
			maxHealth += 20;
			healthBar.maxValue = maxHealth;
			maxHealthDisplay.text = "The drill's max health is " + maxHealth.ToString();
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
		if (upgradeResourcePrice <= resources)
		{
			currentHealth += repairValue;
			if (currentHealth > maxHealth) 
			{
				currentHealth = maxHealth;
			}
			resources -= upgradeResourcePrice;
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
		if(turretPrice <= resources && turretBought == false)
		{
			turretBought = true;
			turret.SetActive(true);
			fireTurretButton.SetActive(true);
			resources -= turretPrice;
			turretPrice *= 2;
			turretDamageDisplay.text = "The turret does " + turretDamage.ToString() + " damage per shot";
			audioSource.clip = upgradeClip;
			audioSource.Play ();
		}
		else if(turretPrice <= resources)
		{
			turretDamage += 1;
			turretPrice *= 2;
			turretDamageDisplay.text = "The turret does " + turretDamage.ToString() + " damage per shot";
			resources -= turretPrice;
			audioSource.clip = upgradeClip;
			audioSource.Play ();
		}
		else 
		{
			audioSource.clip = upgradeFailClip[Random.Range(0, upgradeFailClip.Length)];
			audioSource.Play();	
		}
	}

	public void UpgradeResourceOutput(float bonusIncrease)
	{
		if (upgradeResourcePrice <= resources)
		{
			StateManager.bonusResourceFromUpgrades += bonusIncrease;
			resources -= upgradeResourcePrice;
			upgradeResourcePrice *= 2;
			resourceDisplay.text = "You get " + StateManager.bonusResourceFromUpgrades.ToString() + " extra resources per wave";
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
