using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


namespace Prototype.NetworkLobby
{
public class PlayerInLobby : NetworkLobbyPlayer 
{
	public Button readyButton;
	public Button waitingPlayerButton;
	public Button removePlayerButton;
	public InputField nameInput;

	[SyncVar(hook = "OnMyName")]
	public string playerName = "";
	[SyncVar(hook = "OnMyColor")]
	public Color playerColor = Color.red;

	public override void OnClientEnterLobby()
	{

	}

	public override void OnStartAuthority()
	{

	}

	void SetupOtherPlayer()
	{

	}

	void SetupLocalPlayer()
	{

	}

	public override void OnClientExitLobby()
	{

	}

	public override void OnClientReady(bool readyState)
	{

	}

	public void OnMyName(string newName)
	{
		playerName = newName;
		nameInput.text = playerName;
	}

	public void OnMyColor(Color newColor)
	{
		playerColor = newColor;
	}

	public void OnReadyClicked()
	{
		SendReadyToBeginMessage ();
	}

	public void OnNameChanged(string str)
	{
		CmdNameChanged (str);
	}

	[Command]
	public void CmdNameChanged(string name)
	{
		playerName = name;
	}

	public void ToggleJoinButton(bool enabled)
	{

	}
}
}
