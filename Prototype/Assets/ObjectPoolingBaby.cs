using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingBaby : MonoBehaviour {

	public static ObjectPoolingBaby current;
	public GameObject pooledObjectBaby;
	public int pooledAmount = 12;
	public bool willGrow = true;

	public List<GameObject> pooledObjectsBaby;

	// Use this for initialization
	void Awake()
	{
		current = this;
	}
	void Start () 
	{
		pooledObjectsBaby = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObjectBaby);
			obj.SetActive (false);
			pooledObjectsBaby.Add (obj);
		}
	}


	public GameObject GetPooledObjectBaby()
	{
		for (int i = 0; i < pooledObjectsBaby.Count; i++) 
		{
			if (!pooledObjectsBaby [i].activeInHierarchy) 
			{
				return pooledObjectsBaby[i];
			}

			if (willGrow) 
			{
				GameObject obj = (GameObject)Instantiate (pooledObjectBaby);
				pooledObjectsBaby.Add (obj);
				return obj;
			}
		}
		return null;
	}
}
