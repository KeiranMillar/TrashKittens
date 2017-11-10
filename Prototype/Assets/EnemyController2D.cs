using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2D : MonoBehaviour {

	// A character controller with proper touch input
	// Currently provides no other fuctionality

	private Rigidbody2D enemyRigidbody;
	private bool isTouched;

	public float enemySpeed;
	public float forceMultiplier;
	public bool drawDebugLines;


	void Awake ()
	{
		enemyRigidbody = GetComponent<Rigidbody2D>();
		isTouched = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector2 touchDeltaPos = touch.deltaPosition;
			switch (touch.phase)
			{
			case TouchPhase.Began:
				// convert the touch location to world space and see if that point overlaps with enemiy's collider
				Debug.Log ("Enemy position " + enemyRigidbody.gameObject.transform.position.x.ToString () + " " + enemyRigidbody.gameObject.transform.position.y.ToString ());
				Debug.Log ("Touch pos " + touch.position.x.ToString () + " " + touch.position.y.ToString ());
				Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.0f));
				Debug.Log ("Touch world pos " + touchWorldPos.x.ToString () + " " + touchWorldPos.y.ToString ());
				if (enemyRigidbody.OverlapPoint(new Vector2 (touchWorldPos.x, touchWorldPos.y)) && (isTouched == false)) 
				{
					isTouched = true;
					Debug.Log ("Touch successful");

				}
				break;
			case TouchPhase.Moved:
				if (isTouched == true) 
				{
					enemyRigidbody.AddForce (touchDeltaPos * forceMultiplier);
				}
				break;
			case TouchPhase.Ended:
				Debug.Log ("touch ended");
				isTouched = false;
				break;
			}
		}
	}
}
