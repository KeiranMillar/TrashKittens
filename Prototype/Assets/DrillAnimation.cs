using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillAnimation : MonoBehaviour {

	public Animator anim;

	// sets the upgrade animations to use
	public void setUpgradeLevel(int upgradeLevel)
	{
		anim.SetInteger("UpgradeLevel", upgradeLevel);
	}

	public int getUpgradeLevel()
	{
		return anim.GetInteger("UpgradeLevel");
	}


}
