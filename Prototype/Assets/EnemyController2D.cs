using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2D : MonoBehaviour {

	// scirpt intended to replace the character controller 2d with a controller made for a 3d character

	public float swipeForceMultiplier = 2;
	public float speed = 10f; // speed value should be positive as it gets multiplied by -1
	public float killVelocity = 0.5f; // the velocity at which the alien will die when hitting the floor
	public bool dieOnAttack = false;
	public float gravScale = 10;
	public LayerMask collisionLayer;

	private bool alive;
	private bool grounded;
	private bool grabbed;
    private bool height_death;
    private bool survive_height_death;
	private Vector2 forceToApply;
	private Vector3 touchWorldPos;
	private Animator anim;
	private Rigidbody2D enemyRigidbody;

    // death variables
    private Vector2 highestPos_; // to keep the highest position after the flick
    //private bool dead_;
    public float deathPos;

    // Use this for initialization
    void Start () 
	{
		alive = true;
		grabbed = false;
        height_death = false;
		enemyRigidbody = GetComponent <Rigidbody2D>();
		anim = GetComponent<Animator>();
		forceToApply.Set(0,0);
		touchWorldPos.Set(0,0,0);
		enemyRigidbody.gravityScale = gravScale;
        
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
			touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance (new Vector3 (0, 0, 0), Camera.main.transform.position)));
			forceToApply.Set ((touchWorldPos.x - enemyRigidbody.position.x), (touchWorldPos.y - enemyRigidbody.position.y));
		}
	}

	void OnMouseUp()
	{
		//Debug.Log ("mouse up");
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
					forceToApply.Set ((touchWorldPos.x - enemyRigidbody.position.x), (touchWorldPos.y - enemyRigidbody.position.y));
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

        // remember the heighest position
        if (highestPos_.y < this.transform.position.y)
            highestPos_.y = this.transform.position.y;

        // death height
        if (highestPos_.y > deathPos)
        {
            //Debug.Log("dead");
            height_death = true;
            //anim.SetBool("Death", true);
        }
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
            if (killVelocity <= enemyRigidbody.velocity.magnitude || height_death)
			{
				//Debug.Log("am dead");
				enemyRigidbody.velocity.Set(0.0f, 0.0f);
                alive = false;
                
                anim.SetBool("Death", true);

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
