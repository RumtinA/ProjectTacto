using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimCorner : Attribute {

	public void ActivateCode()
	{
		GameObject[] boardSpaces = GetComponentInParent<Attribute> ().GetBoardSpaces ();
		bool[] availability = new bool[9];
		for (int x = 0; x > 9; x++) 
		{
			availability [x] = false;
		}
			
		if (boardSpaces [0].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [0] = true;
		}
		if (boardSpaces [2].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [2] = true;
		}
		if (boardSpaces [6].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [6] = true;
		}
		if (boardSpaces [8].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [8] = true;
		}

		GetComponentInParent<Attribute> ().SetResult (availability);

	}
}
