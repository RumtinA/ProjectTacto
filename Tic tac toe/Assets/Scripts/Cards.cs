using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour {

	public string nameOfCard; // The full name of the card
	public string type; // Whether the card is Claim, Block, Mirror, Complete
	public int color;  // 0 is red, 1 is blue
	private bool isStored; // Checks if the card is stored in any of the three slots
	private string nameInGame;

	void Start ()
	{
		nameInGame = gameObject.name;
		isStored = false;
	}
		
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool GetIsStored ()
	{
		return isStored;
	}

	public void SetIsStored (bool b) // Stores the card in the next available slot.
	{
		isStored = b;
		Debug.Log ("Storage has changed to: " + isStored.ToString() + "!");
	}
		
	public int GetColor()
	{
		return color;
	}

	public string GetNameOfCard()
	{
		return nameOfCard;
	}

}
