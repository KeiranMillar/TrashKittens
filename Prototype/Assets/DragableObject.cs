using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour 
{
	public float gravScale = 10;
	public float speed = 10f; // speed value should be positive as it gets multiplied by -1
	public LayerMask collisionLayer;

	private bool grabbed;
	private Rigidbody2D objectRigidbody;
	private Vector2 forceToApply;

	void Start()
	{
		grabbed = false;
		objectRigidbody = GetComponent <Rigidbody2D>();
	}

	// fixed update, put physics related things here
	void FixedUpdate()
	{
		// if the object is being swiped, force applied is calculated in TouchDrag script
		if (grabbed == true) 
		{
			objectRigidbody.velocity = forceToApply;
		}
	}

	public void ApplyForce(Vector2 force)
	{
		forceToApply = force;
	}

	public void SetGrabbed(bool touchStatus)
	{
		grabbed = touchStatus;
	}

	public bool GetGrabbed()
	{
		return grabbed;
	}
}
