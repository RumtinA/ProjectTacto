using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	public string playerName = ""; // The Player's Name
	public bool isHost = false; // Whether or not this player is the host

	void Start()
	{
		
	}

	public override void OnStartServer() // On server start
	{
		isHost = true; // The player who starts the server is the host
	}

	public override void OnStartClient() // Once a player has connected
	{
		if (isHost) // The player is the host
		{
			gameObject.name = "Host Player";
			playerName = PlayerPrefs.GetString ("Name"); // Make their name their name
		}
		else // If the player is not the host
		{
			gameObject.name = "Guest Player";
			playerName = PlayerPrefs.GetString ("Opponent Name"); // Make their name the opponent's name
		}
	}
}
