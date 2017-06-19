using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramMat : MonoBehaviour {

	public int color;
	public string matName;
	private bool isTaken;
	private GameObject occupiedCard;

	// Use this for initialization
	void Start () {
		isTaken = false;
		occupiedCard = null;
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

	private void SetIsTaken(bool b)
	{
		isTaken = b;
	}

	private void SetOccupiedCard(GameObject col)
	{
		occupiedCard = col;
	}

	public bool GetIsTaken()
	{
		return isTaken;
	}

	public GameObject GetOccupiedCard()
	{
		return occupiedCard;
	}
}
