using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scripts that controls transition between menu states.

public class menuScript : MonoBehaviour 
{
	// Public variables
	public GameObject mainMenu;
	public GameObject optionsMenu;
	public GameObject upgradesMenu;
	public Button toOptionsButton;
	public Button backOptionsButton;
	public Button toUpgradesButton;
	public Button backUpgradesButton;

	// Private variable


	// Use this for initialization
	void Start () 
	{
		// Set up listeners for the transition buttons
		toOptionsButton.onClick.AddListener(TransitionOptionsMenu);
		toUpgradesButton.onClick.AddListener(TransitionUpgradesMenu);
		backOptionsButton.onClick.AddListener(TransitionMainMenu);
		backUpgradesButton.onClick.AddListener(TransitionMainMenu);
	}

	// Fucntions for transitioning between menues

	void TransitionMainMenu ()
	{
		mainMenu.SetActive (true);
		optionsMenu.SetActive (false);
		upgradesMenu.SetActive (false);
	}

	void TransitionOptionsMenu ()
	{
		mainMenu.SetActive (false);
		optionsMenu.SetActive (true);
		upgradesMenu.SetActive (false);
	}

	void TransitionUpgradesMenu()
	{
		mainMenu.SetActive (false);
		optionsMenu.SetActive (false);
		upgradesMenu.SetActive (true);
	}
}
