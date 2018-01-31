using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for proper touch input flicking.
// Script is a work in progress and is non-fuctional
// Based on the old code used for enemy touch detection, which it is intended to replace.
// Touch response is the domain of the dragable object class and it's derivatives.

public class TouchDrag : MonoBehaviour 
{
	Vector2 forceToApply;

	private DragableObject touchedObject;
	private Vector3 touchWorldPos;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// touch input
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			RaycastHit hit;
			int collisionNumber;
			Collider2D[] touchedColliders = new Collider2D[10];
			float distance = 30;
			switch (touch.phase) {
			case TouchPhase.Began:
				// get the touches coords in world space
				touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, distance));
				collisionNumber = Physics2D.OverlapCircleNonAlloc(new Vector2 (touchWorldPos.x, touchWorldPos.y), (touch.radius + touch.radiusVariance), touchedColliders);
				if (collisionNumber > 0)
				{
					Debug.Log ("New Touch Input Response");

				}
				break;
			case TouchPhase.Moved:
				// sets the force to be applied to the enemy
				// calculate the force and use the DragableObject to apply it to the object.
				break;
			case TouchPhase.Ended:
				// set the object as not grabbed
				break;
			}
		}
	}

}
