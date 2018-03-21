using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A modified version of the enemy pooling scripts made by kieran.

public class ObjectPoolingPickup : MonoBehaviour {

	public static ObjectPoolingPickup current;
	public GameObject pooledObjectPickup;
	public bool willGrow = true;
	// initial pooled count
	public int pooledAmount = 15;

	public List<GameObject> pooledObjectsPickup;

	// Use this for initialization
	void Awake()
	{
		current = this;
		//int[,] tempSpawns = this.gameObject.GetComponent<EnemySpawning> ().spawnLimit;
		//
		//for (int i = 0; i < (tempSpawns.GetLength(0)); i++) 
		//{
		//	pooledAmount[i] = tempSpawns[i, 0];
		//}
	}
	void Start () 
	{
		pooledObjectsPickup = new List<GameObject> ();
		for (int i = 0; i < pooledAmount; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObjectPickup);
			obj.SetActive (false);
			pooledObjectsPickup.Add (obj);
		}
	}

	public bool DeadPickup()
	{
		for (int i = 0; i < pooledObjectsPickup.Count; i++) 
		{
			if (pooledObjectsPickup [i].activeInHierarchy) 
			{
				return false;
			}
		}
		return true;
	}

	public GameObject GetPooledObjectPickup()
	{
		for (int i = 0; i < pooledObjectsPickup.Count; i++) 
		{
			if (!pooledObjectsPickup [i].activeInHierarchy) 
			{
				return pooledObjectsPickup[i];
			}

			if (willGrow) 
			{
				GameObject obj = (GameObject)Instantiate (pooledObjectPickup);
				pooledObjectsPickup.Add (obj);
				return obj;
			}
		}
		return null;
	}
}
