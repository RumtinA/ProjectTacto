using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	private bool animStarted, musicOptions, soundFXOptions;
	public Canvas canvas;
	public GameObject play, options, credits, vsComputer, vsPlayer, tutorial, backButton, creditsText, host, join, enterName, confirmName, musicButton, soundFxButton, optionsText;	
	private GameObject[] buttonsToSpawn;
	private GameObject[] spawnedButtons;
	private string menuTracker;

	void Start()
	{
		animStarted = false; // No animation is playing
		if (PlayerPrefs.GetInt ("Music Options") == 1) // If music was not muted previously
		{
			musicOptions = true; // Then it should be on
			AudioManager.audioInstance.audioSource.mute = false; // The music should not be muted
		}
		else // If it was muted previously
		{
			AudioManager.audioInstance.audioSource.mute = true; // Keep it muted
			musicOptions = false; // Then it is off
		}
		if (PlayerPrefs.GetInt ("FX Options") == 1) // If the sound effects weren't muted before
		{
			soundFXOptions = true; // Then it should be on
			AudioManager.audioInstance.audioSourceFX.mute = false; // Not muted
		}
		else
		{	
			soundFXOptions = false; // Then it should be off
			AudioManager.audioInstance.audioSourceFX.mute = true; // Muted
		}
		menuTracker = "Main Menu";
	}

	public bool SetMusicOptions(Button theButton) // Enables or disables music
	{
		musicOptions = !musicOptions;
		if (!musicOptions)
		{
			AudioManager.audioInstance.audioSource.mute = true;
			PlayerPrefs.SetInt ("Music Options", 0);
		}
		else 
		{
			AudioManager.audioInstance.audioSource.mute = false;
			PlayerPrefs.SetInt ("Music Options", 1);
		}
		return musicOptions;
	}

	public bool SetSoundFXOptions(Button theButton) // Enables or disables sound effects
	{
		soundFXOptions = !soundFXOptions;
		if (!soundFXOptions)
		{
			AudioManager.audioInstance.audioSourceFX.mute = true;
			PlayerPrefs.SetInt ("FX Options", 0);
		}
		else 
		{
			AudioManager.audioInstance.audioSourceFX.mute = false;
			PlayerPrefs.SetInt ("FX Options", 1);
		}
		return soundFXOptions;
	}


