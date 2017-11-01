using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour 
{
	public float resources = 0;
	public float resourcesPerSec = 1;
	public float lastUpdate = 0;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time - lastUpdate >= 1f)
		{
			resources += resourcesPerSec;
			lastUpdate = Time.time;
			Debug.Log("Resources = " + resources.ToString());
		}
	}
}
