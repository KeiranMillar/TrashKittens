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
	public int [,] spawnLimit = new int [2,2] {{5, 2},
											   {7, 3}};

	public float babySpawnRateMin = 2.0f;
	public float babySpawnRateMax = 4.0f;

	public float tankSpawnRateMin = 0.0f;
	public float tankSpawnRateMax = 7.0f;

	public int wave = 0;

	bool emptyField = true;
	bool babyFinished = true;
	bool tankFinished = true;

	// Use this for initialization
	void Start () 
	{
		wave = 1;
	}

	void Update()
	{
		if (stateManager.getState () == GameState.active) 
		{
			if (stateManager.getState () == GameState.active && emptyField == true && babyFinished == true && tankFinished == true) 
			{
				babyFinished = false;
				tankFinished = false;
				emptyField = false;
				StartCoroutine (SpawnBaby ());
				StartCoroutine (SpawnTank ());
			}
			Debug.Log (wave);
			CheckEnemiesDead ();
		}
	}

	void CheckEnemiesDead()
	{
		if(ObjectPoolingBaby.current.DeadBabies() == true && ObjectPoolingTank.current.DeadTanks() == true
		)
		{
			emptyField = true;
			wave++;
			Debug.Log ("dumb");
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

	IEnumerator SpawnTank () 
	{
		for (int i = 0; i < spawnLimit[(wave - 1),1]; i++) 
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
}
