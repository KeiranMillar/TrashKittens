﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

	public GameObject pooledObject;
	public int pooledAmount = 10;
	public bool willGrow = true;

	List<GameObject> pooledObjects;

	// Use this for initialization
	void Start () 
	{
		pooledObjects = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObject);
			obj.SetActive (false);
			pooledObjects.Add (obj);
		}
	}


	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; i++) 
		{
			if (!pooledObjects [i].activeInHierarchy) 
			{
				return pooledObjects[i];
			}

			if (willGrow) 
			{
				GameObject obj = (GameObject)Instantiate (pooledObject);
				pooledObjects.Add (obj);
				return obj;
			}
		}
		return null;
	}
}