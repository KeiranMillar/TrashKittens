using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2D : MonoBehaviour {

	// scirpt intended to replace the character controller 2d with a controller made for a 3d character
	// this script is placeholder, and will be replaced by a generic base alien class.
	// the new base alien class will inherit from the dragable object class.
	// specific alien types will then inherit from the base alien.

	public float swipeForceMultiplier = 2;
	public float speed = 10f; // speed value should be positive as it gets multiplied by -1
	public float killVelocity = 0.5f; // the velocity at which the alien will die when hitting the floor
	public bool dieOnAttack = false;
	public float gravScale = 10;
	public LayerMask collisionLayer;

	private bool alive;
	private bool grounded;
	private bool grabbed;
	private float lastYVelocity;
	private Vector2 forceToApply;
	private Vector3 touchWorldPos;
	private Animator anim;
	private Rigidbody2D enemyRigidbody;

    // Use this for initialization
    void Start () 
	{
		alive = true;
		grabbed = false;
		enemyRigidbody = GetComponent <Rigidbody2D>();
		anim = GetComponent<Animator>();
		forceToApply.Set(0,0);
		touchWorldPos.Set(0,0,0);
		enemyRigidbody.gravityScale = gravScale;
		lastYVelocity = 0f;
        
        anim.SetBool("Ground", true);
        anim.SetBool("Death", false);
	}

	void OnMouseDown()
	{
		//Debug.Log("mouse down");
		touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance (new Vector3 (0, 0, 0), Camera.main.transform.position)));
		if (enemyRigidbody.GetComponent<Collider2D> ().OverlapPoint (new Vector2 (touchWorldPos.x, touchWorldPos.y))) 
		{
			//Debug.Log ("Enemy has been touched");
			grabbed = true;
			enemyRigidbody.gravityScale = 0f;
		}
	}

	void OnMouseDrag()
	{
		//Debug.Log("mouse drag");
		if (grabbed == true) 
		{
			enemyRigidbody.drag = 0;
			float xForce = 0;
			float yForce = 0;
			touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance (new Vector3 (0, 0, 0), Camera.main.transform.position)));
			// if the rigidbody is not at the touch position, then apply force to move it in that direction
			if (!enemyRigidbody.OverlapPoint (new Vector2 (touchWorldPos.x, touchWorldPos.y))) {
				xForce = (touchWorldPos.x - enemyRigidbody.position.x);
				yForce = (touchWorldPos.y - enemyRigidbody.position.y);
				float forceMagnitude = Mathf.Sqrt (Mathf.Pow (xForce, 2) + Mathf.Pow (yForce, 2));
				xForce = (xForce / forceMagnitude) * swipeForceMultiplier;
				yForce = (yForce / forceMagnitude) * swipeForceMultiplier;
				forceToApply.Set (xForce, yForce);
			}
			else 
			{
				//xForce = Mathf.Lerp (enemyRigidbody.velocity.x, 0, 1);
				//yForce = Mathf.Lerp (enemyRigidbody.velocity.y, 0, 1);
				enemyRigidbody.drag = 10000;
			}
		}
	}

	void OnMouseUp()
	{
		//Debug.Log ("mouse up");
		enemyRigidbody.drag = 0;
		if (grabbed == true) 
		{
			grabbed = false;
			enemyRigidbody.gravityScale = gravScale;
		}
	}

	// fixed update, put physics related things here
	void FixedUpdate()
	{
		// if the enemy is being swiped, force applied calculated in update function
		if (grabbed == true) 
		{
			enemyRigidbody.velocity = forceToApply;
		}
		else if(grounded == true)
		{
			//Debug.Log("Moving");
			enemyRigidbody.velocity = new Vector2(-speed, 0);
		}

        anim.SetBool("Ground", grounded);
    }

	// Update is called once per frame
	void Update () 
	{
		// touch input
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			switch (touch.phase) {
			case TouchPhase.Began:
				// get the touches coords in world space, check if it's in the collider
				touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, Vector3.Distance (new Vector3 (0, 0, 0), Camera.main.transform.position)));
				if (enemyRigidbody.GetComponent<Collider2D> ().OverlapPoint (new Vector2 (touchWorldPos.x, touchWorldPos.y))) {
					//Debug.Log ("Enemy has been touched");
					grabbed = true;
					enemyRigidbody.gravityScale = 0f;
				}
				break;
			case TouchPhase.Moved:
					// sets the force to be applied to the enemy
				if (grabbed == true) {
					touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, Vector3.Distance (new Vector3 (0, 0, 0), Camera.main.transform.position)));
					// the force applied to the enemy should be a unit vector in the direction eading from the enemy to touch position. This unit vector is then multiplied.
					float xForce = (touchWorldPos.x - enemyRigidbody.position.x);
					float yForce = (touchWorldPos.y - enemyRigidbody.position.y);
					float forceMagnitude = Mathf.Sqrt (Mathf.Pow (xForce, 2) + Mathf.Pow (yForce, 2));
					xForce = (xForce / forceMagnitude) * swipeForceMultiplier;
					yForce = (yForce / forceMagnitude) * swipeForceMultiplier;
					forceToApply.Set (xForce, yForce);
				}
				break;
			case TouchPhase.Ended:
				if (grabbed == true) {
					grabbed = false;
					enemyRigidbody.gravityScale = gravScale;
				}
				break;
			}
		}
		lastYVelocity = enemyRigidbody.velocity.y;
    }

	// for when the enemy hits something
	void OnCollisionEnter2D(Collision2D coll)
	{
		// for hitting the ground (ground layer is layer 8)
		if (coll.gameObject.layer == 8) 
		{
			grounded = true;
            //anim.SetBool("Ground", grounded);
            //Debug.Log (enemyRigidbody.velocity.magnitude.ToString());
            //Debug.Log (killVelocity.ToString());
            // death check
			//Debug.Log(lastYVelocity);
			//Debug.Log(Mathf.Sqrt(Mathf.Pow(enemyRigidbody.velocity.x, 2) + Mathf.Pow(lastYVelocity, 2)));
			if (killVelocity <= (Mathf.Sqrt(Mathf.Pow(enemyRigidbody.velocity.x, 2) + Mathf.Pow(lastYVelocity, 2))))
			{
				enemyRigidbody.velocity.Set(0.0f, 0.0f);
                anim.SetBool("Death", true);
                //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                gameObject.SetActive(false);
			}
		}
	}

	void OnCollisionExit2D(Collision2D collisionInfo)
	{
		if (collisionInfo.gameObject.layer == 8) 
		{
			grounded = false;
           //anim.SetBool("Ground", grounded);
        }
	}
}
