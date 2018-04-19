using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretFiring : MonoBehaviour {

	public List<GameObject> babies;
	public List<GameObject> mamas;
	public List<GameObject> shooters;
	public List<GameObject> tanks;

	public GameObject bullet;
	public GameObject turretHead;
	public Button fireButton;
	public float bulletLife = 5.0f;
	public int bulletSpeed = 1000000;

	public GameObject SpawnManager;

	public bool fireNow = false;
	public bool cooledDown = true;
	public float cooldownTimer = 5.0f;

	// Use this for initialization
	void Start () 
	{
		turretHead.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(cooledDown)
		{
			//If the gun has been fired
			if(fireNow)
			{
				Transform targetLocation;
				ObjectPoolingBaby babiesScript = SpawnManager.GetComponent<ObjectPoolingBaby>();
				ObjectPoolingMama mamaScript = SpawnManager.GetComponent<ObjectPoolingMama>();
				ObjectPoolingShooter shooterScript = SpawnManager.GetComponent<ObjectPoolingShooter>();
				ObjectPoolingTank tankScript = SpawnManager.GetComponent<ObjectPoolingTank>();
				babies = babiesScript.pooledObjectsBaby;
				mamas = mamaScript.pooledObjectsMama;
				shooters = shooterScript.pooledObjectsShooter;
				tanks = tankScript.pooledObjectsTank;

				//Get the closest enemy to the turret
				targetLocation = GetClosestEnemy(babies, mamas, shooters, tanks);

				//If the target is valid and the bullet isn't already fired
				if(targetLocation && !bullet.activeInHierarchy)
				{
					//Shoot the enemy
					Vector3 difference = targetLocation.position - transform.position;
					float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
					turretHead.transform.rotation = Quaternion.Euler(0.0f, 180.0f, (rotationZ + 3.0f));
					ShootEnemy (targetLocation);
					cooledDown = false;
					fireButton.interactable = false;
					cooldownTimer = 5.0f;
				}
				//Reset the turret to not fire again
				fireNow = false;
			}
		}
		//If the bullet is fired
		if(bullet.activeInHierarchy)
		{
			//Move it towards its target
			bullet.transform.Translate(this.transform.forward * bulletSpeed * Time.deltaTime, Space.Self);
			//Tick down its timer
			bulletLife -= Time.deltaTime;
			//If it's less than bulletLife then assume the bullet has missed and reset it
			if(bulletLife < 0)
			{
				bullet.SetActive(false);
				bulletLife = 5.0f;
			}
		}
		cooldownTimer -= Time.deltaTime;
		if ( cooldownTimer < 0 )
		{
			cooledDown = true;
			cooldownTimer = 5.0f;
			fireButton.interactable = true;
		}
	}
		
	Transform GetClosestEnemy (List<GameObject> babies, List<GameObject> mamas, List<GameObject> shooters, List<GameObject> tanks)
	{
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;

		//Cycle through all the enemies and find which one is closest to the turret
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
		for(int i = 0; i < shooters.Count; i++)
		{
			Vector3 directionToTarget = shooters[i].transform.position - currentPosition;
			if(shooters[i].activeInHierarchy == true)
			{
				float dSqrToTarget = directionToTarget.sqrMagnitude;
				if(dSqrToTarget < closestDistanceSqr)
				{
					closestDistanceSqr = dSqrToTarget;
					bestTarget = shooters[i].transform;
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
		//Activate the bullet and set it location at the turret, looking at its target
		targetLocation.position = new Vector3 (targetLocation.position.x, targetLocation.position.y + 0.5f, targetLocation.position.z);
		this.GetComponent<AudioSource>().Play();
		bullet.SetActive (true);
		bullet.transform.position = this.transform.position;
		bullet.transform.LookAt(targetLocation);
	}

	public void FireNow(bool fireState)
	{
		//Allows changing of the firestate;
		fireNow = fireState;
	}

	public void ResetTimer()
	{
		//Reset the life of the bullet, used when the bullet hits its target
		bulletLife = 5.0f;
	}
}
