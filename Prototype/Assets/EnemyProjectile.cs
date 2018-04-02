using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour 
{

	[SerializeField] bool despawnOnMiss = false;

	private Rigidbody2D projectileRigid;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate()
	{
		
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		// 8 is ground layer
		if (despawnOnMiss == true && coll.gameObject.layer == 8) 
		{
			gameObject.SetActive (false);
		}
		// 10 is the layer of the drill
		if (coll.gameObject.layer == 10) 
		{
			gameObject.SetActive (false);
			Debug.Log ("Projectile Hit");
			coll.gameObject.GetComponent<healthManagement2D> ().Damage(1);
		}
	}

	public void Spawn(float projectileSpeed)
	{
		gameObject.SetActive (true);
		projectileRigid = GetComponent<Rigidbody2D> ();
		projectileRigid.AddForce (new Vector3(-1, 1, 0) * projectileSpeed);
	}
}
