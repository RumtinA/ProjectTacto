using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	public string playerName = "";
	public bool isHost;

	void Start()
	{
		playerName = PlayerPrefs.GetString ("Name");
	}

	public override void OnStartServer()
	{
		isHost = true;
	}

	public override void OnStartClient()
	{
		
	}
}
