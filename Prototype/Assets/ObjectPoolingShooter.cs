using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingShooter : MonoBehaviour {

	public static ObjectPoolingShooter current;
	public GameObject pooledObjectShooter;
	public bool willGrow = true;
	//The size of the array is the number of waves
	int [] pooledAmount = new int[4];

	public List<GameObject> pooledObjectsShooter;

	// Use this for initialization
	void Awake()
	{
		current = this;
		int[,] tempSpawns = this.gameObject.GetComponent<EnemySpawning> ().spawnLimit;

		for (int i = 0; i < (tempSpawns.GetLength(0)); i++) 
		{
			pooledAmount[i] = tempSpawns[i, 3];
		}
	}
	void Start () 
	{
		pooledObjectsShooter = new List<GameObject> ();
		for (int i = 0; i < pooledAmount[(pooledAmount.GetLength(0)- 1)]; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObjectShooter);
			obj.SetActive (false);
			pooledObjectsShooter.Add (obj);
		}
	}

	public bool DeadShooters()
	{
		for (int i = 0; i < pooledObjectsShooter.Count; i++) 
		{
			if (pooledObjectsShooter [i].activeInHierarchy) 
			{
				return false;
			}
		}
		return true;
	}

	public GameObject GetPooledObjectShooter()
	{
		for (int i = 0; i < pooledObjectsShooter.Count; i++) 
		{
			if (!pooledObjectsShooter [i].activeInHierarchy) 
			{
				return pooledObjectsShooter[i];
			}

			if (willGrow) 
			{
				GameObject obj = (GameObject)Instantiate (pooledObjectShooter);
				pooledObjectsShooter.Add (obj);
				return obj;
			}
		}
		return null;
	}
}
