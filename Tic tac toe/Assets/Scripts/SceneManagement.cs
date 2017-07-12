using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SceneManagement : MonoBehaviour {

	public static SceneManagement Instance;

	void Start()
	{
		if (Instance != null) 
		{
			GameObject.Destroy (gameObject);
		} 
		else 
		{
			GameObject.DontDestroyOnLoad (gameObject);
			Instance = this;
		}
	}

	public void SwitchScene(string sceneName)
	{
		if (sceneName.Equals("MainMenu"))
		{
			GameObject.Destroy(GameObject.Find("Network Manager"));
			GameObject.Destroy (GameObject.Find ("Lobby Manager"));
			SceneManager.LoadScene (sceneName);
			return;
		}
			
		else if (sceneName.Equals("MainMenuKicked"))
		{
			NetworkManager.singleton.StopClient ();
			OverrideNetworkDiscovery.networkInstance.StopBroadcast ();
			GameObject.Destroy(GameObject.Find("Network Manager"));
			GameObject.Destroy (GameObject.Find ("Lobby Manager"));
			PlayerPrefs.SetInt ("Kicked", 1);
			SceneManager.LoadScene ("MainMenu");
			return;
		}

		else if (sceneName.Equals("ResetLobby"))
		{
			GameObject.Destroy(GameObject.Find("Network Manager"));
			GameObject.Destroy (GameObject.Find ("Lobby Manager"));
			PlayerPrefs.SetInt ("Player Color", 0);
			SceneManager.LoadScene ("Lobby");
			return;
		}
			
		SceneManager.LoadScene (sceneName);
	}

	public string GetActiveSceneName()
	{
		return SceneManager.GetActiveScene ().name;
	}
}
