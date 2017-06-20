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
	private bool isPlayerTurn;
//	private bool isOpponentTurn;
	private bool playerPassed;
	private bool opponentPassed;
	private bool duringSuddenDeath;
	private float currentResolutionWidth;
	private float currentResolutionHeight;


	// Use this for initialization
	void Start () {
		currentResolutionWidth = Screen.width;
		currentResolutionWidth = Screen.height;
	/*	PlayerPrefs.SetInt ("Player Color", 0);  // Testing Player is Red
		PlayerPrefs.SetString ("Player Top", "RedClaimCross"); // Testing Card
		PlayerPrefs.SetString ("Player Mid", "RedMirrorStraight"); // Testing Card
		PlayerPrefs.SetString ("Player Low", "RedBlockDiagonalSide"); // Testing Card */
		PlayerPrefs.SetString ("Opponent Top", "BlueClaimCorner"); // Testing Card
		PlayerPrefs.SetString ("Opponent Mid", "BlueMirrorStraight"); // Testing Card
		PlayerPrefs.SetString ("Opponent Low", "BlueBlockDiagonalSide"); // Testing Card 
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
		nextTurn ();
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
		} 
		else 
		{
			finder = prefabulousBlue;
		}
		for (int x = 0; x < 15; x++) // Finds and sets the Top Element
		{
			if (PlayerPrefs.GetString ("Player Top") == finder [x].name) {
				playerCode [0] = finder [x];
			}
		}
		for (int x = 0; x < 15; x++) // Finds and sets the Top Element
		{
			if (PlayerPrefs.GetString ("Player Mid") == finder [x].name) {
				playerCode [1] = finder [x];
			}
		}
		for (int x = 0; x < 15; x++) // Finds and sets the Top Element
		{
			if (PlayerPrefs.GetString ("Player Low") == finder [x].name) {
				playerCode [2] = finder [x];
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

	private void nextTurn() // Activates the next turn
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
				return;
			} 
			availability = player1Card2.GetComponent<Attribute> ().callCode (player1Card2, boardSpaces , PlayerPrefs.GetInt("Player Color"));
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
				return;
			}
			availability = player1Card3.GetComponent<Attribute> ().callCode (player1Card3, boardSpaces, PlayerPrefs.GetInt("Player Color"));
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
				SetPlayerPassed (false);
				return;
			} 
			else 
			{
			//	Debug.Log ("Player Has Passed!");
				SetPlayerPassed (true); // The player has passed this turn
				if (GetPlayerPassed () && GetOpponentPassed ()) // If both players have passed
				{
					SuddenDeath (); // The next player can go anywhere
					return; // Leave this method
				} 
				else // If only one player has passed
				{
					nextTurn (); // Then go to the next turn
					return; // Leave this method
				}
			}
		} 
		else 
		{
			availability = player2Card1.GetComponent<Attribute> ().callCode (player2Card1, boardSpaces, PlayerPrefs.GetInt("Opponent Color"));
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
				SetPlayerPassed (false);
				return;
			} 
			availability = player2Card2.GetComponent<Attribute> ().callCode (player2Card2, boardSpaces, PlayerPrefs.GetInt("Opponent Color"));
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
				SetPlayerPassed (false);
				return;
			}
			availability = player2Card3.GetComponent<Attribute> ().callCode (player2Card3, boardSpaces, PlayerPrefs.GetInt("Opponent Color"));
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
				SetOpponentPassed (false);
				return;
			} 
			else 
			{
				SetOpponentPassed (true);
			//	Debug.Log ("Opponent Passed!");
				if (GetPlayerPassed () && GetOpponentPassed ()) 
				{
					SuddenDeath ();
					return;
				} 
				else 
				{
					nextTurn ();
					return;
				}
			}
		}
			
	}

	public void turnDone()
	{
		SetPlayerPassed (false);
		SetOpponentPassed (false);
		for (int x = 0; x < 9; x++) {
			boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (false);
		}
		nextTurn();
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
		duringSuddenDeath = true;
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
