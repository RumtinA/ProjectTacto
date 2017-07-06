﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
			GameObject.Destroy(GameObject.Find("Network Manager"));
		SceneManager.LoadScene (sceneName);
	}

	public string GetActiveSceneName()
	{
		return SceneManager.GetActiveScene ().name;
	}
}
