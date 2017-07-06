using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockIn : MonoBehaviour {



	void Start()
	{
		
	}

	void OnMouseUp()
	{
		SceneManagement.Instance.SwitchScene ("Gameplay");
	}
}
