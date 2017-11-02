using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
		[SerializeField] private float m_Speed = -0.2f;
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

		private float m_Distance = 10;
		private Vector2 beginTouch;
		private Vector2 exitTouch;
		private Vector2 forceToApply;

		private Vector2 lastPos;
		private Vector2 thisPos;

		public float speed = 0.1f;

		Vector3 enterPosition_;
		Quaternion enterRotation_;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

			// mouse drag
			beginTouch = new Vector2(0.0f, 0.0f);
			exitTouch = new Vector2(0.0f, 0.0f);
			forceToApply = new Vector2(0.0f, 0.0f);
        }

		void OnMouseEnter()
		{
			beginTouch.x = Input.mousePosition.x;
			beginTouch.y = Input.mousePosition.y;
		}

		void OnMouseDrag()
		{
			lastPos = thisPos;
			thisPos = transform.position;
			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_Distance);        
			Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = objPosition;
		}

		void OnMouseUp()
		{
			thisPos = transform.position;
			Vector2 velocity = thisPos - lastPos;
			exitTouch.x = Input.mousePosition.x;
			exitTouch.y = Input.mousePosition.y;

//			Debug.Log(forceToApply);
//			forceToApply = exitTouch - beginTouch;
//			float xForce, yForce;
			// if (forceToApply.x >= 150.0f) forceToApply.x = 150.0f;
			// if (forceToApply.y >= 150.0f) forceToApply.y = 150.0f;
			m_Rigidbody2D.velocity = velocity * 3.0f;
			//rb.AddForce(forceToApply * mult * Time.deltaTime, ForceMode2D.Impulse);
		}

		void OnDrawGizmos()
		{
			//Gizmos.color = Color.red;
			//Gizmos.DrawLine(Camera.main.ViewportToWorldPoint( beginTouch), Camera.main.ViewportToWorldPoint(exitTouch));
		}

		private void Update()
		{
//			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
//			{
//				// Get movement of the finger since last frame
//				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//
//				// Move object across XY plane
//				transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
//			}
			for (int i = 0; i < Input.touchCount; ++i)
			{
				if (Input.GetTouch (i).phase == TouchPhase.Began) {
					Debug.Log ("touch began");
				}
					//clone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
				if (Input.GetTouch (i).phase == TouchPhase.Moved) {
					Debug.Log ("touch moved");
					Vector2 touchDeltaPosition = Input.GetTouch (i).deltaPosition;
				}

				if (Input.GetTouch(i).phase == TouchPhase.Ended) {
					Debug.Log ("touch ended");
				}	
			}

			if (Input.GetKey(KeyCode.R)) {
				enterPosition_ = this.transform.position;
				enterPosition_.x = 0.0f;
				enterPosition_.y = 0.0f;

				enterRotation_ = this.transform.rotation;

				this.transform.SetPositionAndRotation (enterPosition_, enterRotation_);
			}

		}

        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

			if (m_Grounded) Move(m_Speed, false, false);

			if (this.transform.position.x < -45.0f) {
				enterPosition_ = this.transform.position;
				enterPosition_.x = 42.0f;

				enterRotation_ = this.transform.rotation;

				this.transform.SetPositionAndRotation (enterPosition_, enterRotation_);
			}
			else if (this.transform.position.x > 45.0f) {
				enterPosition_ = this.transform.position;
				enterPosition_.x = -42.0f;

				enterRotation_ = this.transform.rotation;

				this.transform.SetPositionAndRotation (enterPosition_, enterRotation_);
			}
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
              
				// If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }
			
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

//		private void OnTriggerEnter2D(Collider2D other)
//		{
//			if (other.CompareTag == "reposition") {
//				enterPosition_ = this.transform.position;
//				enterPosition_.x = -enterPosition_.x + 5.0f;
//				
//				enterRotation_ = this.transform.rotation;
//
//				this.transform.SetPositionAndRotation (enterPosition_, enterRotation_);
//			}
//		}
    }
}
