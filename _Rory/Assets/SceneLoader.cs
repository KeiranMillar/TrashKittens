using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script that should allow for scenes to be easily loaded

public class SceneLoader : MonoBehaviour {

	// Public Variables, check build settings for appropriate values

	public int mainMenuScene;
	public int prototypeScene;
	
	public void loadMainMenu ()
	{
		SceneManager.LoadScene (mainMenuScene, LoadSceneMode.Single);
	}

	public void loadPrototype ()
	{
		SceneManager.LoadScene (prototypeScene, LoadSceneMode.Single);
	}
}
