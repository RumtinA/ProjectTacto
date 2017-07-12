using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager audioInstance; // An instance of AudioManager that can be called anywhere

    public string[] audioName, audioFXName; // List of song names
    public AudioClip[] audioClipList; // List of Music Audio Clips
    public AudioClip[] soundFXList; // List of Sound FX Audio Clips
    public bool clipFound;
    public AudioSource audioSource;
    public AudioSource audioSourceFX;

    void Start()
    {
        if (audioInstance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            audioInstance = this;
        }

 
    }

    public void PlaySong(string songToPlay)
    {
        for (int x = 0; x < audioName.Length; x++)
        {
            if (songToPlay == audioName[x])
            {
                audioSource.clip = audioClipList[x];
                audioSource.Play();
                return;
            }
        }
    }

    public void PlaySound(string clip)
    {
        for (int x = 0; x <audioFXName.Length; x++)
        {
            if (clip == audioFXName[x])
            {
                audioSourceFX.clip = soundFXList[x];
                audioSourceFX.Play();
                return;
            }
        }
    }
}
