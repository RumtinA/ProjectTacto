using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour {

	private bool[] result;
	private GameObject[] boardSpaces;
	private int activePlayer;

	void Start()
	{
	}

	public bool[] callCode(GameObject thisObject, GameObject[] bSpaces, int i)
	{
		SetBoardSpaces (bSpaces);
		SetActivePlayer (i);
		thisObject.SendMessage ("ActivateCode");
		return result;
	}

	public void SetResult(bool[] b)
	{
		result = b;
	}

	public void SetBoardSpaces(GameObject[] b)
	{
		boardSpaces = b;
	}

	public GameObject[] GetBoardSpaces()
	{
		return boardSpaces;
	}

	public void SetActivePlayer(int i)
	{
		activePlayer = i;
	}

	public int GetActivePlayer()
	{
		return activePlayer;
	}

	/* public void ActivateCode()
	{
		
	} */
}
