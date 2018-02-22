using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingTank : MonoBehaviour {

	public static ObjectPoolingTank current;
	public GameObject pooledObjectTank;
	public bool willGrow = true;
	//The size of the array is the number of waves
	int [] pooledAmount = new int[3];

	public List<GameObject> pooledObjectsTank;

	// Use this for initialization
	void Awake()
	{
		current = this;
		int[,] tempSpawns = this.gameObject.GetComponent<EnemySpawning> ().spawnLimit;

		for (int i = 0; i < (tempSpawns.GetLength(0)); i++) 
		{
			pooledAmount[i] = tempSpawns[i, 2];
		}
	}

	void Start () 
	{
		pooledObjectsTank = new List<GameObject> ();
		for (int i = 0; i < pooledAmount[(pooledAmount.GetLength(0)- 1)]; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObjectTank);
			obj.SetActive (false);
			pooledObjectsTank.Add (obj);
		}
	}

	public bool DeadTanks()
	{
		for (int i = 0; i < pooledObjectsTank.Count; i++) 
		{
			if (pooledObjectsTank [i].activeInHierarchy) 
			{
				return false;
			}
		}
		return true;
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
