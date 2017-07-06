using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;

public class OverrideNetworkDiscovery : NetworkDiscovery // Replaces Network Discovery
{
	public static OverrideNetworkDiscovery networkInstance;

	private int numOfServersListed; // Number of servers listed
//	public string gameName; // Name of game
	private string[] nameofServers; // Name of servers listed
	private string[] portOfServers; // IP addresses of servers listed


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
		
	//	GameObject.DontDestroyOnLoad (gameObject);
		nameofServers = new string[0];
		portOfServers = new string[0];
	//	this.broadcastData = "NetworkManager:localhost:" + NetworkManager.singleton.networkPort;
		}
	}
		



	// If the client receives a broadcast
	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		NetworkManager.singleton.networkAddress = fromAddress;
		numOfServersListed++;
		string[] temp1 = new string[numOfServersListed];
		string[] temp2 = new string[numOfServersListed];
		for (int x = 0; x < numOfServersListed - 1; x++)
		{
			temp1 = nameofServers;
			temp2 = portOfServers;
		}
		temp1 [numOfServersListed - 1] = data;
		temp2 [numOfServersListed - 1] = fromAddress;
		nameofServers = temp1;
		portOfServers = temp2;
	//	NetworkManager.singleton.StartClient ();
	}




}
