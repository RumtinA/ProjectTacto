using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteDiagonalCenter : Attribute {

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
		if (boardSpaces [4].GetComponent<Spaces> ().GetIsTaken()) // If the center is taken
		{
			GetComponentInParent<Attribute> ().SetResult (availability);
			return;
		}
		if (boardSpaces [0].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [0].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If the upper left corner is taken and not by the active player
		{
			if (boardSpaces [8].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [8].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If the lower right corner is taken and not by the active player
			{
				availability [4] = true;
			}
		}
		if (boardSpaces [2].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [2].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If the upper right corner is taken and not by the active player
		{
			if (boardSpaces [6].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [6].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If the lower left corner is taken and not by the active player
			{
				availability [4] = true;
			}
		}

		// Sends out the result
		GetComponentInParent<Attribute> ().SetResult (availability);

	}
}
