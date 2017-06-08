using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    private int[,] SpaceNumber; //

	// Use this for initialization
	void Start () {
        SpaceNumber = new int[3, 3];
        SpaceNumber [2,2] = 9;
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
