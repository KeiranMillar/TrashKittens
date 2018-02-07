using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script that should allow for scenes to be easily loaded

public class SceneLoader : MonoBehaviour {

	// Public Variables, check build settings for appropriate values

	public int mainMenuScene;
	public int prototypeScene;
	public int prototypeScene2D;
	public int prototypeScene3D;
	
	public void loadMainMenu ()
	{
		SceneManager.LoadScene (mainMenuScene, LoadSceneMode.Single);
	}

	public void loadPrototype ()
	{
		SceneManager.LoadScene (prototypeScene, LoadSceneMode.Single);
	}

	public IEnumerator loadPrototype2D (string sceneName)
	{
		AsyncOperation loadingOperation = SceneManager.LoadSceneAsync (sceneName);

		while (!loadingOperation.isDone) 
		{
			yield return null;
		}
	}

	public void loadPrototype3D ()
	{
		SceneManager.LoadScene (prototypeScene3D, LoadSceneMode.Single);
	}
}
