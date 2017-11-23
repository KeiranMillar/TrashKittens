using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour 
{
	public float resources = 0;
	public float resourcesPerSec = 1;
	public float resourceModifier = 1;
	public enum resourceCollectionType{Periodical,Constant};
	public resourceCollectionType collectionType;
	float lastUpdate = 0;
	// Use this for initialization
	void Start () 
	{
		Debug.Log("Resources = " + resources.ToString());
	}

	// Update is called once per frame
	void Update ()
	{
		if (collectionType == resourceCollectionType.Constant) 
		{
			resources += resourcesPerSec * resourceModifier * Time.deltaTime;
			Debug.Log ("Resources = " + resources.ToString ());
		} 
		else if (collectionType == resourceCollectionType.Periodical)
		{
			if (Time.time - lastUpdate >= 1f) 
			{
				resources += resourcesPerSec * resourceModifier;
				lastUpdate = Time.time;
				Debug.Log ("Resources = " + resources.ToString ());
			}
		}
	}
}
