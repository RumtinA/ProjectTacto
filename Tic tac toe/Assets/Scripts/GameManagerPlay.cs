using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPlay : MonoBehaviour {

	public GameObject[] boardSpaces; // Each board space
	public Canvas r16_9; // Resolution 16:9
	private GameObject[] playerCode; // The three cards the player chose
	private GameObject[] opponentCode; // The three cards the opponent chose
	public GameObject[] prefabulousRed; // Red card prefabs
	public GameObject[] prefabulousBlue; // Blue card prefabs
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


	// Use this for initialization
	void Start () {
		
		PlayerPrefs.SetInt ("Player Color", 0);  // Testing Player is Red
		PlayerPrefs.SetString ("Player Top", "RedClaimCenter"); // Testing Card
		PlayerPrefs.SetString ("Player Mid", "RedMirrorStraight"); // Testing Card
		PlayerPrefs.SetString ("Player Low", "RedBlockDiagonalSide"); // Testing Card
		PlayerPrefs.SetString ("Opponent Top", "BlueClaimCenter"); // Testing Card
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
			player1Card1 = Instantiate (playerCode [0], new Vector3 (-75.0f, 32.0f, 108.0f), Quaternion.identity) as GameObject;
			player1Card1.transform.Rotate (0.0f, 0.0f, -90.0f);
			player1Card2 = Instantiate (playerCode [1], new Vector3 (-75.0f, 0.0f, 108.0f), Quaternion.identity) as GameObject;
			player1Card2.transform.Rotate (0.0f, 0.0f, -90.0f);
			player1Card3 = Instantiate (playerCode [2], new Vector3 (-75.0f, -32.0f, 108.0f), Quaternion.identity) as GameObject;
			player1Card3.transform.Rotate (0.0f, 0.0f, -90.0f);
			player2Card1 = Instantiate (opponentCode [0], new Vector3 (75.0f, 32.0f, 108.0f), Quaternion.identity) as GameObject;
			player2Card1.transform.Rotate (0.0f, 0.0f, -90.0f);
			player2Card2 = Instantiate (opponentCode [1], new Vector3 (75.0f, 0.0f, 108.0f), Quaternion.identity) as GameObject;
			player2Card2.transform.Rotate (0.0f, 0.0f, -90.0f);
			player2Card3 = Instantiate (opponentCode [2], new Vector3 (75.0f, -32.0f, 108.0f), Quaternion.identity) as GameObject;
			player2Card3.transform.Rotate (0.0f, 0.0f, -90.0f);
	}

	private void nextTurn()
	{
		isPlayerTurn = !isPlayerTurn;
		bool validSpace = false;
		string carduno = playerCode [0].name;
		if (isPlayerTurn && PlayerPrefs.GetInt ("Player Color") == 0 || !isPlayerTurn && PlayerPrefs.GetInt("Player Color") != 0) {
			carduno.Replace ("Red", "");
		} 
		else
		{
			carduno.Replace ("Blue", "");
		}
		bool[] availability = new bool[9];
		if (isPlayerTurn) {
			availability = player1Card1.GetComponent<Attribute> ().callCode (player1Card1, boardSpaces, PlayerPrefs.GetInt("Player Color"));
			for (int x = 0; x < 9; x++) {
				if (availability [x]) {
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
				}
			}
			if (validSpace) {
				SetPlayerPassed (false);
				return;
			} 
			availability = player1Card2.GetComponent<Attribute> ().callCode (player1Card2, boardSpaces , PlayerPrefs.GetInt("Player Color"));
			for (int x = 0; x < 9; x++) {
				if (availability [x]) {
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
				}
			}
			if (validSpace) {
				SetPlayerPassed (false);
				return;
			}
			availability = player1Card3.GetComponent<Attribute> ().callCode (player1Card3, boardSpaces, PlayerPrefs.GetInt("Player Color"));
			for (int x = 0; x < 9; x++) {
				if (availability [x]) {
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
				}
			}
			if (validSpace) 
			{
				SetPlayerPassed (false);
				return;
			} 
			else 
			{
				Debug.Log ("Player Has Passed!");
				SetPlayerPassed (true);
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
		else 
		{
			availability = player2Card1.GetComponent<Attribute> ().callCode (player2Card1, boardSpaces, PlayerPrefs.GetInt("Opponent Color"));
			for (int x = 0; x < 9; x++) 
			{
				if (availability [x]) 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
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
				if (availability [x]) 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
				}
			}
			if (validSpace) {
				SetPlayerPassed (false);
				return;
			}
			availability = player2Card3.GetComponent<Attribute> ().callCode (player2Card3, boardSpaces, PlayerPrefs.GetInt("Opponent Color"));
			for (int x = 0; x < 9; x++) 
			{
				if (availability [x]) 
				{
					boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
					validSpace = true;
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
				Debug.Log ("Opponent Passed!");
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
		for (int x = 0; x < 9; x++) 
		{
			if (!boardSpaces[x].GetComponent<Spaces>().GetIsTaken())
			{
				boardSpaces [x].GetComponent<Spaces> ().SetIsAvailable (true);
			}
		}
	}

	public bool GetIsPlayerTurn()
	{
		return isPlayerTurn;
	}

}
