using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFiring : MonoBehaviour {

	public List<GameObject> babies;
	public List<GameObject> mamas;
	public List<GameObject> tanks;

	public GameObject ObjectPoolBaby;
	public GameObject ObjectPoolMama;
	public GameObject ObjectPoolTank;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform targetLocation;
		//ResourceCollection resourcesScript = drill.GetComponent<ResourceCollection>();
		ObjectPoolingBaby babiesScript = ObjectPoolBaby.GetComponent<ObjectPoolingBaby>();
		ObjectPoolingMama mamaScript = ObjectPoolMama.GetComponent<ObjectPoolingMama>();
		ObjectPoolingTank tankScript = ObjectPoolTank.GetComponent<ObjectPoolingTank>();
		babies = babiesScript.pooledObjectsBaby;
		mamas = mamaScript.pooledObjectsMama;
		tanks = tankScript.pooledObjectsTank;
		targetLocation = GetClosestEnemy(babies, mamas, tanks);
		ShootEnemy (targetLocation);
		
	}

	Transform GetClosestEnemy (List<GameObject> babies)
	{
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;
		for(int i = 0; i < babies.Count; i++)
		{
			Vector3 directionToTarget = babies[i].transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = babies[i].transform;
			}
		}
		for(int i = 0; i < mamas.Count; i++)
		{
			Vector3 directionToTarget = mamas[i].transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = mamas[i].transform;
			}
		}
		for(int i = 0; i < tanks.Count; i++)
		{
			Vector3 directionToTarget = tanks[i].transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = tanks[i].transform;
			}
		}
		return bestTarget;
	}

	void ShootEnemy(Transform targetLocation)
	{
		//GameObject obj;// = ObjectPoolingBullets.current.GetPooledObjectBullet ();

		//if (obj == null) 
		//{

		//} else {
		//	obj.transform.position = this.transform.position;
		//	obj.transform.LookAt(targetLocation);
		//	obj.SetActive (true);
		//}
	}

}
