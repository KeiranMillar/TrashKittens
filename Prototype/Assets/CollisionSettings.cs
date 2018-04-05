using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSettings : MonoBehaviour {

	// Disallows collsions between certain game objects.
	// see the Tags and Layers window for the list layers.


	void Start () 
	{

		Physics2D.IgnoreLayerCollision (9, 9, true);
		Physics2D.IgnoreLayerCollision (11, 9, true);
		Physics2D.IgnoreLayerCollision (11, 10, true);
		Physics2D.IgnoreLayerCollision (11, 12, true);
		Physics2D.IgnoreLayerCollision (11, 13, true);
		Physics2D.IgnoreLayerCollision (13, 9, true);
		Physics2D.IgnoreLayerCollision (12, 13, true);
	}
}
