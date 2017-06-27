using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

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
		Application.LoadLevel ("SceneRumtin");
	}
}
