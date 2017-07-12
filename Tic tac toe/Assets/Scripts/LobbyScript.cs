using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;

public class LobbyScript : MonoBehaviour {

	public static LobbyScript singleton;
	public GameObject playerPrefab;
	private GameObject player;
	private bool isHost, hostReady, guestReady, clientReady, playerInCharge;
	public Text hostName, guestName, lobbySearch, serverSetup;
	private string otherIP;
	public Text serverName;
	public GameObject joinServerButton, startGameButton, switchColorButton, leaveLobbyGuestButton, returnToMenuButton, leaveLobbyHostButton, readyUpButton, hostReadyText, guestReadyText, readyUpText;
//	private int yourPort = 25000;
	public bool cannotMakeServer, nowHosting;
	private ConnectionTesterStatus 	er = ConnectionTesterStatus.Error, 
									uD = ConnectionTesterStatus.Undetermined, 
	//								lNPR = ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted, 
	//								lNS = ConnectionTesterStatus.LimitedNATPunchthroughSymmetric, 
	//								pC = ConnectionTesterStatus.PublicIPIsConnectable, 
	//								pNS = ConnectionTesterStatus.PublicIPNoServerStarted, 
	//								nRC = ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone, 
	//								nFC = ConnectionTesterStatus.NATpunchthroughFullCone,
									pB = ConnectionTesterStatus.PublicIPPortBlocked; 

	void Start () 
	{
		if (singleton != null)
			GameObject.Destroy (gameObject);
		else
		{
			GameObject.DontDestroyOnLoad (gameObject);
			singleton = this;
		}
		PlayerPrefs.SetString ("Opponent Name", ""); // Testing purposes
		startGameButton.SetActive(false);
		switchColorButton.SetActive (false);
		clientReady = false;
		guestReady = false;
		nowHosting = false;
		cannotMakeServer = true;
		returnToMenuButton.SetActive (true);
		if (PlayerPrefs.GetString ("Multiplayer Role").Equals ("Host Local")) // If the player is the host
		{
			isHost = true; // They are the host
			HostLobbyLocal (); // Creates a lobby for the host
			PlayerPrefs.SetInt("Player Color", 0);

		}
		else // If not the host
		{
			isHost = false; // They are not the host
			PlayerPrefs.SetInt("Player Color", 1);
			FindLobbyLocal (); // Are looking for a local lobby
			GameObject.Find("Background").transform.Rotate(new Vector3(0,0,180));
		}
	}
	
	public Text GetServerNameText()
	{
		return serverName;
	}

	public GameObject GetJoinButton()
	{
		return joinServerButton;
	}
		
	public void ReadyButtonTextSwitch(bool isThisHost, bool isReady)
	{
		if (isThisHost && isReady)
		{
			hostReady = true;
			hostReadyText.GetComponent<Text>().text = "Ready to Play!";
			readyUpButton.GetComponent<Image> ().color = new Color(0,1,0,1);
			readyUpText.GetComponent<Text>().text = "Ready!";
			GameObject.Find ("Host").GetComponent<Player> ().RpcReadyOrNot (isThisHost, isReady);
		}
		else if (isThisHost && !isReady)
		{
			hostReady = false;
			hostReadyText.GetComponent<Text>().text = "Not Ready";
			readyUpButton.GetComponent<Image> ().color = new Color(1,1,1,1);
			readyUpText.GetComponent<Text>().text = "Not Ready";
			GameObject.Find ("Host").GetComponent<Player> ().RpcReadyOrNot (isThisHost, isReady);
		}
		else if (!isThisHost && isReady)
		{
			guestReady = true;
			guestReadyText.GetComponent<Text>().text = "Ready to Play!";
			readyUpButton.GetComponent<Image> ().color = new Color(0,1,0,1);
			readyUpText.GetComponent<Text>().text = "Ready!";
			GameObject.Find ("Guest").GetComponent<Player> ().CmdReadyOrNot (isThisHost, isReady);
		}
		else
		{
			guestReady = false;
			guestReadyText.GetComponent<Text>().text = "Not Ready";
			readyUpButton.GetComponent<Image> ().color = new Color(1,1,1,1);
			readyUpText.GetComponent<Text>().text = "Not Ready";
			GameObject.Find ("Guest").GetComponent<Player> ().CmdReadyOrNot (isThisHost, isReady);
		}

		if (guestReady && hostReady && playerInCharge)
		{
			startGameButton.SetActive (true);
			switchColorButton.GetComponent<Button> ().interactable = false;
		}
		else if ((!guestReady || !hostReady) && playerInCharge)
		{
			if (!switchColorButton.GetComponent<Button> ().interactable)
			{
				switchColorButton.GetComponent<Button> ().interactable = true;
				startGameButton.SetActive (false);
			}
		}
	}

