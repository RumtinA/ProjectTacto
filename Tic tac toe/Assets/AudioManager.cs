using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager audioInstance;

	public string[] audioName;
	public AudioClip[] audioClipList;
	public bool clipFound;
	public AudioSource audioSource;
	
	void Start()
	{
		if (audioInstance != null) 
		{
			GameObject.Destroy (gameObject);
		} 
		else 
		{
			GameObject.DontDestroyOnLoad (gameObject);
			audioInstance = this;
		}
	}

	public void PlaySong(string songToPlay)
	{
		for (int x = 0; x < audioName.Length; x++) 
		{
			if (songToPlay == audioName [x]) 
			{
				audioSource.clip = audioClipList [x];
				audioSource.Play ();
				return;
			}
		}
	}
}
