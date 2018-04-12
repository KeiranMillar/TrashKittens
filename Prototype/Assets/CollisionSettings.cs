using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSettings : MonoBehaviour {

	// Disallows collsions between certain game objects.
	// see the Tags and Layers window for the list layers.


	void Start () 
	{
		// ungrabbed enemy on  ungrabbed enemy
		Physics2D.IgnoreLayerCollision (9, 9, true);
		// pickup on ungrabbed enemy
		Physics2D.IgnoreLayerCollision (11, 9, true);
		// pickup on drill
		Physics2D.IgnoreLayerCollision (11, 10, true);
		// pickup on turret bullets
		Physics2D.IgnoreLayerCollision (11, 12, true);
		// pickup on shooter spit
		Physics2D.IgnoreLayerCollision (11, 13, true);
		// shooter spit on ungrabbed enemy
		Physics2D.IgnoreLayerCollision (13, 9, true);
		// turret bullet on shooter spit
		Physics2D.IgnoreLayerCollision (12, 13, true);
		// grabbed enemy on ungrabbed enemy
		Physics2D.IgnoreLayerCollision (14, 9, true);
		// grabbed enemy on pickup
		Physics2D.IgnoreLayerCollision (14, 11, true);
		// grabbed enemy on grabbed enemy
		Physics2D.IgnoreLayerCollision (14, 14, true);
	}
}
