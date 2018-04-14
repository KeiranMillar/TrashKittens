using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	EnemyController2D script;
	TurretFiring scriptTimer;
	public GameObject timerReset;
	public GameObject drill;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// for when the bullet hits something
	void OnCollisionEnter2D(Collision2D coll)
	{
		DrillController drillController = drill.GetComponent<DrillController>();

		//If the target is an enemy, take health off them
		if (coll.gameObject.tag == "Enemy")
		{
			script = coll.gameObject.GetComponent<EnemyController2D>();
			script.DealLaserDamage(1 * drillController.turretDamage);
		}
		this.gameObject.SetActive(false);

		//Reset the timer for the bullet being reset
		scriptTimer = timerReset.GetComponent<TurretFiring>();
		scriptTimer.ResetTimer();
	}
}
