using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPlay : MonoBehaviour {

	public GameObject[] boardSpaces; // Each board space
	private GameObject[] playerCode; // The three cards the player chose
	private GameObject[] opponentCode; // The three cards the opponent chose
	public GameObject[] prefabulousRed; // Red card prefabs
	public GameObject[] prefabulousBlue; // Blue card prefabs
	public GameObject background;
	private GameObject player1Card1;
	private GameObject player1Card2;
	private GameObject player1Card3;
	private GameObject player2Card1;
	private GameObject player2Card2;
	private GameObject player2Card3;
	private bool hasResolutionChanged;
	private bool isPlayerTurn;
//	private bool isOpponentTurn;
	private bool playerPassed;
	private bool opponentPassed;
//	private bool duringSuddenDeath;
	private float currentResolutionWidth;
	private float currentResolutionHeight;
	public GameObject redStartButton;
	public GameObject blueStartButton;
	private GameObject redButtonController;
	private GameObject blueButtonController;


	// Use this for initialization
	void Start () {
		hasResolutionChanged = false;
		currentResolutionWidth = Screen.width;
		currentResolutionWidth = Screen.height;
		if (PlayerPrefs.GetInt ("Player Color") == 0) 
		{
			PlayerPrefs.SetString ("Opponent Top", "RedClaimCenter"); // Testing Card
			PlayerPrefs.SetString ("Opponent Mid", "RedBlockDiagonalCenter"); // Testing Card
			PlayerPrefs.SetString ("Opponent Low", "RedBlockDiagonalSide"); // Testing Card
		} 
		else 
		{
			PlayerPrefs.SetString ("Opponent Top", "BlueMirrorCorner"); // Testing Card
			PlayerPrefs.SetString ("Opponent Mid", "BlueClaimCross"); // Testing Card
			PlayerPrefs.SetString ("Opponent Low", "BlueBlockRowCenter"); // Testing Card
		}
		playerCode = new GameObject[3];
		opponentCode = new GameObject[3];
		setCode ();
		revealCode ();
		if (PlayerPrefs.GetInt ("Player Color") == 0) // If the player is red then they go first and it's their turn
		{
			isPlayerTurn = false;
			PlayerPrefs.SetInt ("Opponent Color", 1);
		//	isOpponentTurn = true;
		} 
		else // If they are blue, then they do not go first
		{
			isPlayerTurn = true;
			PlayerPrefs.SetInt ("Opponent Color", 0);
		//	isOpponentTurn = false;
		}
		SpawnButton ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentResolutionWidth != Screen.width || currentResolutionHeight != Screen.height) 
		{
			changeResolution ();
		}
	}

	private void changeResolution()
	{
		if (!hasResolutionChanged) 
		{
			hasResolutionChanged = true;
			return;
		}
		currentResolutionWidth = Screen.width;
		currentResolutionHeight = Screen.height;
		if (currentResolutionWidth/currentResolutionHeight > 1.7f) // 16:9
		{
			background.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			boardSpaces [0].transform.position = new Vector3 (-3.97f, 3.92f, 2);
			boardSpaces [0].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [1].transform.position = new Vector3 (0.0f, 3.92f, 2);
			boardSpaces [1].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [2].transform.position = new Vector3 (3.97f, 3.92f, 2);
			boardSpaces [2].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [3].transform.position = new Vector3 (-3.97f, 0.0f, 2);
			boardSpaces [3].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [4].transform.position = new Vector3 (0.0f, 0.0f, 2);
			boardSpaces [4].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [5].transform.position = new Vector3 (3.97f, 0.0f, 2);
			boardSpaces [5].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [6].transform.position = new Vector3 (-3.97f, -3.92f, 2);
			boardSpaces [6].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [7].transform.position = new Vector3 (0.0f, -3.92f, 2);
			boardSpaces [7].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [8].transform.position = new Vector3 (3.97f,- 3.92f, 2);
			boardSpaces [8].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			player1Card1.transform.position = new Vector3 (-9.5f, 3.92f, 2);
			player1Card2.transform.position = new Vector3 (-9.5f, 0.0f, 2);
			player1Card3.transform.position = new Vector3 (-9.5f, -3.92f, 2);
			player2Card1.transform.position = new Vector3 (9.5f, 3.92f, 2);
			player2Card2.transform.position = new Vector3 (9.5f, 0.0f, 2);
			player2Card3.transform.position = new Vector3 (9.5f, -3.92f, 2);
			player1Card1.transform.localScale = new Vector3 (0.45f, 0.45f, 1.0f);
			player1Card2.transform.localScale = new Vector3 (0.45f, 0.45f, 1.0f);
			player1Card3.transform.localScale = new Vector3 (0.45f, 0.45f, 1.0f);
			player2Card1.transform.localScale = new Vector3 (0.45f, 0.45f, 1.0f);
			player2Card2.transform.localScale = new Vector3 (0.45f, 0.45f, 1.0f);
			player2Card3.transform.localScale = new Vector3 (0.45f, 0.45f, 1.0f);
		}
		else if (currentResolutionWidth/currentResolutionHeight > 1.52f) // 16:10
		{
			background.transform.localScale = new Vector3(0.89f, 0.98f, 1.0f);
			boardSpaces [0].transform.position = new Vector3 (-3.6f, 3.92f, 2);
			boardSpaces [0].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [1].transform.position = new Vector3 (0.0f, 3.92f, 2);
			boardSpaces [1].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [2].transform.position = new Vector3 (3.6f, 3.92f, 2);
			boardSpaces [2].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [3].transform.position = new Vector3 (-3.6f, 0.0f, 2);
			boardSpaces [3].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [4].transform.position = new Vector3 (0.0f, 0.0f, 2);
			boardSpaces [4].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [5].transform.position = new Vector3 (3.6f, 0.0f, 2);
			boardSpaces [5].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [6].transform.position = new Vector3 (-3.6f, -3.92f, 2);
			boardSpaces [6].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [7].transform.position = new Vector3 (0.0f, -3.92f, 2);
			boardSpaces [7].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			boardSpaces [8].transform.position = new Vector3 (3.6f,- 3.92f, 2);
			boardSpaces [8].transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			player1Card1.transform.position = new Vector3 (-8.5f, 3.92f, 2);
			player1Card2.transform.position = new Vector3 (-8.5f, 0.0f, 2);
			player1Card3.transform.position = new Vector3 (-8.5f, -3.92f, 2);
			player2Card1.transform.position = new Vector3 (8.5f, 3.92f, 2);
			player2Card2.transform.position = new Vector3 (8.5f, 0.0f, 2);
			player2Card3.transform.position = new Vector3 (8.5f, -3.92f, 2);
			player1Card1.transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			player1Card2.transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			player1Card3.transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			player2Card1.transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			player2Card2.transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
			player2Card3.transform.localScale = new Vector3 (0.4f, 0.4f, 1.0f);
		}
		else if (currentResolutionWidth/currentResolutionHeight > 1.49f)
		{
			background.transform.localScale = new Vector3(0.82f, 0.969f, 1.0f);
			boardSpaces [0].transform.position = new Vector3 (-3.3f, 3.92f, 2);
			boardSpaces [0].transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			boardSpaces [1].transform.position = new Vector3 (0.0f, 3.92f, 2);
			boardSpaces [1].transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			boardSpaces [2].transform.position = new Vector3 (3.3f, 3.92f, 2);
			boardSpaces [2].transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			boardSpaces [3].transform.position = new Vector3 (-3.3f, 0.0f, 2);
			boardSpaces [3].transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			boardSpaces [4].transform.position = new Vector3 (0.0f, 0.0f, 2);
			boardSpaces [4].transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			boardSpaces [5].transform.position = new Vector3 (3.3f, 0.0f, 2);
			boardSpaces [5].transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			boardSpaces [6].transform.position = new Vector3 (-3.3f, -3.92f, 2);
			boardSpaces [6].transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			boardSpaces [7].transform.position = new Vector3 (0.0f, -3.92f, 2);
			boardSpaces [7].transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			boardSpaces [8].transform.position = new Vector3 (3.3f,- 3.92f, 2);
			boardSpaces [8].transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			player1Card1.transform.position = new Vector3 (-8.0f, 3.92f, 2);
			player1Card2.transform.position = new Vector3 (-8.0f, 0.0f, 2);
			player1Card3.transform.position = new Vector3 (-8.0f, -3.92f, 2);
			player2Card1.transform.position = new Vector3 (8.0f, 3.92f, 2);
			player2Card2.transform.position = new Vector3 (8.0f, 0.0f, 2);
			player2Card3.transform.position = new Vector3 (8.0f, -3.92f, 2);
			player1Card1.transform.localScale = new Vector3 (0.375f, 0.375f, 1.0f);
			player1Card2.transform.localScale = new Vector3 (0.375f, 0.375f, 1.0f);
			player1Card3.transform.localScale = new Vector3 (0.375f, 0.375f, 1.0f);
			player2Card1.transform.localScale = new Vector3 (0.375f, 0.375f, 1.0f);
			player2Card2.transform.localScale = new Vector3 (0.375f, 0.375f, 1.0f);
			player2Card3.transform.localScale = new Vector3 (0.375f, 0.375f, 1.0f);
		}
		else if (currentResolutionWidth/currentResolutionHeight > 1.33f)
		{
			background.transform.localScale = new Vector3(0.728f, 0.98f, 1.0f);
			boardSpaces [0].transform.position = new Vector3 (-2.95f, 3.92f, 2);
			boardSpaces [0].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [1].transform.position = new Vector3 (0.0f, 3.92f, 2);
			boardSpaces [1].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [2].transform.position = new Vector3 (2.95f, 3.92f, 2);
			boardSpaces [2].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [3].transform.position = new Vector3 (-2.95f, 0.0f, 2);
			boardSpaces [3].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [4].transform.position = new Vector3 (0.0f, 0.0f, 2);
			boardSpaces [4].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [5].transform.position = new Vector3 (2.95f, 0.0f, 2);
			boardSpaces [5].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [6].transform.position = new Vector3 (-2.95f, -3.92f, 2);
			boardSpaces [6].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [7].transform.position = new Vector3 (0.0f, -3.92f, 2);
			boardSpaces [7].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [8].transform.position = new Vector3 (2.95f,- 3.92f, 2);
			boardSpaces [8].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			player1Card1.transform.position = new Vector3 (-7.0f, 3.92f, 2);
			player1Card2.transform.position = new Vector3 (-7.0f, 0.0f, 2);
			player1Card3.transform.position = new Vector3 (-7.0f, -3.92f, 2);
			player2Card1.transform.position = new Vector3 (7.0f, 3.92f, 2);
			player2Card2.transform.position = new Vector3 (7.0f, 0.0f, 2);
			player2Card3.transform.position = new Vector3 (7.0f, -3.92f, 2);
			player1Card1.transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			player1Card2.transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			player1Card3.transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			player2Card1.transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			player2Card2.transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
			player2Card3.transform.localScale = new Vector3 (0.35f, 0.35f, 1.0f);
		}
		else if (currentResolutionWidth/currentResolutionHeight > 1.24f)
		{
			background.transform.localScale = new Vector3(0.683f, 0.969f, 1.0f);
			boardSpaces [0].transform.position = new Vector3 (-2.75f, 3.92f, 2);
			boardSpaces [0].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [1].transform.position = new Vector3 (0.0f, 3.92f, 2);
			boardSpaces [1].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [2].transform.position = new Vector3 (2.75f, 3.92f, 2);
			boardSpaces [2].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [3].transform.position = new Vector3 (-2.75f, 0.0f, 2);
			boardSpaces [3].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [4].transform.position = new Vector3 (0.0f, 0.0f, 2);
			boardSpaces [4].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [5].transform.position = new Vector3 (2.75f, 0.0f, 2);
			boardSpaces [5].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [6].transform.position = new Vector3 (-2.75f, -3.92f, 2);
			boardSpaces [6].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [7].transform.position = new Vector3 (0.0f, -3.92f, 2);
			boardSpaces [7].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);
			boardSpaces [8].transform.position = new Vector3 (2.75f,- 3.92f, 2);
			boardSpaces [8].transform.localScale = new Vector3 (0.3f, 0.3f, 1.0f);

			player1Card1.transform.position = new Vector3 (-6.5f, 3.92f, 2);
			player1Card2.transform.position = new Vector3 (-6.5f, 0.0f, 2);
			player1Card3.transform.position = new Vector3 (-6.5f, -3.92f, 2);
			player2Card1.transform.position = new Vector3 (6.5f, 3.92f, 2);
			player2Card2.transform.position = new Vector3 (6.5f, 0.0f, 2);
			player2Card3.transform.position = new Vector3 (6.5f, -3.92f, 2);
			player1Card1.transform.localScale = new Vector3 (0.325f, 0.3f, 1.0f);
			player1Card2.transform.localScale = new Vector3 (0.325f, 0.3f, 1.0f);
			player1Card3.transform.localScale = new Vector3 (0.325f, 0.3f, 1.0f);
			player2Card1.transform.localScale = new Vector3 (0.325f, 0.3f, 1.0f);
			player2Card2.transform.localScale = new Vector3 (0.325f, 0.3f, 1.0f);
			player2Card3.transform.localScale = new Vector3 (0.325f, 0.3f, 1.0f);
		}
	}

	private void setCode() // Sets the player's code based on previous choices
	{
		GameObject[] finder;
		if (PlayerPrefs.GetInt ("Player Color") == 0) {
			finder = prefabulousRed;
			PlayerPrefs.SetString ("Players Color", "Red");
			PlayerPrefs.SetString ("Opponents Color", "Blue");
		} 
		else 
		{
			finder = prefabulousBlue;
			PlayerPrefs.SetString ("Players Color", "Blue");
			PlayerPrefs.SetString ("Opponents Color", "Red");
		}
		for (int x = 0; x < 15; x++) // Finds and sets the Top Element
		{
			if (PlayerPrefs.GetString ("Player Top") == finder [x].name) {
				playerCode [0] = finder [x];
				Debug.Log (finder [x].GetComponent<Cards> ().GetNameOfCard ());
			}
		}
		for (int x = 0; x < 15; x++) // Finds and sets the Top Element
		{
			if (PlayerPrefs.GetString ("Player Mid") == finder [x].name) {
				playerCode [1] = finder [x];
				Debug.Log (finder [x].GetComponent<Cards> ().GetNameOfCard ());
			}
		}
		for (int x = 0; x < 15; x++) // Finds and sets the Top Element
		{
			if (PlayerPrefs.GetString ("Player Low") == finder [x].name) {
				playerCode [2] = finder [x];
				Debug.Log (finder [x].GetComponent<Cards> ().GetNameOfCard ());
			}
		}

		if (PlayerPrefs.GetInt ("Player Color") == 0) {
			finder = prefabulousBlue;
		} 
		else 
		{
			finder = prefabulousRed;
		}
		for (int x = 0; x < 15; x++) // Finds and sets the Top Element
		{
			if (PlayerPrefs.GetString ("Opponent Top") == finder [x].name) {
				opponentCode [0] = finder [x];
			}
		}
		for (int x = 0; x < 15; x++) // Finds and sets the Top Element
		{
			if (PlayerPrefs.GetString ("Opponent Mid") == finder [x].name) {
				opponentCode [1] = finder [x];
			}
		}
		for (int x = 0; x < 15; x++) // Finds and sets the Top Element
		{
			if (PlayerPrefs.GetString ("Opponent Low") == finder [x].name) {
				opponentCode [2] = finder [x];
			}
		}
	}

	private void revealCode()
	{
			player1Card1 = Instantiate (playerCode [0], new Vector3 (-9.5f, 3.97f, 108.0f), Quaternion.identity) as GameObject;
			player1Card1.transform.Rotate (0.0f, 0.0f, -90.0f);
			player1Card2 = Instantiate (playerCode [1], new Vector3 (-9.5f, 0.0f, 108.0f), Quaternion.identity) as GameObject;
			player1Card2.transform.Rotate (0.0f, 0.0f, -90.0f);
			player1Card3 = Instantiate (playerCode [2], new Vector3 (-9.5f, -3.97f, 108.0f), Quaternion.identity) as GameObject;
			player1Card3.transform.Rotate (0.0f, 0.0f, -90.0f);
			player2Card1 = Instantiate (opponentCode [0], new Vector3 (9.5f, 3.97f, 108.0f), Quaternion.identity) as GameObject;
			player2Card1.transform.Rotate (0.0f, 0.0f, -90.0f);
			player2Card2 = Instantiate (opponentCode [1], new Vector3 (9.5f, 0.0f, 108.0f), Quaternion.identity) as GameObject;
			player2Card2.transform.Rotate (0.0f, 0.0f, -90.0f);
			player2Card3 = Instantiate (opponentCode [2], new Vector3 (9.5f, -3.97f, 108.0f), Quaternion.identity) as GameObject;
			player2Card3.transform.Rotate (0.0f, 0.0f, -90.0f);
	}

	private void SpawnButton()
	{
		player1Card1.GetComponent<Animator>().SetInteger("animation", 0);
		player1Card2.GetComponent<Animator>().SetInteger("animation", 0);
		player1Card3.GetComponent<Animator>().SetInteger("animation", 0);
		player2Card1.GetComponent<Animator>().SetInteger("animation", 0);
		player2Card2.GetComponent<Animator>().SetInteger("animation", 0);
		player2Card3.GetComponent<Animator>().SetInteger("animation", 0);
		if (isPlayerTurn == false) // The next turn will be the player's turn
		{
			if (PlayerPrefs.GetInt ("Player Color") == 0)
			{
				StartCoroutine (WaitThenSpawn ("red"));
			}
			else
			{
				StartCoroutine (WaitThenSpawn ("blue"));
			}
		}
		else // The next turn will be the opponent's turn
		{
			if (PlayerPrefs.GetInt ("Opponent Color") == 0)
			{
				StartCoroutine (WaitThenSpawn ("red"));
			}
			else
			{
				StartCoroutine (WaitThenSpawn ("blue"));
			}
		}
	}

	IEnumerator WaitThenSpawn(string n)
	{
		yield return new WaitForSeconds (0.5f);
		if (n == "red") 
		{
			redButtonController = (Instantiate (redStartButton, new Vector3 (0.0f, -6.0f, 10.0f), Quaternion.identity)) as GameObject;
			redButtonController.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
		} 
		else 
		{
			blueButtonController = (Instantiate (blueStartButton, new Vector3 (0.0f, -6.0f, 10.0f), Quaternion.identity)) as GameObject;
			blueButtonController.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
		}
	}
		

	public void ProceedToNextTurn()
	{
		StartCoroutine (nextTurn ());
	}

	IEnumerator nextTurn() // Activates the next turn
	{
		isPlayerTurn = !isPlayerTurn; // Switches turns
		bool validSpace = false; // No space is valid by default
		string carduno = playerCode [0].name; // Gets the string name
		if (isPlayerTurn && PlayerPrefs.GetInt ("Player Color") == 0 || !isPlayerTurn && PlayerPrefs.GetInt("Player Color") != 0) {
			carduno.Replace ("Red", "");  // If the player is active player is red, remove red from string
			Debug.Log("Red's turn!");
		} 
		else
		{
			carduno.Replace ("Blue", ""); // Else, remove blue from string
			Debug.Log("Blue's turn!");
		}
		bool[] availability = new bool[9]; // Checks to see if spaces are available
		if (isPlayerTurn) // If it is the player's turn
		{ 
			availability = player1Card1.GetComponent<Attribute> ().callCode (player1Card1, boardSpaces, PlayerPrefs.GetInt("Player Color")); // Availability is equal to the available spaces
			player1Card1.GetComponent<Animator>().SetInteger("animation", 1);
			yield return new WaitForSeconds (0.5f);
			for (int x = 0; x < 9; x++) {
				if (availability [x] && !boardSpaces[x].GetComponent<Spaces>().GetIsTaken()) 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true); // if it is available, then make it available
					validSpace = true; // If at least one is valid, then this is true
				} 
				else 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (false); // If it isn't open, then it isn't available
				}
			}
			if (validSpace) {
				SetPlayerPassed (false);
				player1Card1.GetComponent<Animator>().SetInteger("animation", 3);
				yield break;
			}
			player1Card1.GetComponent<Animator>().SetInteger("animation", 2);
			yield return new WaitForSeconds (0.5f);
			player1Card1.GetComponent<Animator>().SetInteger("animation", 0);
			availability = player1Card2.GetComponent<Attribute> ().callCode (player1Card2, boardSpaces , PlayerPrefs.GetInt("Player Color"));
			player1Card2.GetComponent<Animator>().SetInteger("animation", 1);
			yield return new WaitForSeconds (0.5f);
			for (int x = 0; x < 9; x++) {
				if (availability [x] && !boardSpaces[x].GetComponent<Spaces>().GetIsTaken()) 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
				}
				else 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (false); // If it isn't open, then it isn't available
				}
			}
			if (validSpace) {
				SetPlayerPassed (false);
				player1Card2.GetComponent<Animator>().SetInteger("animation", 3);
				yield break;
			}
			player1Card2.GetComponent<Animator>().SetInteger("animation", 2);
			yield return new WaitForSeconds (0.5f);
			player1Card2.GetComponent<Animator>().SetInteger("animation", 0);
			availability = player1Card3.GetComponent<Attribute> ().callCode (player1Card3, boardSpaces, PlayerPrefs.GetInt("Player Color"));
			player1Card3.GetComponent<Animator>().SetInteger("animation", 1);
			yield return new WaitForSeconds (0.5f);
			for (int x = 0; x < 9; x++) {
				if (availability [x] && !boardSpaces[x].GetComponent<Spaces>().GetIsTaken()) 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
				}
				else 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (false); // If it isn't open, then it isn't available
				}
			}
			if (validSpace) 
			{
				player1Card3.GetComponent<Animator>().SetInteger("animation", 3);
				SetPlayerPassed (false);
				yield break;
			} 
			else 
			{
				player1Card3.GetComponent<Animator>().SetInteger("animation", 2);
				yield return new WaitForSeconds (0.5f);
				player1Card3.GetComponent<Animator>().SetInteger("animation", 0);
			//	Debug.Log ("Player Has Passed!");
				SetPlayerPassed (true); // The player has passed this turn
				if (GetPlayerPassed () && GetOpponentPassed ()) // If both players have passed
				{
					SuddenDeath (); // The next player can go anywhere
					yield break; // Leave this method
				} 
				else // If only one player has passed
				{
					SpawnButton (); // Then go to the next turn
					yield break; // Leave this method
				}
			}
		} 
		else 
		{
			availability = player2Card1.GetComponent<Attribute> ().callCode (player2Card1, boardSpaces, PlayerPrefs.GetInt("Opponent Color"));
			player2Card1.GetComponent<Animator>().SetInteger("animation", 1);
			yield return new WaitForSeconds (0.5f);
			for (int x = 0; x < 9; x++) 
			{
				if (availability [x] && !boardSpaces[x].GetComponent<Spaces>().GetIsTaken()) 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
				}
				else 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (false); // If it isn't open, then it isn't available
				}
			}
			if (validSpace) 
			{
				player2Card1.GetComponent<Animator>().SetInteger("animation", 3);
				SetOpponentPassed (false);
				yield break;
			}
			player2Card1.GetComponent<Animator>().SetInteger("animation", 2);
			yield return new WaitForSeconds (0.5f);
			player2Card1.GetComponent<Animator>().SetInteger("animation", 0);
			availability = player2Card2.GetComponent<Attribute> ().callCode (player2Card2, boardSpaces, PlayerPrefs.GetInt("Opponent Color"));
			player2Card2.GetComponent<Animator>().SetInteger("animation", 1);
			yield return new WaitForSeconds (0.5f);
			for (int x = 0; x < 9; x++) 
			{
				if (availability [x] && !boardSpaces[x].GetComponent<Spaces>().GetIsTaken()) 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
				}
				else 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (false); // If it isn't open, then it isn't available
				}
			}
			if (validSpace) {
				player2Card2.GetComponent<Animator>().SetInteger("animation", 3);
				SetPlayerPassed (false);
				yield break;
			}
			player2Card2.GetComponent<Animator>().SetInteger("animation", 2);
			yield return new WaitForSeconds (0.5f);
			player2Card2.GetComponent<Animator>().SetInteger("animation", 0);
			availability = player2Card3.GetComponent<Attribute> ().callCode (player2Card3, boardSpaces, PlayerPrefs.GetInt("Opponent Color"));
			player2Card3.GetComponent<Animator>().SetInteger("animation", 1);
			yield return new WaitForSeconds (0.5f);
			for (int x = 0; x < 9; x++) 
			{
				if (availability [x] && !boardSpaces[x].GetComponent<Spaces>().GetIsTaken()) 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
				}
				else 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (false); // If it isn't open, then it isn't available
				}
			}
			if (validSpace) 
			{
				player2Card3.GetComponent<Animator>().SetInteger("animation", 3);
				SetOpponentPassed (false);
				yield break;
			} 
			else 
			{
				player2Card3.GetComponent<Animator>().SetInteger("animation", 2);
				yield return new WaitForSeconds (0.5f);
				player2Card3.GetComponent<Animator>().SetInteger("animation", 0);
				SetOpponentPassed (true);
			//	Debug.Log ("Opponent Passed!");
				if (GetPlayerPassed () && GetOpponentPassed ()) 
				{
					SuddenDeath ();
					yield break;
				} 
				else 
				{
					SpawnButton ();
					yield break;
				}
			}
		}
			
	}

	public void turnDone()
	{
		player1Card1.GetComponent<Animator>().SetInteger("animation", 0);
		player1Card2.GetComponent<Animator>().SetInteger("animation", 0);
		player1Card3.GetComponent<Animator>().SetInteger("animation", 0);
		SetPlayerPassed (false);
		SetOpponentPassed (false);
		for (int x = 0; x < 9; x++) {
			boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (false);
		}
		if (CheckForWin ()) 
		{
			if (GetIsPlayerTurn ()) {
				Debug.Log (PlayerPrefs.GetString ("Players Color") + " Player Wins!");
			} 
			else 
			{
				Debug.Log (PlayerPrefs.GetString ("Opponents Color") + " Player Wins!");
			}
				return;
		}
		SpawnButton ();
	}

	private bool CheckForWin()
	{
		int currentPlayerColor;
		if (GetIsPlayerTurn ()) 
		{
			currentPlayerColor = PlayerPrefs.GetInt ("Player Color");
		} 
		else 
		{	
			currentPlayerColor = PlayerPrefs.GetInt ("Opponent Color");
		}
		if (boardSpaces [0].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor)  // Checks for winning combinations based on upper left corner square
		{
			if (boardSpaces [1].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor && boardSpaces [2].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Top Row
			{ 
				return true;
			}
			if (boardSpaces [3].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor && boardSpaces [6].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Left Column
			{
				return true;
			}
			if (boardSpaces [4].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor && boardSpaces [8].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Diagonal Upper Left to Lower Right
			{
				return true;
			}
		}
		if (boardSpaces [1].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Checks for winning combinations based on the upper square
		{
			if (boardSpaces [4].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor && boardSpaces [7].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Middle Column
			{
				return true;
			}
		}
		if (boardSpaces [2].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Checks for winning combinations based on the upper right corner square
		{
			if (boardSpaces [4].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor && boardSpaces [6].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Diagonal Upper Right to Lower Left
			{
				return true;
			}
			if (boardSpaces [5].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor && boardSpaces [8].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Right Column
			{
				return true;
			}
		}
		if (boardSpaces [3].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Checks for winning combinations based on left square
		{
			if (boardSpaces [4].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor && boardSpaces [5].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Middle Row
			{
				return true;
			}
		}
		if (boardSpaces [6].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor)
		{
			if (boardSpaces [7].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor && boardSpaces [8].GetComponent<Spaces> ().GetWhoHasIt () == currentPlayerColor) // Bottom Row
			{
				return true;
			}
		}
		return false;
	}


	private void SetPlayerPassed(bool b)
	{
		playerPassed = b;
	}

	private void SetOpponentPassed(bool b)
	{
		opponentPassed = b;
	}

	private bool GetPlayerPassed()
	{
		return playerPassed;
	}

	private bool GetOpponentPassed()
	{
		return opponentPassed;
	}

	private void SuddenDeath()
	{
		isPlayerTurn = !isPlayerTurn;
		bool isValid = false;
//		duringSuddenDeath = true;
		for (int x = 0; x < 9; x++) 
		{
			if (!boardSpaces[x].GetComponent<Spaces>().GetIsTaken())
			{
				boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
				isValid = true;
			}
		}
		if (!isValid) 
		{
			Debug.Log ("Cat's game!");
		}
	}

	public bool GetIsPlayerTurn()
	{
		return isPlayerTurn;
	}
		
}
