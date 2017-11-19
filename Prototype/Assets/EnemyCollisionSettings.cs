using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionSettings : MonoBehaviour {

	// disables collisions between enemies when set to false.
	// make sure the aliens are set to the "enemy" layer in the editor

	public bool allowEnemyOnEnemyCollision = false;

	void Start () 
	{
		if (allowEnemyOnEnemyCollision == false) 
		{
			Physics.IgnoreLayerCollision (9, 9, true);
		}
	}
}