// ******** BUTTON PRESSING SECTION ******** \\

	// Player selects either the Play, Options, or Credits button on the Main Menu
	public void buttonSelected(Button selectedButton, string nameOfButton) 
	{
		if (animStarted) // If there is already a button selected
			return; // Do not do anything
		animStarted = true; // Button has been selected
		GameObject other1, other2; // Temp GameObjects used to remove the other remaining objects
		selectedButton.interactable = false; // The button that was pressed is no longer interactable

		// CHECKS FOR BUTTON PRESSED ON MAIN MENU

		if (nameOfButton.Equals("Play Button")) // If the play button was pressed
		{
			other1 = GameObject.FindGameObjectWithTag ("Credits Button"); // Other button was the credits button
			other2 = GameObject.FindGameObjectWithTag ("Options Button"); // Other button was the options button
			buttonsToSpawn = new GameObject[4]; // Going to spawn four objects
			buttonsToSpawn[0] = vsComputer as GameObject; // The VS Computer Button
			buttonsToSpawn[1] = vsPlayer as GameObject; // The VS Player Button
			buttonsToSpawn[2] = tutorial as GameObject; // The Tutorial Button
			buttonsToSpawn[3] = backButton as GameObject; // A Back Button
		}
		else if (nameOfButton.Equals("Options Button")) // If the Options Button was pressed
		{
			other1 = GameObject.FindGameObjectWithTag ("Credits Button"); // Other button was the credits button
			other2 = GameObject.FindGameObjectWithTag ("Play Button"); // Other button was the play button
			buttonsToSpawn = new GameObject[4]; // Going to spawn four objects
			buttonsToSpawn[0] = optionsText as GameObject; // The Option's text
			buttonsToSpawn[1] = musicButton as GameObject; // The Music Enable/Disable button
			buttonsToSpawn[2] = soundFxButton as GameObject; // The Sound Effects Enable/Disable button
			buttonsToSpawn[3] = backButton as GameObject; // A Back Button
		}
		else
		{
			other1 = GameObject.FindGameObjectWithTag ("Play Button"); // Other button was the play button
			other2 = GameObject.FindGameObjectWithTag ("Options Button"); // Other button was the credits button
			buttonsToSpawn = new GameObject[2]; // Going to spawn two objects
			buttonsToSpawn [0] = creditsText; // The Credit's text
			buttonsToSpawn [1] = backButton; // A Back Button
		}
		StartCoroutine (KillButton(selectedButton, other1, other2, nameOfButton)); // Calls the Kill Button Coroutine which transitions to the next menu

	} 
	// End of Main Menu Buttons


	// Player has selected either the Confirm Name, VS Computer, VS Player, Host, Join, Tutorial, or one of many back buttons
	public void buttonSelectedPlay(Button selectedButton, string nameOfButton) 
	{
		if (animStarted)  // If there is already a button selected
			return; // Don't do anything
		animStarted = true; // Animation has started
		GameObject other1 = null, other2 = null, other3 = null; // Possibility of three other objects on screen that need removal
		selectedButton.interactable = false; // The selected button is no longer interactable

		// CHECKS FOR BUTTON PRESSED


		// Play Menu
		if (nameOfButton.Equals("VS Computer Button")) // VS Computer Button was pressed
		{
			other1 = GameObject.FindGameObjectWithTag ("Back Button"); // Other button was the Back button
			other2 = GameObject.FindGameObjectWithTag ("VS Player Button"); // Other button was the VS Player button
			other3 = GameObject.FindGameObjectWithTag ("Tutorial Button"); // Other button was the Tutorial button
		}
		else if (nameOfButton.Equals("VS Player Button")) // VS Player Button was pressed
		{
			other1 = GameObject.FindGameObjectWithTag ("VS AI Button"); // Other button was the VS AI button
			other2 = GameObject.FindGameObjectWithTag ("Back Button"); // Other button was the Back button
			other3 = GameObject.FindGameObjectWithTag ("Tutorial Button"); // Other button was the Tutorial button
			buttonsToSpawn = new GameObject[3]; // Going to spawn three objects
			buttonsToSpawn [0] = host; // The Host button
			buttonsToSpawn [1] = join; // The Join button
			buttonsToSpawn [2] = backButton; // The back button
			StartCoroutine (KillButton (selectedButton, other1, other2, other3, nameOfButton)); // Begin the menu transition with remaining buttons
			return;
		}
		else if (nameOfButton.Equals("Tutorial Button")) // If the Tutorial Button was pressed
		{
			other1 = GameObject.FindGameObjectWithTag ("VS AI Button"); // Other button was the VS AI button
			other2 = GameObject.FindGameObjectWithTag ("VS Player Button"); // Other button was the VS Player button
			other3 = GameObject.FindGameObjectWithTag ("Back Button"); // Other button was the Back button
		}
		else if (nameOfButton.Equals("Back Button") && menuTracker.Equals("Play Menu")) // If the back button on the Play menu was selected
		{
			other1 = GameObject.FindGameObjectWithTag ("VS AI Button"); // Other button was the VS AI button
			other2 = GameObject.FindGameObjectWithTag ("VS Player Button"); // Other button was the VS Player button
			other3 = GameObject.FindGameObjectWithTag ("Tutorial Button"); // Other button was the Tutorial button
			buttonsToSpawn = new GameObject[3]; // Going to spawn three objects
			buttonsToSpawn[0] = play as GameObject; // The Play Button
			buttonsToSpawn[1] = options as GameObject; // The Options Button
			buttonsToSpawn[2] = credits as GameObject; // The Credits Button
			StartCoroutine (KillButton (selectedButton, other1, other2, other3, nameOfButton)); // Begin the menu transition with remaining buttons
			return;
		}
		// End of Play Menu


		// Options Menu
		else if (nameOfButton.Equals("Back Button") && menuTracker.Equals("Options Menu")) // If the back button in the option's menu is selected
		{
			other1 = GameObject.Find ("Options Text(Clone)"); // Other object was the Options Text
			other2 = GameObject.Find ("Music Button(Clone)"); // Other button was the Music button
			other3 = GameObject.Find ("Sound FX Button(Clone)"); // Other button was the Sound FX button
			buttonsToSpawn = new GameObject[3]; // Going to spawn three objects
			buttonsToSpawn [0] = play as GameObject; // The Play Button
			buttonsToSpawn [1] = options as GameObject; // The Options Button
			buttonsToSpawn [2] = credits as GameObject; // The Credits Button
			StartCoroutine (KillButton (selectedButton, other1, other2, other3, nameOfButton)); // Begin the menu transition with remaining buttons
			return;
		}
		// End of Options Menu


		// Credits Menu
		else if (nameOfButton.Equals("Back Button") && menuTracker.Equals("Credits Menu")) // If the back button is selected on the Credits Menu
		{
			other1 = GameObject.FindGameObjectWithTag ("Credits Text"); // Other object was the Credit's Text
			buttonsToSpawn = new GameObject[3]; // Going to spawn three objects
			buttonsToSpawn [0] = play as GameObject; // The Play Button
			buttonsToSpawn [1] = options as GameObject; // The Options Button
			buttonsToSpawn [2] = credits as GameObject; // The Credits Button
			StartCoroutine (KillButton (selectedButton, other1, nameOfButton)); // Begin the menu transition with remaining buttons
			return;
		}
		// End of Credits Menu


		// Name Selection Menu
		else if (nameOfButton.Equals("Confirm Name")) // If name is confirmed for multiplayer
		{
			PlayerPrefs.SetString ("Name", GameObject.Find("enterName(Clone)").GetComponent<InputField> ().textComponent.text); // Sets the player's name to what they've entered
			if (PlayerPrefs.GetString ("Name") == null || PlayerPrefs.GetString ("Name") == "") // If the player's name is null or blank
				return; // Stop them from continuing
			PlayerPrefs.SetString ("Opponent Name", ""); // Sets the opponent's name to blank
			SceneManagement.Instance.SwitchScene ("Lobby"); // Go to the lobby scene
			return;
		}
		else if (nameOfButton.Equals("Back Button") && (menuTracker.Equals("VS Player Join Menu") || (menuTracker.Equals("VS Player Host Menu")))) // If the back button is pressed on the Name selection menu
		{
			other1 = GameObject.Find ("enterName(Clone)"); // Other button was the Enter Name Input Field
			other2 = GameObject.Find ("Confirm Name(Clone)"); // Other button was the Confirm Name button
			buttonsToSpawn = new GameObject[3]; // Going to spawn three objects
			buttonsToSpawn [0] = host; // The Host Button
			buttonsToSpawn [1] = join; // The Join Button
			buttonsToSpawn [2] = backButton; // The Back Button
			StartCoroutine (KillButton (selectedButton, other1, other2, nameOfButton)); // Begin the menu transition with remaining buttons
			return;
		}
		// End of Name Selection Menu


		// VS Player Menu
		else if (nameOfButton.Equals("Host Button")) // If the Host button is selected
		{
			PlayerPrefs.SetString ("Multiplayer Role", "Host Local"); // Set the player's role to Host Local [MUST BE CHANGED ONCE IMPLEMENTING WIFI]
			other1 = GameObject.Find ("Back Button(Clone)"); // Other button was the Back button
			other2 = GameObject.Find("Join Button(Clone)"); // Other button was the Join button
			buttonsToSpawn = new GameObject[3]; // Going to spawn three objects
			buttonsToSpawn [0] = enterName; // The Enter Name Input Field
			buttonsToSpawn [1] = confirmName; // The Confirm Name Button
			buttonsToSpawn [2] = backButton; // The Back Button
			StartCoroutine (KillButton (selectedButton, other1, other2, nameOfButton)); // Begin the menu transition with remaining buttons
			return;
		}
		else if (nameOfButton.Equals("Join Button")) // If the Join button is selected
		{
			PlayerPrefs.SetString ("Multiplayer Role", "Guest Local"); // Set the player's role to Guest Local [MUST BE CHANGED ONCE IMPLEMENTING WIFI]
			other1 = GameObject.Find ("Back Button(Clone)"); // Other button was the Back button
			other2 = GameObject.Find("Host Button(Clone)"); // Other button was the Host button
			buttonsToSpawn = new GameObject[3]; // Going to spawn three objects
			buttonsToSpawn [0] = enterName; // The Enter Name Input Field
			buttonsToSpawn [1] = confirmName; // The Confirm Name Button
			buttonsToSpawn [2] = backButton; // The Back Button
			StartCoroutine (KillButton (selectedButton, other1, other2, nameOfButton)); // Begin the menu transition with remaining buttons
			return;
		}
		else if (nameOfButton.Equals("Back Button") && menuTracker.Equals("VS Player Menu")) // If the back button on the VS Player Menu is selected
		{
			other1 = GameObject.Find ("Host Button(Clone)"); // Other button was the Host button
			other2 = GameObject.Find ("Join Button(Clone)"); // Other button was the Join button
			buttonsToSpawn = new GameObject[4]; // Going to spawn four objects
			buttonsToSpawn[0] = vsComputer as GameObject; // The VS AI Button
			buttonsToSpawn[1] = vsPlayer as GameObject; // The VS Player Button
			buttonsToSpawn[2] = tutorial as GameObject; // The Tutorial Button
			buttonsToSpawn[3] = backButton as GameObject; // The Back Button
			StartCoroutine (KillButton (selectedButton, other1, other2, nameOfButton)); // Begin the menu transition with remaining buttons
			return;
		}
		// End of VS Player Menu


		if (other1 == null || other2 == null || other3 == null) // To prevent an error, If not all three other objects were given an object
			return; // Leave the method
		StartCoroutine (KillButton (selectedButton, other1, other2, other3, nameOfButton)); // Begin the menu transition with remaining buttons selected
	}
	// End of other buttons


