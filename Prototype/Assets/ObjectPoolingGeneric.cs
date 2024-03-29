using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A modified version of the enemy pooling scripts made by kieran.
// Changed for general purpose use.

public class ObjectPoolingGeneric : MonoBehaviour {

	public static ObjectPoolingGeneric current;
	public GameObject pooledObject;
	public bool willGrow = true;
	// initial pooled count
	public int pooledAmount = 15;

	public List<GameObject> pooledObjectsList;

	// Use this for initialization
	void Awake()
	{
		current = this;
	}
	void Start () 
	{
		pooledObjectsList = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObject);
			obj.SetActive (false);
			pooledObjectsList.Add (obj);
		}
	}

	public bool InactiveObject()
	{
		for (int i = 0; i < pooledObjectsList.Count; i++) 
		{
			if (pooledObjectsList [i].activeInHierarchy) 
			{
				return false;
			}
		}
		return true;
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjectsList.Count; i++) 
		{
			if (!pooledObjectsList [i].activeInHierarchy) 
			{
				return pooledObjectsList[i];
			}

			if (willGrow) 
			{
				GameObject obj = (GameObject)Instantiate (pooledObject);
				pooledObjectsList.Add (obj);
				return obj;
			}
		}
		return null;
	}
}
