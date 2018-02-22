using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingMama : MonoBehaviour {

	public static ObjectPoolingMama current;
	public GameObject pooledObjectMama;
	public bool willGrow = true;
	//The size of the array is the number of waves
	int [] pooledAmount = new int[3];

	public List<GameObject> pooledObjectsMama;

	// Use this for initialization
	void Awake()
	{
		current = this;
		int[,] tempSpawns = this.gameObject.GetComponent<EnemySpawning> ().spawnLimit;

		for (int i = 0; i < (tempSpawns.GetLength(0)); i++) 
		{
			pooledAmount[i] = tempSpawns[i, 1];
		}
	}

	void Start () 
	{
		pooledObjectsMama = new List<GameObject> ();
		for (int i = 0; i < pooledAmount[(pooledAmount.GetLength(0)- 1)]; i++) 
		{
			GameObject obj = (GameObject)Instantiate (pooledObjectMama);
			obj.SetActive (false);
			pooledObjectsMama.Add (obj);
		}
	}

	public bool DeadMamas()
	{
		for (int i = 0; i < pooledObjectsMama.Count; i++) 
		{
			if (pooledObjectsMama [i].activeInHierarchy) 
			{
				return false;
			}
		}
		return true;
	}

	public GameObject GetPooledObjectMama()
	{
		for (int i = 0; i < pooledObjectsMama.Count; i++) 
		{
			if (!pooledObjectsMama [i].activeInHierarchy) 
			{
				return pooledObjectsMama[i];
			}

			if (willGrow) 
			{
				GameObject obj = (GameObject)Instantiate (pooledObjectMama);
				pooledObjectsMama.Add (obj);
				return obj;
			}
		}
		return null;
	}
}
