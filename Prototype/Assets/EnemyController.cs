using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// scirpt intended to replace the character controller 2d with a controller made for a 3d character

	public float swipeForceMultiplier = 2;
	public float speed = 0.2f; // speed value should be positive as it gets multiplied by -1
	public LayerMask collisionLayer;

	private bool alive;
	private bool grounded;
	private bool grabbed;
	private float groundedRadius = 0.1f;
	private Vector3 forceToApply;
	private Animator anim;
	private Transform groundCheckTransform;
	private Rigidbody enemyRigidbody;

	// Use this for initialization
	void Start () 
	{
		alive = true;
		grabbed = false;
		groundCheckTransform = transform.Find ("GroundCheck");
		enemyRigidbody = GetComponent <Rigidbody>();
		anim = GetComponent<Animator> ();
		forceToApply = new Vector3(0.0f, 0.0f, 0.0f);
	}


	// fixed update, put physics related things here
	void FixedUpdate()
	{
		grounded = false;
		// check if the enemy is touching the floor
		Collider[] colliders = Physics.OverlapSphere (groundCheckTransform.position, groundedRadius, collisionLayer);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders [i].gameObject != gameObject)
			{
				grounded = true;
				//Debug.Log ("Enemy is grounded");
			}
		}

		// if the enemy is grounded and not held
		if ((grounded == true) && (grabbed == false))
		{
			// move the enemy if alive
			if (alive == true) 
			{
				enemyRigidbody.velocity = new Vector3((-1.0f * speed), 0.0f, 0.0f);
			}
		}

		// if the enemy is being swiped, force applied calculated in update function
		if (grabbed == true) 
		{
			enemyRigidbody.velocity = forceToApply;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		// touch input
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.GetTouch(0);
			switch (touch.phase) 
			{
			case TouchPhase.Began:
				// use a raycast to determine if the touch has hit the enemy
				Ray touchRay = Camera.main.ScreenPointToRay (touch.position);
				RaycastHit touchRayHit;
				if (Physics.Raycast (touchRay, out touchRayHit)) 
				{
					if (touchRayHit.collider == gameObject.GetComponent<Collider>())
					{
						Debug.Log ("Enemy has been touched");
						grabbed = true;
					}
				}
				break;
			case TouchPhase.Moved:
				// sets the force to be applied to the enemy via delta touch
				if (grabbed == true)
				{
					forceToApply = new Vector3 (touch.deltaPosition.x, touch.deltaPosition.y, 0.0f);
					Debug.Log ("Moving rigidbody with touch");
				}
				break;
			case TouchPhase.Ended:
				if (grabbed = true) 
				{
					grabbed = false;
				}
				break;
			}
		}
	}
}