	public void OtherSideReadyButtonTextSwitch(bool isThisHost, bool isReady)
	{
		if (isThisHost && isReady)
		{
			hostReady = true;
			hostReadyText.GetComponent<Text>().text = "Ready to Play!";
		}
		else if (isThisHost && !isReady)
		{
			hostReady = false;
			hostReadyText.GetComponent<Text>().text = "Not Ready";
		}
		else if (!isThisHost && isReady)
		{
			guestReady = true;
			guestReadyText.GetComponent<Text>().text = "Ready to Play!";
		}
		else
		{
			guestReady = false;
			guestReadyText.GetComponent<Text>().text = "Not Ready";
		}

		if (guestReady && hostReady && playerInCharge)
		{
			startGameButton.SetActive (true);
			switchColorButton.GetComponent<Button> ().interactable = false;
		}
		else if ((!guestReady || !hostReady) && playerInCharge)
		{
			if (!switchColorButton.GetComponent<Button> ().interactable)
			{
				switchColorButton.GetComponent<Button> ().interactable = true;
				startGameButton.SetActive (false);
			}
		}
	}

	public void HostLobbyLocal() // Brings the host to the hosted lobby
	{
		
	//	Debug.Log (PlayerPrefs.GetString ("Opponent Name"));
		if (PlayerPrefs.GetString ("Opponent Name").Equals("")) // If the host has no opponent
		{
			serverSetup.gameObject.SetActive(true);
			StartCoroutine(SearchingForPort());
			StartCoroutine(FindOpenPort());
			playerInCharge = true;
		}
		else // If they do have an opponent (they just finished a match), make it their name
			guestName.text = PlayerPrefs.GetString ("Opponent Name");
	}

	public bool GetIsHost()
	{
		return isHost;
	}

	public void SetIsHost(bool b)
	{
		isHost = b;
	}
		
	// Guest who is searching for a lobby
	public void FindLobbyLocal()
	{
		if (PlayerPrefs.GetString ("Opponent Name").Equals (""))
		{
			playerInCharge = false;
			lobbySearch.gameObject.SetActive (true);
			StartCoroutine (SearchingForLobbiesLocal ());
			GameObject.Find ("Network Manager").GetComponent<OverrideNetworkDiscovery> ().Initialize ();
			GameObject.Find ("Network Manager").GetComponent<OverrideNetworkDiscovery> ().broadcastData = PlayerPrefs.GetString ("Name");
			GameObject.Find ("Network Manager").GetComponent<OverrideNetworkDiscovery> ().StartAsClient ();
		}
	}

	// Enables the start and switch buttons for the host
	public void StartButtonEnabled()
	{
		readyUpButton.SetActive (true);
		if (playerInCharge)
			switchColorButton.SetActive (true);
		hostReadyText.SetActive (true);
		guestReadyText.SetActive (true);
	}

	// Disables the start and switch buttons for the host
	public void StartButtonDisabled()
	{
		readyUpButton.SetActive (false);
		switchColorButton.SetActive (false);
		hostReadyText.SetActive (false);
		guestReadyText.SetActive (false);
		if (PlayerPrefs.GetInt ("Player Color") == 1) 
		{
			PlayerPrefs.SetInt ("Player Color", 0);
			GameObject.Find("Background").transform.Rotate(new Vector3(0,0,180));
		}
	}

	public void JoinLobbyLocal() // Creates the lobby for the guest who just joined a lobby
	{
		hostName.gameObject.SetActive (true); // Enables names at the top
		guestName.gameObject.SetActive (true); // Enables names at the top
		guestName.text = PlayerPrefs.GetString("Name"); // Set the Guest name to the player's name
		hostName.text = PlayerPrefs.GetString ("Opponent Name"); // Set the Host's name to the opponent name
	}
		

	public void LeaveLobbyLocal()
	{
		
	}

	public void LeaveHostedLobby()
	{
		SceneManagement.Instance.SwitchScene ("MainMenu");
	}


	public void RestartSearchingForPlayer()
	{
		StartCoroutine (SearchingForPlayer ());
	}

	public void RestartServer()
	{
		SceneManagement.Instance.SwitchScene ("ResetLobby");
	}

	// Make the opponent text "searching for player..."
	private IEnumerator SearchingForPlayer() 
	{
		guestName.text = "Searching For Player.";
		yield return new WaitForSeconds (0.5f);
		if (PlayerPrefs.GetString ("Opponent Name") != "") 
		{
			guestName.text = PlayerPrefs.GetString ("Opponent Name");
			yield break;
		}
		guestName.text = "Searching For Player..";
		yield return new WaitForSeconds (0.5f);
		if (PlayerPrefs.GetString ("Opponent Name") != "")
		{
			guestName.text = PlayerPrefs.GetString ("Opponent Name");
			yield break;
		}
		guestName.text = "Searching For Player...";
		yield return new WaitForSeconds (0.5f);
		if (PlayerPrefs.GetString ("Opponent Name") != "")
		{
			guestName.text = PlayerPrefs.GetString ("Opponent Name");
			yield break;
		}
		StartCoroutine (SearchingForPlayer ());
	}

