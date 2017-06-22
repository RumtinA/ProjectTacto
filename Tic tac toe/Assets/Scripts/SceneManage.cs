using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManage : MonoBehaviour {

	void Start()
	{
		
	}

	void Update()
	{
		
	}

	public void GameTime()
	{
		Application.LoadLevel ("Gameplay");
	}

	public void CardSelection(int p)
	{
		PlayerPrefs.SetInt ("Player Color", p);
		Application.LoadLevel ("Selection");
	}
}
