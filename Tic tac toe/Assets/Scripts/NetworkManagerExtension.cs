using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManagerExtension : NetworkManager {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

/*	public override void OnServerAddPlayer(NetworkConnection n, short playerControllerID)
	{
		GameObject player = (GameObject)(Instantiate (playerPrefab, Vector3.zero, Quaternion.identity));
		player.GetComponent<Player> ().isHost = true;
		NetworkServer.AddPlayerForConnection (n, player, playerControllerID);
	} */
}
