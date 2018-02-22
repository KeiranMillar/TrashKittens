using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script intended as a game state manager
// Initially just tracks if game is paused or not

// Game state enum
public enum GameState {active, loading, paused, wave, upgrades};

public class GameStateManager : MonoBehaviour {



	// public variables
	public GameState currentGameState;
	public SceneLoader sceneLoader;

	public float startDelay = 3.0f;
	public float timeBetweenWaves = 5.0f;
	public Canvas mainUICanvas;
	public Canvas upgradeUICanvas;

	void Start()
	{
		currentGameState = GameState.paused;
		StartCoroutine (Delay (startDelay));
	}

	void Update()
	{
		//if (currentGameState == GameState.upgrades) 
		//{
		//	StartCoroutine (Delay (timeBetweenWaves));
		//}
	}

	// Enters paused State (currently does nothing but change time scale, changes in resource and health scripts needed)
	public void PauseGame () 
	{
		currentGameState = GameState.paused;
		Time.timeScale = 0.0f;
	}

	// Enters active state
	public void UnpauseGame()
	{
		currentGameState = GameState.active;
		Time.timeScale = 1.0f;
	}

	public void WaveEnd()
	{
		Time.timeScale = 0.0f;
		currentGameState = GameState.upgrades;
		mainUICanvas.gameObject.SetActive (false);
		upgradeUICanvas.gameObject.SetActive (true);
	}

	//public void LoadScene(string sceneName)
	//{
	//	currentGameState = GameState.loading;
	//
	//	StartCoroutine (sceneLoader.loadPrototype2D(sceneName));
	//}

	public GameState getState()
	{
		return currentGameState;
	}

	IEnumerator Delay(float delayTimer)
	{
		Time.timeScale = 0.0000001f;
		yield return new WaitForSeconds (delayTimer * Time.timeScale);
		currentGameState = GameState.active;
		Time.timeScale = 1.0f;
	}
}