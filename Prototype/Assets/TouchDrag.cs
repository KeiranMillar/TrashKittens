using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for proper touch input flicking.
// Currently setup for 1 touch, can be changed if needed.

public class TouchDrag : MonoBehaviour 
{
	Vector2 forceToApply;

	public Rigidbody2D enemyRigidbody;

	// Use this for initialization
	void Start () 
	{
		//enemyRigidbody = null;
		forceToApply = new Vector2(0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.GetTouch(0);
			switch (touch.phase) 
			{
			// touch start
			// uses a raycast to get the rigidbody associated with an enemy
			case TouchPhase.Began:
				
				break;
			// finger drag
			// moves the rigid body to the touch position
			case TouchPhase.Moved:
				if (enemyRigidbody != null)
				{
					forceToApply = touch.deltaPosition;
					enemyRigidbody.AddForce (forceToApply);
					Debug.Log ("Moving rigidbody with touch");
				}
				break;
			// touch ended
			// drop the enemy with its current velocity
			case TouchPhase.Ended:
				enemyRigidbody = null;
				break;
			}
		}
	}
}
