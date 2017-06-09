using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaces : MonoBehaviour {

    public string name; //Name of Space
    public string type; //Type of Space (Center, Corner, Cross)
    public int BoardPosition; //Position of space in relation to board, position numbers are 1-9
    private bool isTaken { get; set; } //Is position already occupied?
    private bool isAvailable { get; set; } //Is position available to select?

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isTaken == false)
        {
            isAvailable = true;
        }

        else
        {
            isTaken = true;
            isAvailable = false;
        }
	}
}