// ******** COROUTINE SECTION ******** \\


	// Coroutine that transitions two other objects
	private IEnumerator KillButton(Button selectedButton, GameObject other1, GameObject other2, string nameOfButton)
	{
		selectedButton.animator.Play ("ButtonAnim"); // Plays animation for button that was pressed
		if (other1.name.Equals("enterName(Clone)")) // If the first object that must be transitioned out is the Enter Name Input Field
			other1.GetComponent<Animator> ().Play ("AnimateIt"); // Use the AnimateIt animation instead of Swirl Away
		else
			other1.GetComponent<Animator> ().Play ("SwirlAway"); // If anything else, use Swirl Away
		other2.GetComponent<Animator> ().Play ("SwirlAway"); // Play the Swirl Away Animation
		yield return new WaitForSeconds (1.0f); // Wait a second.
		Button buttonThatWasPressed = selectedButton; // Retain the information on the button that was pressed
		DestroyObject (selectedButton.gameObject); // Destroy the button that was pressed
		DestroyObject (other1); // Destroy the first other object
		DestroyObject (other2); // Destroy the second other object
		animStarted = false; // No longer in an animation
		SpawnButtons (buttonThatWasPressed, nameOfButton); // Spawn the buttons for the next menu
	}

	// Coroutine that transitions one other object
	private IEnumerator KillButton(Button selectedButton, GameObject other1, string nameOfButton)
	{
		selectedButton.animator.Play ("ButtonAnim"); // Plays animation for button that was pressed
		if (other1.tag.Equals("Credits Text")) // If the first object that must be transitioned out is the Credits Text
			other1.GetComponent<Animator> ().Play ("SwirlAwayText"); // Use the SwirlAwayText animation instead of Swirl Away
		else
			other1.GetComponent<Animator> ().Play ("SwirlAway"); // If anything else, use Swirl Away
		yield return new WaitForSeconds (1.0f); // Wait a second.
		Button buttonThatWasPressed = selectedButton; // Retain the information on the button that was pressed
		DestroyObject (selectedButton.gameObject); // Destroy the button that was pressed
		DestroyObject (other1); // Destroy the other object
		animStarted = false; // No longer in an animation
		SpawnButtons (buttonThatWasPressed, nameOfButton); // Spawn the buttons for the next menu
	}

	// Coroutine that transitions two other objects
	private IEnumerator KillButton(Button selectedButton, GameObject other1, GameObject other2, GameObject other3, string nameOfButton)
	{
		selectedButton.animator.Play ("ButtonAnim"); // Plays animation for button that was pressed
		if (other1.name.Equals("Options Text(Clone)")) // If the first object that must be transitioned out is the Options Text
			other1.GetComponent<Animator> ().Play ("SwirlAwayText"); // Use the SwirlAwayText animation instead of Swirl Away
		else
			other1.GetComponent<Animator> ().Play ("SwirlAway"); // If anything else, use Swirl Away
		other2.GetComponent<Animator> ().Play ("SwirlAway"); // Play the Swirl Away Animation
		other3.GetComponent<Animator> ().Play ("SwirlAway"); // Play the Swirl Away Animation
		yield return new WaitForSeconds (1.0f); // Wait a second.
		Button buttonThatWasPressed = selectedButton; // Retain the information on the button that was pressed
		DestroyObject (selectedButton.gameObject); // Destroy the button that was pressed
		DestroyObject (other1); // Destroy the first other object
		DestroyObject (other2); // Destroy the second other object
		DestroyObject (other3); // Destroy the third other object
		animStarted = false; // No longer in an animation
		SpawnButtons (buttonThatWasPressed, nameOfButton); // Spawn the buttons for the next menu
	}

	// Method that spawns the objects for the next menu
	private void SpawnButtons(Button selectedButton, string nameOfButton)
	{
		spawnedButtons = buttonsToSpawn; // Spawned buttons equals buttonsToSpawn;

		// Main Menu Buttons
		if (nameOfButton.Equals ("Play Button")) // If player pressed Play
		{
			spawnedButtons [0] = (Instantiate (spawnedButtons [0])) as GameObject;
			spawnedButtons [0].transform.SetParent (canvas.transform);
			spawnedButtons [0].transform.localPosition = new Vector3 (-250.0f, -134.0f, 0.0f);
			spawnedButtons [1] = (Instantiate (spawnedButtons [1])) as GameObject;
			spawnedButtons [1].transform.SetParent (canvas.transform);
			spawnedButtons [1].transform.localPosition = new Vector3 (250.0f, -134.0f, 0.0f);
			spawnedButtons [2] = (Instantiate (spawnedButtons [2])) as GameObject;
			spawnedButtons [2].transform.SetParent (canvas.transform);
			spawnedButtons [2].transform.localPosition = new Vector3 (0.0f, -134.0f, 0.0f);
			spawnedButtons [3] = (Instantiate (spawnedButtons [3])) as GameObject;
			spawnedButtons [3].transform.SetParent (canvas.transform);
			spawnedButtons [3].transform.localPosition = new Vector3 (362.0f, 220.0f, 0.0f);
			menuTracker = "Play Menu";
		}
		else if (nameOfButton.Equals("Options Button")) // If player pressed Options
		{
			spawnedButtons[0] = (Instantiate (spawnedButtons [0])) as GameObject;
			spawnedButtons [0].transform.SetParent (canvas.transform);
			spawnedButtons [0].transform.localPosition = new Vector3 (-136.0f, -125.0f, 0.0f);
			spawnedButtons [0].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			spawnedButtons [1] = (Instantiate (spawnedButtons [1])) as GameObject;
			spawnedButtons [1].transform.SetParent (canvas.transform);
			spawnedButtons [1].transform.localPosition = new Vector3 (81.0f, -103.0f, 0.0f);
			spawnedButtons [2] = (Instantiate (spawnedButtons [2])) as GameObject;
			spawnedButtons [2].transform.SetParent (canvas.transform);
			spawnedButtons [2].transform.localPosition = new Vector3 (76.0f, -158.0f, 0.0f);
			spawnedButtons [3] = (Instantiate (spawnedButtons [3])) as GameObject;
			spawnedButtons [3].transform.SetParent (canvas.transform);
			spawnedButtons [3].transform.localPosition = new Vector3 (362.0f, 220.0f, 0.0f);
			if (!musicOptions)
				spawnedButtons [1].GetComponentInChildren<Text> ().text = "Off";
			if (!soundFXOptions)
				spawnedButtons [2].GetComponentInChildren<Text> ().text = "Off";
			menuTracker = "Options Menu";
		}
		else if (nameOfButton.Equals("Credits Button")) // If player pressed Credits
		{
			spawnedButtons[0] = (Instantiate (spawnedButtons [0])) as GameObject;
			spawnedButtons [0].transform.SetParent (canvas.transform);
			spawnedButtons [0].transform.localPosition = new Vector3 (0.0f, -128.0f, 0.0f);
			spawnedButtons [0].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			spawnedButtons [1] = (Instantiate (spawnedButtons [1])) as GameObject;
			spawnedButtons [1].transform.SetParent (canvas.transform);
			spawnedButtons [1].transform.localPosition = new Vector3 (362.0f, 220.0f, 0.0f);
			menuTracker = "Credits Menu";
		}
		// End of Main Menu Buttons

		// Back Button on any menu directly after Main menu
		else if (nameOfButton.Equals ("Back Button") && (menuTracker.Equals("Play Menu") || menuTracker.Equals("Credits Menu") || menuTracker.Equals("Options Menu"))) 
		{
			spawnedButtons [0] = (Instantiate (spawnedButtons [0])) as GameObject;
			spawnedButtons [0].transform.SetParent (canvas.transform);
			spawnedButtons [0].transform.localPosition = new Vector3 (-200.0f, -134.0f, 0.0f);
			spawnedButtons [1] = (Instantiate (spawnedButtons [1])) as GameObject;
			spawnedButtons [1].transform.SetParent (canvas.transform);
			spawnedButtons [1].transform.localPosition = new Vector3 (200.0f, -134.0f, 0.0f);
			spawnedButtons [2] = (Instantiate (spawnedButtons [2])) as GameObject;
			spawnedButtons [2].transform.SetParent (canvas.transform);
			spawnedButtons [2].transform.localPosition = new Vector3 (362.0f, 220.0f, 0.0f);
			menuTracker = "Main Menu";
		}
		// End of Back Button after main menu


		// Play Menu Buttons
		else if (nameOfButton.Equals("VS Player Button"))
		{
			spawnedButtons [0] = (Instantiate (spawnedButtons [0])) as GameObject;
			spawnedButtons [0].transform.SetParent (canvas.transform);
			spawnedButtons [0].transform.localPosition = new Vector3 (-200.0f, -134.0f, 0.0f);
			spawnedButtons [1] = (Instantiate (spawnedButtons [1])) as GameObject;
			spawnedButtons [1].transform.SetParent (canvas.transform);
			spawnedButtons [1].transform.localPosition = new Vector3 (200.0f, -134.0f, 0.0f);
			spawnedButtons [2] = (Instantiate (spawnedButtons [2])) as GameObject;
			spawnedButtons [2].transform.SetParent (canvas.transform);
			spawnedButtons [2].transform.localPosition = new Vector3 (362.0f, 220.0f, 0.0f);
			menuTracker = "VS Player Menu";
		}
		// End of Play menu Buttons


		// VS AI Menu Buttons

		// End of VS AI Menu Buttons


		// VS Player Menu Buttons
		else if (nameOfButton.Equals("Back Button") && menuTracker.Equals("VS Player Menu"))
		{
			spawnedButtons [0] = (Instantiate (spawnedButtons [0])) as GameObject;
			spawnedButtons [0].transform.SetParent (canvas.transform);
			spawnedButtons [0].transform.localPosition = new Vector3 (-250.0f, -134.0f, 0.0f);
			spawnedButtons [1] = (Instantiate (spawnedButtons [1])) as GameObject;
			spawnedButtons [1].transform.SetParent (canvas.transform);
			spawnedButtons [1].transform.localPosition = new Vector3 (250.0f, -134.0f, 0.0f);
			spawnedButtons [2] = (Instantiate (spawnedButtons [2])) as GameObject;
			spawnedButtons [2].transform.SetParent (canvas.transform);
			spawnedButtons [2].transform.localPosition = new Vector3 (0.0f, -134.0f, 0.0f);
			spawnedButtons [3] = (Instantiate (spawnedButtons [3])) as GameObject;
			spawnedButtons [3].transform.SetParent (canvas.transform);
			spawnedButtons [3].transform.localPosition = new Vector3 (362.0f, 220.0f, 0.0f);
			menuTracker = "Play Menu";
		}
		else if (nameOfButton.Equals("Host Button"))
		{
			spawnedButtons [0] = (Instantiate (spawnedButtons [0])) as GameObject;
			spawnedButtons [0].transform.SetParent (canvas.transform);
			spawnedButtons [0].transform.localPosition = new Vector3 (-150.0f, -134.0f, 0.0f);
			spawnedButtons [0].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			spawnedButtons [1] = (Instantiate (spawnedButtons [1])) as GameObject;
			spawnedButtons [1].transform.SetParent (canvas.transform);
			spawnedButtons [1].transform.localPosition = new Vector3 (200.0f, -134.0f, 0.0f);
			spawnedButtons [2] = (Instantiate (spawnedButtons [2])) as GameObject;
			spawnedButtons [2].transform.SetParent (canvas.transform);
			spawnedButtons [2].transform.localPosition = new Vector3 (362.0f, 220.0f, 0.0f);
			menuTracker = "VS Player Host Menu";
		}
		else if (nameOfButton.Equals("Join Button"))
		{
			spawnedButtons [0] = (Instantiate (spawnedButtons [0])) as GameObject;
			spawnedButtons [0].transform.SetParent (canvas.transform);
			spawnedButtons [0].transform.localPosition = new Vector3 (-150.0f, -134.0f, 0.0f);
			spawnedButtons [0].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			spawnedButtons [1] = (Instantiate (spawnedButtons [1])) as GameObject;
			spawnedButtons [1].transform.SetParent (canvas.transform);
			spawnedButtons [1].transform.localPosition = new Vector3 (200.0f, -134.0f, 0.0f);
			spawnedButtons [2] = (Instantiate (spawnedButtons [2])) as GameObject;
			spawnedButtons [2].transform.SetParent (canvas.transform);
			spawnedButtons [2].transform.localPosition = new Vector3 (362.0f, 220.0f, 0.0f);
			menuTracker = "VS Player Join Menu";
		}
		// End of VS Player Menu Buttons

		// Back Button for Name Selection menu
		else if (nameOfButton.Equals("Back Button") && (menuTracker.Equals("VS Player Host Menu") || menuTracker.Equals("VS Player Join Menu")))
		{
			spawnedButtons [0] = (Instantiate (spawnedButtons [0])) as GameObject;
			spawnedButtons [0].transform.SetParent (canvas.transform);
			spawnedButtons [0].transform.localPosition = new Vector3 (-200.0f, -134.0f, 0.0f);
			spawnedButtons [1] = (Instantiate (spawnedButtons [1])) as GameObject;
			spawnedButtons [1].transform.SetParent (canvas.transform);
			spawnedButtons [1].transform.localPosition = new Vector3 (200.0f, -134.0f, 0.0f);
			spawnedButtons [2] = (Instantiate (spawnedButtons [2])) as GameObject;
			spawnedButtons [2].transform.SetParent (canvas.transform);
			spawnedButtons [2].transform.localPosition = new Vector3 (362.0f, 220.0f, 0.0f);
			menuTracker = "VS Player Menu";
		}
		// End of Back Button for Name Selection menu


	}
}
