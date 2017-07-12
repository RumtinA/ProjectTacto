using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	public string playerName = ""; // The Player's Name
	public bool isHost = false; // Whether or not this player is the host
	public int playerColor;
//	public bool isTesting = false;

	void Start()
	{
		
	}

	public override void OnStartServer() // On server start
	{
		if (isHost)
			playerColor = 0;
		else
			playerColor = 1;
	}

	public override void OnStartClient() // Once a player has connected
	{
		
	}

	[ClientRpc]
	void RpcTryAgain()
	{
		CmdSendNameToServer (playerName);
	}

	[Command]
	void CmdSendNameToServer(string nameToSend) // Sends name information to host
	{
	//	Debug.Log ("WE DID IT.");
		PlayerPrefs.SetString ("Opponent Name", nameToSend);
		GameObject isItThere = GameObject.Find ("Player(Clone)");
		if (isItThere != null) 
		{
			GameObject.Find ("Player(Clone)").name = "Guest";
			LobbyScript.singleton.StartButtonEnabled ();
			LobbyScript.singleton.returnToMenuButton.SetActive (false);
			LobbyScript.singleton.leaveLobbyHostButton.SetActive (true);
			RpcSendNameToClient (PlayerPrefs.GetString ("Name"));
		} 
		else
			RpcTryAgain ();
	}

	[ClientRpc]
	void RpcSendNameToClient(string nameToSend)
	{
	//	Debug.Log ("Received Name!");
	//	LobbyScript.singleton.StartButtonEnabled ();
		PlayerPrefs.SetString ("Opponent Name", nameToSend);
		GameObject isItThere = GameObject.Find ("Player(Clone)");
		if (isItThere != null) 
		{
			GameObject.Find ("Player(Clone)").GetComponent<Player> ().isHost = true;
			GameObject.Find ("Player(Clone)").GetComponent<Player> ().playerName = nameToSend;
			GameObject.Find ("Player(Clone)").name = "Host";
			LobbyScript.singleton.StartButtonEnabled ();
		}
		else
		{
			CmdSendNameToServer (playerName);
		}
	}

	[Command]
	public void CmdLeaveGame() // If guest leave the game
	{
		LobbyScript.singleton.RestartServer ();
	}

	[ClientRpc]
	public void RpcFinishLeaving()
	{
		NetworkManager.singleton.StopClient ();
		OverrideNetworkDiscovery.networkInstance.StopBroadcast ();
		SceneManagement.Instance.SwitchScene ("MainMenu");
	}

	[ClientRpc]
	public void RpcLeaveGame() // If the host leaves the game
	{
		PlayerPrefs.SetString ("Opponent Name", "");
		SceneManagement.Instance.SwitchScene ("MainMenuKicked");
	}
		

	[ClientRpc]
	public void RpcSwitchColors() // If the host switches the colors
	{
		if (PlayerPrefs.GetInt("Player Color") == 0)
			PlayerPrefs.SetInt("Player Color", 1);
		else
			PlayerPrefs.SetInt("Player Color", 0);
		GameObject.Find("Background").transform.Rotate(new Vector3(0,0,180));
	}

	public override void OnStartLocalPlayer() // When the new player is made locally
	{
		if (!isHost) // If it is not the host
		{
			playerName = PlayerPrefs.GetString ("Name");
			gameObject.name = "Guest";
			CmdSendNameToServer (playerName);
			GameObject[] list = GameObject.FindGameObjectsWithTag ("Join");
			for (int x = 0; x < list.Length; x++)
			{
				Destroy (list [x]);
			}
			LobbyScript.singleton.returnToMenuButton.SetActive (false);
			LobbyScript.singleton.leaveLobbyGuestButton.SetActive (true);
		}
	}

	[ClientRpc]
	public void RpcReadyOrNot(bool isThisHost, bool isReady)
	{
		LobbyScript.singleton.OtherSideReadyButtonTextSwitch (isThisHost, isReady);
	}

	[Command]
	public void CmdReadyOrNot(bool isThisHost, bool isReady)
	{
		LobbyScript.singleton.OtherSideReadyButtonTextSwitch (isThisHost, isReady);
	}
}
