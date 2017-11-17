using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingTank : MonoBehaviour {

	public static ObjectPoolingTank current;
	public GameObject pooledObjectTank;
	public int pooledAmount = 2;
	public bool willGrow = true;

	public List<GameObject> pooledObjectsTank;

	// Use this for initialization
	void Awake()
	{
		current = this;
	}

	void Start () 
	{
		pooledObjectsTank = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObjectTank);
			obj.SetActive (false);
			pooledObjectsTank.Add (obj);
		}
	}


	public GameObject GetPooledObjectTank()
	{
		for (int i = 0; i < pooledObjectsTank.Count; i++) 
		{
			if (!pooledObjectsTank [i].activeInHierarchy) 
			{
				return pooledObjectsTank[i];
			}

			if (willGrow) 
			{
				GameObject obj = (GameObject)Instantiate (pooledObjectTank);
				pooledObjectsTank.Add (obj);
				return obj;
			}
		}
		return null;
	}
}
