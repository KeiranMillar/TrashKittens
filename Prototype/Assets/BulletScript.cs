using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public EnemyController2D script;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// for when the bullet hits something
	void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log(coll.gameObject.tag);
		if (coll.gameObject.tag == "Enemy")
		{
			script = coll.gameObject.GetComponent<EnemyController2D>();
			script.hitpoints--;
		}
		this.gameObject.SetActive(false);
	}
}
