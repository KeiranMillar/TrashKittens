using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyController2D : MonoBehaviour {

	// scirpt intended to replace the character controller 2d with a controller made for a 3d character
	// this script is placeholder, and will be replaced by a generic base alien class.
	// the new base alien class will inherit from the dragable object class.
	// specific alien types will then inherit from the base alien.

	public float hitpoints = 1;
	public float swipeForceMultiplier = 2;
	public float speed = 10f; // speed value should be positive as it gets multiplied by -1
	public float killVelocity = 0.5f; // the velocity at which the alien will die when hitting the floor
	public bool dieOnAttack = false;
	public float gravScale = 10;
	public AudioClip[] clips;
	public AudioMixerGroup output;
	public LayerMask collisionLayer;

	private bool alive;
	private bool grounded;
	private bool grabbed;
	private float lastYVelocity;
	private float maxHitpoints;
	private Vector2 forceToApply;
	private Vector3 touchWorldPos;
	private Animator anim;
	private Rigidbody2D enemyRigidbody;
	private Collider2D enemyCollider;

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
		maxHitpoints = hitpoints;
        anim.SetBool("Grounded", true);
        anim.SetBool("Dead", false);
		enemyCollider = gameObject.GetComponent<Collider2D>();
	}

	void OnMouseDown()
	{
		//Debug.Log("mouse down");
		touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance (new Vector3 (0, 0, 0), Camera.main.transform.position)));
		if (enemyRigidbody.OverlapPoint(new Vector2(touchWorldPos.x, touchWorldPos.y))) 
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
			//enemyRigidbody.drag = 0;
			float xForce = 0;
			float yForce = 0;
			touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance (new Vector3 (0, 0, 0), Camera.main.transform.position)));
			Debug.DrawLine (touchWorldPos, new Vector3 (0, -1, 0), Color.yellow, 0.0f, false);
			// if the rigidbody is not at the touch position, then apply force to move it in that direction
			//!enemyRigidbody.OverlapPoint (new Vector2 (touchWorldPos.x, touchWorldPos.y))
			if (!enemyRigidbody.OverlapPoint (new Vector2 (touchWorldPos.x, touchWorldPos.y))) 
			{
				xForce = (touchWorldPos.x - enemyCollider.bounds.center.x);
				yForce = (touchWorldPos.y - enemyCollider.bounds.center.y);
				float forceMagnitude = Mathf.Sqrt (Mathf.Pow (xForce, 2) + Mathf.Pow (yForce, 2));
				xForce = (xForce / forceMagnitude) * swipeForceMultiplier;
				yForce = (yForce / forceMagnitude) * swipeForceMultiplier;
				forceToApply.Set (xForce, yForce);
			}
			else 
			{
				xForce = (touchWorldPos.x - enemyCollider.bounds.center.x);
				yForce = (touchWorldPos.y - enemyCollider.bounds.center.y);
				forceToApply.Set (xForce, yForce);
			}
		}
	}

	void OnMouseUp()
	{
		//Debug.Log ("mouse up");
		//enemyRigidbody.drag = 0;
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
			Debug.DrawLine (enemyRigidbody.position, new Vector3 (enemyCollider.bounds.center.x + enemyRigidbody.velocity.x, enemyCollider.bounds.center.y + enemyRigidbody.velocity.y, gameObject.transform.position.z), Color.blue, 0.0f, false);
		}
		else if(grounded == true)
		{
			//Debug.Log("Moving");
			enemyRigidbody.velocity = new Vector2(-speed, 0);
		}

        anim.SetBool("Grounded", grounded);
    }

	// Update is called once per frame
	void Update () 
	{
		// death check
		if (hitpoints <= 0) 
		{
			grabbed = false;
			enemyRigidbody.velocity.Set(0.0f, 0.0f);
			anim.SetBool("Dead", true);
			gameObject.SetActive(false);
			hitpoints = maxHitpoints;
		}

		// touch input
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			switch (touch.phase) {
			case TouchPhase.Began:
				// get the touches coords in world space, check if it's in the collider
				touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, Vector3.Distance (new Vector3 (0, 0, 0), Camera.main.transform.position)));
				if (Physics2D.OverlapCircle(new Vector2 (touchWorldPos.x, touchWorldPos.y), 0.2f) == enemyCollider) 
				{
					//Debug.Log ("Enemy has been touched");
					grabbed = true;
					enemyRigidbody.gravityScale = 0f;
				}
				break;
			case TouchPhase.Moved:
					// sets the force to be applied to the enemy
				if (grabbed == true) 
				{
					float xForce = (touchWorldPos.x - enemyCollider.bounds.center.x);
					float yForce = (touchWorldPos.y - enemyCollider.bounds.center.y);
					touchWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, Vector3.Distance (new Vector3 (0, 0, 0), Camera.main.transform.position)));
					// if the rigidbody is not at the touch position, then apply force to move it in that direction
					//!enemyRigidbody.OverlapPoint (new Vector2 (touchWorldPos.x, touchWorldPos.y))
					Debug.DrawLine(enemyRigidbody.position, new Vector3 (enemyCollider.bounds.center.x + xForce, enemyCollider.bounds.center.y + yForce, gameObject.transform.position.z), Color.white, 0.0f, false);
					if (!enemyRigidbody.OverlapPoint (new Vector2 (touchWorldPos.x, touchWorldPos.y))) 
					{
						float forceMagnitude = Mathf.Sqrt (Mathf.Pow (xForce, 2) + Mathf.Pow (yForce, 2));
						xForce = (xForce / forceMagnitude) * swipeForceMultiplier;
						yForce = (yForce / forceMagnitude) * swipeForceMultiplier;
						Debug.DrawLine (enemyRigidbody.position, new Vector3 (enemyCollider.bounds.center.x + xForce, enemyCollider.bounds.center.y + yForce, gameObject.transform.position.z), Color.red, 0.0f, false);
					} 
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
		// for collisions (ground layer is layer 8, the drill is layer 10)
		if ((coll.gameObject.layer == 8) || (coll.gameObject.layer == 10))
		{
			// Play squish sound (Miro's work)
			if (coll.gameObject.layer == 8) 
			{
				grounded = true;
				// generate random number between 0 and the number of clips in the array
				int randomNumber = Random.Range (0, clips.Length);
				// add audiosource component to the object
				AudioSource source = gameObject.AddComponent<AudioSource>();
				// set the cip
				source.clip = clips[randomNumber];
				// set the output
				source.outputAudioMixerGroup = output;
				// play sound
				source.Play ();
				// destroy AudioSOurce when finished playing
				//Destroy(source, clips[randomNumber].length);

			}
			//Debug.Log (Mathf.Sqrt(Mathf.Pow(enemyRigidbody.velocity.x, 2) + Mathf.Pow(lastYVelocity, 2)));
			// check if damage is dealt
			if (killVelocity <= (Mathf.Sqrt(Mathf.Pow(enemyRigidbody.velocity.x, 2) + Mathf.Pow(lastYVelocity, 2))))
			{
				hitpoints--;
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
