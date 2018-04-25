using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public GameStateManager stateManager;
	public Vector3 spawn;
	// Game state enum
	//Each value in this 2D array corresponds to the 
	//number of enemies spawning per wave.
	//So, for the first wave, 5 babies and 2 tanks
	public int [,] spawnLimit = new int [4,4] {{5, 2, 0, 1},
		{7, 3, 1, 2}, {10, 5, 2, 3}, {14, 8, 3, 4}};

	public float babySpawnRateMin = 2.0f;
	public float babySpawnRateMax = 4.0f;

	public float mamaSpawnRateMin = 4.0f;
	public float mamaSpawnRateMax = 5.0f;
	public float mamaSpawnDelay = 2.0f;

	public float tankSpawnRateMin = 3.0f;
	public float tankSpawnRateMax = 7.0f;
	public float tankSpawnDelay = 4.0f;

	public float shooterSpawnRateMin = 3.0f;
	public float shooterSpawnRateMax = 5.0f;
	public float shooterSpawnDelay = 3.0f;

	public int wave = 0;

	bool emptyField = true;
	bool babyFinished = true;
	bool tankFinished = true;
	bool mamaFinished = true;
	bool shooterFinished = true;

	// Use this for initialization
	void Start () 
	{
		wave = 1;
	}

	void Update()
	{
		if (stateManager.getState () == GameState.active) 
		{
			if (stateManager.getState () == GameState.active && emptyField == true && babyFinished == true && tankFinished == true && mamaFinished == true && shooterFinished == true) 
			{
				babyFinished = false;
				mamaFinished = false;
				tankFinished = false;
				shooterFinished = false;
				emptyField = false;

				StartCoroutine (SpawnBaby ());
				StartCoroutine (SpawnMama ());
				StartCoroutine (SpawnTank ());
				StartCoroutine (SpawnShooter ());
			}
			if (stateManager.getState () == GameState.active && babyFinished == true && tankFinished == true && mamaFinished == true && shooterFinished == true) 
			{
			CheckEnemiesDead ();
			}
		}
	}

	void CheckEnemiesDead()
	{
		if(ObjectPoolingBaby.current.DeadBabies() == true && ObjectPoolingTank.current.DeadTanks() == true && ObjectPoolingMama.current.DeadMamas() == true && ObjectPoolingShooter.current.DeadShooters() == true)
		{
			emptyField = true;
			wave++;
			// PLACEHOLDER
			if (wave > 4) 
			{
				wave = 1;
			}
			// PLACEHOLDER END
			stateManager.WaveEnd ();
		}
	}

	IEnumerator SpawnBaby () 
	{
		for (int i = 0; i < spawnLimit[(wave - 1),0]; i++) 
		{
			GameObject obj = ObjectPoolingBaby.current.GetPooledObjectBaby ();

			if (obj == null) {
					
			} else {
				obj.transform.position = spawn;
				obj.transform.rotation.Set (0, 0, 0, 0);
				obj.SetActive (true);
			}
		yield return new WaitForSeconds (Random.Range (babySpawnRateMin, babySpawnRateMax));
		}
		babyFinished = true;
	}

	IEnumerator SpawnMama () 
	{
		yield return new WaitForSeconds (mamaSpawnDelay);
		for (int i = 0; i < spawnLimit[(wave - 1),1]; i++) 
		{
			GameObject obj = ObjectPoolingMama.current.GetPooledObjectMama ();

			if (obj == null) {

			} else {
				obj.transform.position = spawn;
				obj.transform.rotation.Set (0, 0, 0, 0);
				obj.SetActive (true);
			}
			yield return new WaitForSeconds (Random.Range (mamaSpawnRateMin, mamaSpawnRateMax));
		}
		mamaFinished = true;
	}

	IEnumerator SpawnTank () 
	{
		yield return new WaitForSeconds (tankSpawnDelay);
		for (int i = 0; i < spawnLimit[(wave - 1),2]; i++) 
		{
			GameObject obj = ObjectPoolingTank.current.GetPooledObjectTank ();

			if (obj == null) {
				
			} else {
				obj.transform.position = spawn;
				obj.transform.rotation.Set (0, 0, 0, 0);
				obj.SetActive (true);
			}
			yield return new WaitForSeconds (Random.Range (tankSpawnRateMin, tankSpawnRateMax));
		}
		tankFinished = true;
	}

	IEnumerator SpawnShooter () 
	{
		yield return new WaitForSeconds (shooterSpawnDelay);
		for (int i = 0; i < spawnLimit[(wave - 1),3]; i++) 
		{
			GameObject obj = ObjectPoolingShooter.current.GetPooledObjectShooter ();

			if (obj == null) {

			} else {
				obj.transform.position = spawn;
				obj.transform.rotation.Set (0, 0, 0, 0);
				obj.SetActive (true);
			}
			yield return new WaitForSeconds (Random.Range (shooterSpawnRateMin, shooterSpawnRateMax));
		}
		shooterFinished = true;
	}
}
