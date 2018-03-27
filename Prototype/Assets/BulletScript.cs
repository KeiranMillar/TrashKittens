using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public EnemyController2D script;
	public TurretFiring scriptTimer;
	public GameObject timerReset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// for when the bullet hits something
	void OnCollisionEnter2D(Collision2D coll)
	{
		//If the target is an enemy, take health off them
		if (coll.gameObject.tag == "Enemy")
		{
			script = coll.gameObject.GetComponent<EnemyController2D>();
			script.DealLaserDamage(1);
		}
		this.gameObject.SetActive(false);

		//Reset the timer for the bullet being reset
		scriptTimer = timerReset.GetComponent<TurretFiring>();
		scriptTimer.ResetTimer();
	}
}
