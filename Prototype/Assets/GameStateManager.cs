using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script intended as a game state manager
// Initially just tracks if game is paused or not

// Game state enum
public enum GameState {pregame, active, loading, paused, wave, upgrades};

public class GameStateManager : MonoBehaviour {



	// public variables
	public GameState currentGameState;
	public SceneLoader sceneLoader;
	public ObjectPoolingGeneric pickupPool;
	public GameObject resourceSpawnPoint;
	public EnemySpawning spawnManager;
	public MusicSelector musicSelector;
	public GameObject upgradeButton;
	public Canvas mainUICanvas;
	public Canvas upgradeUICanvas;
	public AudioClip victoryAudio;


	public float baseResourcesPerWave = 25f;
	public float bonusResourcePerWaveNumber = 5;
	public float bonusResourceFromUpgrades = 0;
	public float startDelay = 3.0f;
	public float timeBetweenWaves = 5.0f;

	private AudioSource waveAudioSource;
	private float preGameTimer;

	void Start()
	{
		currentGameState = GameState.pregame;
		//StartCoroutine (Delay (startDelay));
		waveAudioSource = GetComponent<AudioSource> ();
		preGameTimer = 0;
	}

	void Update()
	{
		//if (currentGameState == GameState.upgrades) 
		//{
		//	StartCoroutine (Delay (timeBetweenWaves));
		//}

		if (currentGameState == GameState.pregame) 
		{
			preGameTimer += Time.deltaTime;
			if (preGameTimer > startDelay) 
			{
				currentGameState = GameState.active;
			}
		}
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
		//Time.timeScale = 0.0f;
		currentGameState = GameState.upgrades;
		//mainUICanvas.gameObject.SetActive (false);
		//upgradeUICanvas.gameObject.SetActive (true);
		float waveReward = baseResourcesPerWave + bonusResourceFromUpgrades + (spawnManager.wave * bonusResourcePerWaveNumber);
		int pickupCount = Random.Range(1, 5);
		for(int i = 1; i <= pickupCount; i++)
		{
			PickupResource spawnedPickup = pickupPool.GetPooledObject().GetComponent<PickupResource>();
			spawnedPickup.Spawn (resourceSpawnPoint.transform.position, waveReward/pickupCount);
		}
		upgradeButton.SetActive (true);
		waveAudioSource.PlayOneShot (victoryAudio);
		musicSelector.StopCurrentTrack ();
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