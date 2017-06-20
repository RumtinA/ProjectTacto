using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramMat : MonoBehaviour {

	public int color;
	public string matName;
	private bool isTaken;
	private string nameOfOccupiedCard;

	// Use this for initialization
	void Start () {
		isTaken = false;
		nameOfOccupiedCard = "none";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetColor()
	{
		return color;
	}

	public void chooseOrRemoveCard(bool b, GameObject collidedCard)
	{
		SetIsTaken (b);
	}

	public void SetIsTaken(bool b)
	{
		isTaken = b;
	}

	public void SetOccupiedCard(string n)
	{
		nameOfOccupiedCard = n;
		Debug.Log (nameOfOccupiedCard);
	}

	public bool GetIsTaken()
	{
		return isTaken;
	}

	public string GetNameOfOccupiedCard()
	{
		return nameOfOccupiedCard;
	}
}
