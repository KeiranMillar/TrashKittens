using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFiring : MonoBehaviour {

	public List<GameObject> babies;
	public GameObject ObjectPool;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform targetLocation;
		//ResourceCollection resourcesScript = drill.GetComponent<ResourceCollection>();
		ObjectPoolingBaby babiesScript = ObjectPool.GetComponent<ObjectPoolingBaby>();
		babies = babiesScript.pooledObjectsBaby;
		targetLocation = GetClosestEnemy(babies);
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
