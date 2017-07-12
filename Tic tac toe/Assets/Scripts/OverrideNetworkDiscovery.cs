using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Net;

public class OverrideNetworkDiscovery : NetworkDiscovery // Replaces Network Discovery
{
	public static OverrideNetworkDiscovery networkInstance;

	private int numOfServersListed; // Number of servers listed
	private int numOfServersShown;
//	public string gameName; // Name of game
	private string[] nameofServers; // Name of servers listed
	private string[] ipOfServers, portOfServers; // IP addresses of servers listed


	void Start()
	{
		if (networkInstance != null) 
		{
			GameObject.Destroy (gameObject);
		} 
		else 
		{
			GameObject.DontDestroyOnLoad (gameObject);
			networkInstance = this;
	//		networkInstance.broadcastData = PlayerPrefs.GetString ("Name");
	//		GameObject.DontDestroyOnLoad (gameObject);
			nameofServers = new string[0];
			ipOfServers = new string[0];
			portOfServers = new string[0];
	//		this.broadcastData = "NetworkManager:localhost:" + NetworkManager.singleton.networkPort;
		}
	}
		


	// If the client receives a broadcast
	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		string[] findNameAndPort = data.Split (null);
		string tempName = findNameAndPort [0];
		string tempPort = findNameAndPort [1];
	//	Debug.Log (tempName);
	//	Debug.Log (tempPort);
		for (int x = 0; x < numOfServersListed; x++)
		{
		//	Debug.Log ("Does " + data);
			if (nameofServers [x] == tempName && ipOfServers[x] == fromAddress && portOfServers[x] == tempPort) 
			{
			//	Debug.Log ("Apparently yes.");
				return;
			}
		}
		numOfServersListed++;
	//	NetworkManager.singleton.networkAddress = fromAddress;
		string[] temp1 = new string[numOfServersListed];
		string[] temp2 = new string[numOfServersListed];
		string[] temp3 = new string[numOfServersListed];
		for (int x = 0; x < numOfServersListed - 1; x++)
		{
			temp1[x] = nameofServers [x];
			temp2[x] = ipOfServers [x];
			temp3 [x] = portOfServers [x];
		}
		temp1 [numOfServersListed - 1] = tempName;
		temp2 [numOfServersListed - 1] = fromAddress;
		temp3 [numOfServersListed - 1] = tempPort;
		nameofServers = temp1;
		ipOfServers = temp2;
		portOfServers = temp3;
	//	Debug.Log (nameofServers);
		GameObject[] list = GameObject.FindGameObjectsWithTag ("Join");
		for (int x = 0; x < list.Length; x++)
		{
			Destroy (list [x]);
		}
		for (int x = 0; x < numOfServersListed; x++)
		{
			Text temp = Instantiate (GameObject.Find("Lobby Manager").GetComponent<LobbyScript>().GetServerNameText());
			temp.transform.SetParent (GameObject.Find ("Canvas").transform);
			temp.transform.localPosition = new Vector3 (-168.0f, (10.0f - (x * 40.0f)), 1.0f);
			temp.transform.localScale = new Vector3(1, 1, 1);
			temp.text = nameofServers [x] + ":";
	//		Debug.Log (nameofServers [x]);
			GameObject tempButton = (Instantiate (GameObject.Find("Lobby Manager").GetComponent<LobbyScript>().GetJoinButton())) as GameObject;
			tempButton.transform.SetParent (GameObject.Find ("Canvas").transform);
			tempButton.transform.localPosition = new Vector3 (60.0f, (10.0f - (x * 40.0f)), 1.0f);
			tempButton.transform.localScale = new Vector3(1, 1, 1);
			tempButton.GetComponent<MenuButton> ().joinButtonAttachment = ipOfServers[x];
			tempButton.GetComponent<MenuButton> ().joinButtonPortAttachment = portOfServers [x];
		}


		return;
	//	NetworkManager.singleton.StartClient ();
	}



}
