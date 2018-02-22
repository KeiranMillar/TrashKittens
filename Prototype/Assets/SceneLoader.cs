using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script that should allow for scenes to be easily loaded

public class SceneLoader : MonoBehaviour {

	// Public Variables, check build settings for appropriate values

	public int menuScene;
	public int mainScene;
	
	public void loadMainMenu ()
	{
		SceneManager.LoadScene (menuScene, LoadSceneMode.Single);
	}

	public void loadMainScene ()
	{
		SceneManager.LoadScene (mainScene, LoadSceneMode.Single);
	}
}
