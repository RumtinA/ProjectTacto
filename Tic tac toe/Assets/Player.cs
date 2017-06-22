using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : MonoBehaviour {

	public string playerName = "";
	public bool isHost;
	public InputField nameInput;

	void Start()
	{
		nameInput = GetComponent<InputField> ();
		nameInput.interactable = true;
	}
}
