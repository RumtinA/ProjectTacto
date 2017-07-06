using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ServerManagement : MonoBehaviour {

	public string ip;
	public int port;

	void Start()
	{
		ip = Network.player.ipAddress;
		port = 23466;
	}

	void OnGUI () 
	{
		//if the player is NOT connected
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			// this is temporary for input of the ip address
			// find out your ip address and assign it here during gameplay
			ip = GUI.TextField(new Rect(200, 100, 100, 25), ip);
			port = int.Parse (GUI.TextField (new Rect (200, 125, 100, 25), "" + port));

			if (GUI.Button(new Rect(100,100,100,25), "Start Client"))
			{
				Network.Connect (ip, port);
			}

			if (GUI.Button(new Rect(100,125,100,25), "Create Server"))
			{
				Network.InitializeServer(10, port, false);
			}
		}

		else
		{
			if (Network.peerType == NetworkPeerType.Client)
			{
				GUI.Label(new Rect(100,100,100,25), "Client");

				if (GUI.Button(new Rect(100,125,100,25), "Logout"))
				{
					Network.Disconnect(200);
				}
			}

			if (Network.peerType == NetworkPeerType.Server)
			{
				GUI.Label(new Rect(100,100,100,25), "Server");

				GUI.Label(new Rect(100,125,100,25), "Connections: " + Network.connections.Length);

				if (GUI.Button(new Rect(100,150,100,25), "Logout"))
				{
					Network.Disconnect(200);
				}
			}

		}
	}
	

	void Update () 
	{
		
	}
}
