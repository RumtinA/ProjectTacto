using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;

public class LobbyScript : MonoBehaviour {

	private bool isHost, hostReady, guestReady;
	public Text hostName, guestName, lobbySearch, serverSetup;
	private string otherIP;
//	private int yourPort = 25000;
	private bool cannotMakeServer, nowHosting;
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
		nowHosting = false;
		cannotMakeServer = true;
		if (PlayerPrefs.GetString ("Multiplayer Role").Equals ("Host Local")) // If the player is the host locally
		{
			isHost = true; // They are the host
			HostLobbyLocal (); // Creates a lobby for the host
		}
		else if (PlayerPrefs.GetString ("Multiplayer Role").Equals ("Host Wifi")) // If the player is the host over Wifi
		{
			isHost = true; // They are the host
	//		HostLobbyWifi (); // Creates a lobby for the host
		}
		else if (PlayerPrefs.GetString ("Multiplayer Role").Equals ("Guest Wifi")) // If the player is the guest over Wifi
		{
			isHost = false; // They are not the host
	//		FindLobbyWifi  (); // Are looking for a lobby over wifi
		}
		else // If not the host and not on Wifi
		{
			isHost = false; // They are not the host
			FindLobbyLocal (); // Are looking for a local lobby
		}
	}
	

	void Update () 
	{
		
	}

	public void HostLobbyLocal() // Brings the host to the hosted lobby
	{
		
	//	Debug.Log (PlayerPrefs.GetString ("Opponent Name"));
		if (PlayerPrefs.GetString ("Opponent Name").Equals("")) // If the host has no opponent
		{
			serverSetup.gameObject.SetActive(true);
			StartCoroutine(SearchingForPort());
			StartCoroutine(FindOpenPort());
		}
		else // If they do have an opponent (they just finished a match), make it their name
			guestName.text = PlayerPrefs.GetString ("Opponent Name");
	}

/*	public void HostLobbyWifi() // Brings the host to the hosted lobby
	{
		hostName.gameObject.SetActive (true); // Enables names at the top
		guestName.gameObject.SetActive (true); // Enables names at the top
		hostName.text = PlayerPrefs.GetString ("Name"); // Set the host's name to their name
		//	Debug.Log (PlayerPrefs.GetString ("Opponent Name"));
		if (PlayerPrefs.GetString ("Opponent Name").Equals("")) // If the host has no opponent
		{
			//	Debug.Log ("No Opponent");
			StartCoroutine (SearchingForPlayer ()); // Make the searching for player text active
		}
		else // If they do have an opponent (they just finished a match), make it their name
			guestName.text = PlayerPrefs.GetString ("Opponent Name");
	} */

/*	public void FindLobbyWifi() // Default for guest who is searching for a lobby via Wifi
	{

	} */

	public void FindLobbyLocal() // Default for guest who is searching for a lobby via Local
	{
		lobbySearch.gameObject.SetActive (true);
		StartCoroutine (SearchingForLobbiesLocal ());
		GameObject.Find ("Network Manager").GetComponent<OverrideNetworkDiscovery> ().Initialize ();
		GameObject.Find ("Network Manager").GetComponent<OverrideNetworkDiscovery> ().StartAsClient ();
	}

	public void JoinLobbyLocal() // Creates the lobby for the guest who just joined a lobby
	{
		hostName.gameObject.SetActive (true); // Enables names at the top
		guestName.gameObject.SetActive (true); // Enables names at the top
		guestName.text = PlayerPrefs.GetString("Name"); // Set the Guest name to the player's name
		hostName.text = PlayerPrefs.GetString ("Opponent Name"); // Set the Host's name to the opponent name
	}

	public void JoinLobbyWifi() // Creates the lobby for the guest who just joined a lobby
	{
		hostName.gameObject.SetActive (true); // Enables names at the top
		guestName.gameObject.SetActive (true); // Enables names at the top
		guestName.text = PlayerPrefs.GetString("Name");
		hostName.text = PlayerPrefs.GetString ("Opponent Name");
	}

	public void LeaveLobbyLocal()
	{
		
	}

	public void LeaveHostedLobby()
	{
		SceneManagement.Instance.SwitchScene ("MainMenu");
	}

	private IEnumerator SearchingForPlayer() // Make the opponent text "searching for player..."
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

	private IEnumerator SearchingForLobbiesLocal() // Make the search text say "searching for lobbies..."
	{
		lobbySearch.text = "Searching For Lobby.";
		yield return new WaitForSeconds (0.5f);
		lobbySearch.text = "Searching For Lobby..";
		yield return new WaitForSeconds (0.5f);
		lobbySearch.text = "Searching For Lobby...";
		yield return new WaitForSeconds (0.5f);
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

	public void ReturnToLobbyWifi()
	{
		if (isHost) 
		{
	//		HostLobbyWifi ();
		} 
		else
		{
	//		JoinLobbyWifi ();
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
					break;
				}
			}
			NetworkManager.singleton.StopClient ();
		}

		if (cannotMakeServer)
		{
			Debug.Log ("Error with ports!");
			SceneManagement.Instance.SwitchScene ("MainMenu");
			yield break;
		}
		hostName.gameObject.SetActive (true); // Enables names at the top
		guestName.gameObject.SetActive (true); // Enables names at the top
		hostName.text = PlayerPrefs.GetString ("Name"); // Set the host's name to their name
		StartCoroutine (SearchingForPlayer ()); // Make the searching for player text active
		OverrideNetworkDiscovery.networkInstance.Initialize ();
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
