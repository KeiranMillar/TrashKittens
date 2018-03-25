using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFiring : MonoBehaviour {

	public List<GameObject> babies;
	public List<GameObject> mamas;
	public List<GameObject> tanks;

	public GameObject bullet;
	public float bulletLife = 5.0f;
	public int bulletSpeed = 1000000;

	public GameObject SpawnManager;


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform targetLocation;
		//ResourceCollection resourcesScript = drill.GetComponent<ResourceCollection>();
		ObjectPoolingBaby babiesScript = SpawnManager.GetComponent<ObjectPoolingBaby>();
		ObjectPoolingMama mamaScript = SpawnManager.GetComponent<ObjectPoolingMama>();
		ObjectPoolingTank tankScript = SpawnManager.GetComponent<ObjectPoolingTank>();
		babies = babiesScript.pooledObjectsBaby;
		mamas = mamaScript.pooledObjectsMama;
		tanks = tankScript.pooledObjectsTank;
		targetLocation = GetClosestEnemy(babies, mamas, tanks);

		if(targetLocation && !bullet.activeInHierarchy)
		{
			ShootEnemy (targetLocation);
		}

		if(bullet.activeInHierarchy)
		{
			bullet.transform.Translate(this.transform.forward * bulletSpeed * Time.deltaTime, Space.Self);
			bulletLife -= Time.deltaTime;
			if(bulletLife < 0)
			{
				bullet.SetActive(false);
				bulletLife = 5.0f;
			}
		}
		
	}
		
	Transform GetClosestEnemy (List<GameObject> babies, List<GameObject> mamas, List<GameObject> tanks)
	{
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;
		for(int i = 0; i < babies.Count; i++)
		{
			Vector3 directionToTarget = babies[i].transform.position - currentPosition;
			if(babies[i].activeInHierarchy == true)
			{
				float dSqrToTarget = directionToTarget.sqrMagnitude;
				if(dSqrToTarget < closestDistanceSqr)
				{
					closestDistanceSqr = dSqrToTarget;
					bestTarget = babies[i].transform;
				}
			}
		}
		for(int i = 0; i < mamas.Count; i++)
		{
			Vector3 directionToTarget = mamas[i].transform.position - currentPosition;
			if(mamas[i].activeInHierarchy == true)
			{
				float dSqrToTarget = directionToTarget.sqrMagnitude;
				if(dSqrToTarget < closestDistanceSqr)
				{
					closestDistanceSqr = dSqrToTarget;
					bestTarget = mamas[i].transform;
				}
			}
		}
		for(int i = 0; i < tanks.Count; i++)
		{
			Vector3 directionToTarget = tanks[i].transform.position - currentPosition;
			if(tanks[i].activeInHierarchy == true)
			{
				float dSqrToTarget = directionToTarget.sqrMagnitude;
				if(dSqrToTarget < closestDistanceSqr)
				{
					closestDistanceSqr = dSqrToTarget;
					bestTarget = tanks[i].transform;
				}
			}
		}
		return bestTarget;
	}

	void ShootEnemy(Transform targetLocation)
	{
		bullet.SetActive (true);
		bullet.transform.position = this.transform.position;
		bullet.transform.LookAt(targetLocation);
	}
}
