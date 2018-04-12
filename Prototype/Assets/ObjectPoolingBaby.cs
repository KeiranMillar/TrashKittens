using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingBaby : MonoBehaviour {

	public static ObjectPoolingBaby current;
	public GameObject pooledObjectBaby;
	public bool willGrow = true;
	//The size of the array is the number of waves
	int [] pooledAmount = new int[4];

	public List<GameObject> pooledObjectsBaby;

	// Use this for initialization
	void Awake()
	{
		current = this;
		int[,] tempSpawns = this.gameObject.GetComponent<EnemySpawning> ().spawnLimit;

		for (int i = 0; i < (tempSpawns.GetLength(0)); i++) 
		{
			pooledAmount[i] = tempSpawns[i, 0];
		}
	}
	void Start () 
	{
		pooledObjectsBaby = new List<GameObject> ();
		for (int i = 0; i < pooledAmount[(pooledAmount.GetLength(0)- 1)]; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObjectBaby);
			obj.SetActive (false);
			pooledObjectsBaby.Add (obj);
		}
	}

	public bool DeadBabies()
	{
		for (int i = 0; i < pooledObjectsBaby.Count; i++) 
		{
			if (pooledObjectsBaby [i].activeInHierarchy) 
			{
				return false;
			}
		}
		return true;
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
