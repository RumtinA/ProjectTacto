using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimCenter : Attribute {

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
		if (boardSpaces [4].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [4] = true;
		}

		// Sends out the result
		GetComponentInParent<Attribute> ().SetResult (availability);

	}
}