	// Make the search text say "searching for lobbies..."
	private IEnumerator SearchingForLobbiesLocal() 
	{
		lobbySearch.text = "Searching For Lobby.";
		yield return new WaitForSeconds (0.5f);
		if (PlayerPrefs.GetString("Opponent Name") != "")
		{
			lobbySearch.gameObject.SetActive (false);
			JoinLobbyLocal ();
			yield break;
		}
		lobbySearch.text = "Searching For Lobby..";
		yield return new WaitForSeconds (0.5f);
		if (PlayerPrefs.GetString("Opponent Name") != "")
		{
			lobbySearch.gameObject.SetActive (false);
			JoinLobbyLocal ();
			yield break;
		}
		lobbySearch.text = "Searching For Lobby...";
		yield return new WaitForSeconds (0.5f);
		if (PlayerPrefs.GetString("Opponent Name") != "")
		{
			lobbySearch.gameObject.SetActive (false);
			JoinLobbyLocal ();
			yield break;
		}
		StartCoroutine (SearchingForLobbiesLocal ());
	}

	public void ReturnToLobbyLocal()
	{
		if (isHost) 
		{
			HostLobbyLocal ();
		} 
		else
		{
			JoinLobbyLocal ();
		}
	}

	private IEnumerator FindOpenPort()
	{
		NetworkManager.singleton.networkPort = 25000;
		Debug.Log("Testing Port 25000");
		NetworkManager.singleton.StartClient ();
		Network.TestConnection ();
		yield return new WaitForSeconds (4.0f);
		Debug.Log (Network.TestConnection ());
		if (Network.TestConnection () != er && Network.TestConnection () != uD && Network.TestConnection () != pB && !NetworkManager.singleton.IsClientConnected())
		{
			Debug.Log("Client is open!");
			NetworkManager.singleton.StopClient ();
			cannotMakeServer = false;
			NetworkManager.singleton.StartServer();
			nowHosting = true;
			player = Instantiate (playerPrefab);
			player.GetComponent<Player> ().isHost = true;
			player.GetComponent<Player> ().playerName = PlayerPrefs.GetString ("Name");
			player.name = "Host";
			NetworkServer.Spawn (player);
		}
		else
		{
			for (int x = 25001; x < 40000; x++) 
			{
				NetworkManager.singleton.StopClient ();
				Debug.Log("Testing Port " + x);
				NetworkManager.singleton.networkPort = x;
				NetworkManager.singleton.StartClient ();
				Network.TestConnection ();
				yield return new WaitForSeconds (4.0f);
				if (Network.TestConnection () != er && Network.TestConnection () != uD && Network.TestConnection () != pB && !NetworkManager.singleton.IsClientConnected()) 
				{
					Debug.Log("Client is open!");
					NetworkManager.singleton.StopClient ();
					NetworkManager.singleton.StartServer();
					cannotMakeServer = false;
					nowHosting = true;
					player = Instantiate (playerPrefab);
					player.GetComponent<Player> ().isHost = true;
					player.GetComponent<Player> ().playerName = PlayerPrefs.GetString ("Name");
					player.name = "Host"; 
					break;
				}
			}
		}

		if (cannotMakeServer)
		{
			NetworkManager.singleton.StopClient ();
			Debug.Log ("Error with ports!");
			SceneManagement.Instance.SwitchScene ("MainMenu");
			yield break;
		}
		hostName.gameObject.SetActive (true); // Enables names at the top
		guestName.gameObject.SetActive (true); // Enables names at the top
		hostName.text = PlayerPrefs.GetString ("Name"); // Set the host's name to their name
		StartCoroutine (SearchingForPlayer ()); // Make the searching for player text active
//		OverrideNetworkDiscovery.networkInstance.broadcastData = PlayerPrefs.GetString("Name");
		OverrideNetworkDiscovery.networkInstance.Initialize ();
		OverrideNetworkDiscovery.networkInstance.broadcastData = PlayerPrefs.GetString("Name") + " " + NetworkManager.singleton.networkPort;
		OverrideNetworkDiscovery.networkInstance.StartAsServer ();
	}


	private IEnumerator SearchingForPort()
	{
		serverSetup.text = "Searching For Port.";
		yield return new WaitForSeconds (0.5f);
		if (nowHosting) 
		{
			serverSetup.gameObject.SetActive(false);
			yield break;
		}
		serverSetup.text = "Searching For Port..";
		yield return new WaitForSeconds (0.5f);
		if (nowHosting)
		{
			serverSetup.gameObject.SetActive(false);
			yield break;
		}
		serverSetup.text = "Searching For Port...";
		yield return new WaitForSeconds (0.5f);
		if (nowHosting)
		{
			serverSetup.gameObject.SetActive(false);
			yield break;
		}
		StartCoroutine (SearchingForPort ());

	}
}
