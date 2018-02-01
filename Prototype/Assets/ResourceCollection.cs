using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCollection : MonoBehaviour 
{
	public float resources = 0;
	public float resourcesPerSec = 1;
	public float resourceModifier = 1;
	public int upgradePrice = 5; 
	public Text upgradePriceDisplay;
	public enum resourceCollectionType{Periodical,Constant};
	public resourceCollectionType collectionType;
	float lastUpdate = 0;
	// Use this for initialization
	void Start () 
	{
		//Debug.Log("Resources = " + resources.ToString());
	}

	// Update is called once per frame
	void Update ()
	{
		if (collectionType == resourceCollectionType.Constant) 
		{
			resources += resourcesPerSec * resourceModifier * Time.deltaTime;
			//Debug.Log ("Resources = " + resources.ToString ());
		} 
		else if (collectionType == resourceCollectionType.Periodical)
		{
			if (Time.time - lastUpdate >= 1f) 
			{
				resources += resourcesPerSec * resourceModifier;
				lastUpdate = Time.time;
				//Debug.Log ("Resources = " + resources.ToString ());
			}
		}
		upgradePriceDisplay.text = upgradePrice.ToString();
	}

	// function intended to replace the ClickOnDrill script.
	public void Upgrade()
	{
		// Do something
		//Debug.Log ("Click");
		if (resources >= upgradePrice) 
		{
			resources -= upgradePrice;
			resourceModifier *= 2;
			upgradePrice *= 2;
		} 
		else 
		{
			Debug.Log ("Need more Money");
		}
	}
}
