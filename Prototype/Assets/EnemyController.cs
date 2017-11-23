using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// scirpt intended to replace the character controller 2d with a controller made for a 3d character

	public float swipeForceMultiplier = 2;
	public float speed = 0.2f; // speed value should be positive as it gets multiplied by -1
	public float killVelocity = 0.5f; // the velocity at which the alien will die when hitting the floor
	public LayerMask collisionLayer;

	private bool alive;
	private bool grounded;
	private bool grabbed;
	private Vector3 forceToApply;
	private Animator anim;
	private Rigidbody enemyRigidbody;

	// Use this for initialization
	void Start () 
	{
		alive = true;
		grabbed = false;
		enemyRigidbody = GetComponent <Rigidbody>();
		anim = GetComponent<Animator> ();
		forceToApply = new Vector3(0.0f, 0.0f, 0.0f);
	}


	// fixed update, put physics related things here
	void FixedUpdate()
	{
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
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				switch (touch.phase) 
				{
					case TouchPhase.Began:
					// use a raycast to determine if the touch has hit the enemy
						Ray touchRay = Camera.main.ScreenPointToRay (touch.position);
						RaycastHit touchRayHit;
						if (Physics.Raycast (touchRay, out touchRayHit)) 
						{
							if (touchRayHit.collider == gameObject.GetComponent<Collider> ()) 
							{
								//Debug.Log ("Enemy has been touched");
								grabbed = true;
							}
						}
						break;
					case TouchPhase.Moved:
					// sets the force to be applied to the enemy via delta touch
						if (grabbed == true) 
						{
							forceToApply = new Vector3 (touch.deltaPosition.x, touch.deltaPosition.y, 0.0f);
							//Debug.Log ("Moving rigidbody with touch");
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

	// for when the enemy hits something
	void OnCollisionEnter(Collision coll)
	{
		// for hitting the ground (ground layer is layer 8)
		if (coll.gameObject.layer == 8) 
		{
			grounded = true;
			//Debug.Log (enemyRigidbody.velocity.magnitude.ToString());
			//Debug.Log (killVelocity.ToString());
			// death check
			if (killVelocity <= enemyRigidbody.velocity.magnitude)
			{
				// alive = false;
				//Debug.Log("am dead");
				enemyRigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
				gameObject.SetActive(false);
			}
		}
	}

	// for continued contact with something (the floor mostly, maybe the drill as well)
	void OnCollisionStay(Collision coll)
	{
		if (coll.gameObject.layer == 8)
		{
			Debug.Log ("Contact with floor");
			if (alive == true) 
			{
				enemyRigidbody.velocity = new Vector3 ((-1 * speed), enemyRigidbody.velocity.y, 0);
			}
		}
	}

	void OnCollisionExit(Collision coll)
	{
		if (coll.gameObject.layer == 8) 
		{
			grounded = false;
		}
	}
}
