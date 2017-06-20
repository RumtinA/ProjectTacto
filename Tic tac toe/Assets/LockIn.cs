using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockIn : MonoBehaviour {

	public GameObject manager;

	void Start()
	{
		
	}

	void OnMouseUp()
	{
		manager.GetComponent<SceneManager>().GameTime ();
	}
}
