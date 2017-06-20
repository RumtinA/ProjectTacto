using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimCross : Attribute {

	public void ActivateCode()
	{
		// Prepares the code
		GameObject[] boardSpaces = GetComponentInParent<Attribute> ().GetBoardSpaces ();
		bool[] availability = new bool[9];
		for (int x = 0; x > 9; x++) 
		{
			availability [x] = false;
		}

		// Unique Code
		if (boardSpaces [1].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [1] = true;
		}
		if (boardSpaces [3].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [3] = true;
		}
		if (boardSpaces [5].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [5] = true;
		}
		if (boardSpaces [7].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [7] = true;
		}

		// Sends out the result
		GetComponentInParent<Attribute> ().SetResult (availability);

	}
}
