using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script intended as a game state manager
// Initially just tracks if game is paused or not

public class GameStateManager : MonoBehaviour {

	// Game state enum
	public enum GameState {active, loading, paused};

	// public variables
	public GameState currentGameState;
	public SceneLoader sceneLoader;

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

	public void LoadScene(string sceneName)
	{
		currentGameState = GameState.loading;

		StartCoroutine (sceneLoader.loadPrototype2D(sceneName));
	}
}
