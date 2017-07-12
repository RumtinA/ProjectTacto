using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MenuButton : MonoBehaviour {

	public string nameOfButton;
	public string joinButtonAttachment;
	public string joinButtonPortAttachment;
	private bool ready;

	void Start()
	{
		ready = false;
	}

    public void PlaySoundFX(string clip)
    {
        AudioManager.audioInstance.PlaySound(clip);
    }

	public void CallMenu(Button theButton)
	{
		GameObject.Find ("Menu Manager").GetComponent<MenuManager> ().buttonSelected (theButton, nameOfButton);
	}

	public void CallMenuPlay(Button theButton)
	{
		GameObject.Find ("Menu Manager").GetComponent<MenuManager> ().buttonSelectedPlay (theButton, nameOfButton);
	}

	public void CallMenuMuteMusc(Button thebutton)
	{
		bool t = GameObject.Find ("Menu Manager").GetComponent<MenuManager> ().SetMusicOptions (thebutton);
		if (!t)
			GameObject.Find("Music Button(Clone)").GetComponentInChildren<Text> ().text = "Off";
		else
			GameObject.Find("Music Button(Clone)").GetComponentInChildren<Text> ().text = "On";
	}

	public void CallMenuMuteFx(Button thebutton)
	{
		bool t = GameObject.Find ("Menu Manager").GetComponent<MenuManager> ().SetSoundFXOptions (thebutton);
		if (!t)
			gameObject.GetComponentInChildren<Text> ().text = "Off";
		else
			gameObject.GetComponentInChildren<Text> ().text = "On";
	}

	public void JoinGame()
	{
		NetworkManager.singleton.networkAddress = joinButtonAttachment;
		NetworkManager.singleton.networkPort = Int32.Parse(joinButtonPortAttachment);
	//	Int32.TryParse(joinButtonPortAttachment, out NetworkManager.singleton.networkPort);
		Debug.Log (NetworkManager.singleton.networkPort);
		NetworkManager.singleton.StartClient ();
	}

	public void SwitchColors()
	{
		if (PlayerPrefs.GetInt("Player Color") == 0)
			PlayerPrefs.SetInt("Player Color", 1);
		else
			PlayerPrefs.SetInt("Player Color", 0);
		GameObject.Find("Background").transform.Rotate(new Vector3(0,0,180));
		GameObject.Find ("Host").GetComponent<Player> ().RpcSwitchColors ();
	}

	public void LeaveLobbyGuest()
	{
		StartCoroutine (LeaveItGuest ());
	}

	private IEnumerator LeaveItGuest()
	{
		PlayerPrefs.SetString ("Opponent Name", "");
		GameObject.Find ("Guest").GetComponent<Player> ().CmdLeaveGame ();
		yield return new WaitForSeconds (2.0f);
		NetworkManager.singleton.StopClient ();
		OverrideNetworkDiscovery.networkInstance.StopBroadcast ();
		SceneManagement.Instance.SwitchScene ("MainMenu");
	}

	public void LeaveLobbyHost()
	{
		StartCoroutine (LeaveIt ());
	}

	private IEnumerator LeaveIt()
	{
		PlayerPrefs.SetString ("Opponent Name", "");
		GameObject.Find ("Host").GetComponent<Player> ().RpcLeaveGame ();
		yield return new WaitForSeconds (1.0f);
		OverrideNetworkDiscovery.networkInstance.StopBroadcast ();
		NetworkManager.singleton.StopServer ();
		SceneManagement.Instance.SwitchScene ("MainMenu");
	}

	public void ReturnToMenu()
	{
		if (NetworkManager.singleton.isNetworkActive)
		{
			if (LobbyScript.singleton.nowHosting)
			{
				NetworkManager.singleton.StopServer ();
				OverrideNetworkDiscovery.networkInstance.StopBroadcast ();
				SceneManagement.Instance.SwitchScene ("MainMenu");
				return;
			}
		}
		if (OverrideNetworkDiscovery.networkInstance.isServer || OverrideNetworkDiscovery.networkInstance.isClient)
			OverrideNetworkDiscovery.networkInstance.StopBroadcast ();
		SceneManagement.Instance.SwitchScene ("MainMenu");
	}

	public void ReadyToStart()
	{
		ready = !ready;
		if (LobbyScript.singleton.GetIsHost())
			LobbyScript.singleton.ReadyButtonTextSwitch (true, ready);
		else
			LobbyScript.singleton.ReadyButtonTextSwitch (false, ready);
		
	}
}
