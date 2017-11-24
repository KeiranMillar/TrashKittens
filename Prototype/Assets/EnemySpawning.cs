using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public Vector3 spawn;
    public float spawnX;
	public float babySpawnRate = 5.0f;
	public float tankSpawnRate = 7.0f;


	// Use this for initialization
	void Start () {
		spawn.Set(spawnX, 0.0f, 0.0f);
		StartCoroutine (SpawnBaby ());
		StartCoroutine (SpawnTank ());
	}
	
	// Update is called once per frame
	IEnumerator SpawnBaby () 
	{
		while (true) 
		{
			GameObject obj = ObjectPoolingBaby.current.GetPooledObjectBaby ();

			if (obj == null) 
			{

			} else {
				obj.transform.position = spawn;
				obj.transform.rotation.Set (0, 0, 0, 0);
				obj.SetActive (true);
			}
			yield return new WaitForSeconds (babySpawnRate);
		}
	}

	IEnumerator SpawnTank () 
	{
		while (true) 
		{
			GameObject obj = ObjectPoolingTank.current.GetPooledObjectTank ();

			if (obj == null) 
			{

			} else {
				obj.transform.position = spawn;
				obj.transform.rotation.Set (0, 0, 0, 0);
				obj.SetActive (true);
			}
			yield return new WaitForSeconds (tankSpawnRate);
		}
	}
}
