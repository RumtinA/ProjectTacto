﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp()
	{
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManagerPlay> ().ProceedToNextTurn ();
		DestroyObject (gameObject);
	}
}
