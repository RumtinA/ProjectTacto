using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimAdjacent : Attribute {

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
		if (boardSpaces [0].GetComponent<Spaces> ().GetIsTaken() && boardSpaces [0].GetComponent<Spaces> ().GetWhoHasIt() == GetActivePlayer()) // If space 0 is taken by the active player
		{
			if (!boardSpaces [1].GetComponent<Spaces> ().GetIsTaken()) // If space 1 is not taken
			{
				availability [1] = true; // Make space 1 available
			}
			if (!boardSpaces [3].GetComponent<Spaces> ().GetIsTaken()) // If space 3 is not taken
			{
				availability [3] = true; // Make space 3 available
			}
		}
		if (boardSpaces [1].GetComponent<Spaces> ().GetIsTaken() && boardSpaces [1].GetComponent<Spaces> ().GetWhoHasIt() == GetActivePlayer()) // If space 1 is taken by the active player
		{
			if (!boardSpaces [0].GetComponent<Spaces> ().GetIsTaken()) // If space 0 is not taken
			{
				availability [0] = true; // Make space 0 available
			}
			if (!boardSpaces [4].GetComponent<Spaces> ().GetIsTaken()) // If space 4 is not taken
			{
				availability [4] = true; // Make space 4 available
			}
			if (!boardSpaces [2].GetComponent<Spaces> ().GetIsTaken()) // If space 2 is not taken
			{
				availability [2] = true; // Make space 2 available
			}
		}
		if (boardSpaces [2].GetComponent<Spaces> ().GetIsTaken() && boardSpaces [2].GetComponent<Spaces> ().GetWhoHasIt() == GetActivePlayer()) // If space 2 is taken by the active player
		{
			if (!boardSpaces [1].GetComponent<Spaces> ().GetIsTaken()) // If space 1 is not taken
			{
				availability [1] = true; // Make space 1 available
			}
			if (!boardSpaces [5].GetComponent<Spaces> ().GetIsTaken()) // If space 5 is not taken
			{
				availability [5] = true; // Make space 5 available
			}
		}
		if (boardSpaces [3].GetComponent<Spaces> ().GetIsTaken() && boardSpaces [3].GetComponent<Spaces> ().GetWhoHasIt() == GetActivePlayer()) // If space 3 is taken by the active player
		{
			if (!boardSpaces [0].GetComponent<Spaces> ().GetIsTaken()) // If space 0 is not taken
			{
				availability [0] = true; // Make space 0 available
			}
			if (!boardSpaces [6].GetComponent<Spaces> ().GetIsTaken()) // If space 6 is not taken
			{
				availability [6] = true; // Make space 6 available
			}
			if (!boardSpaces [4].GetComponent<Spaces> ().GetIsTaken()) // If space 4 is not taken
			{
				availability [4] = true; // Make space 4 available
			}
		}
		if (boardSpaces [4].GetComponent<Spaces> ().GetIsTaken() && boardSpaces [4].GetComponent<Spaces> ().GetWhoHasIt() == GetActivePlayer()) // If space 4 is taken by the active player
		{
			if (!boardSpaces [1].GetComponent<Spaces> ().GetIsTaken()) // If space 1 is not taken
			{
				availability [1] = true; // Make space 1 available
			}
			if (!boardSpaces [3].GetComponent<Spaces> ().GetIsTaken()) // If space 3 is not taken
			{
				availability [3] = true; // Make space 3 available
			}
			if (!boardSpaces [5].GetComponent<Spaces> ().GetIsTaken()) // If space 5 is not taken
			{
				availability [5] = true; // Make space 5 available
			}
			if (!boardSpaces [7].GetComponent<Spaces> ().GetIsTaken()) // If space 7 is not taken
			{
				availability [7] = true; // Make space 7 available
			}
		}
		if (boardSpaces [5].GetComponent<Spaces> ().GetIsTaken() && boardSpaces [5].GetComponent<Spaces> ().GetWhoHasIt() == GetActivePlayer()) // If space 5 is taken by the active player
		{
			if (!boardSpaces [4].GetComponent<Spaces> ().GetIsTaken()) // If space 4 is not taken
			{
				availability [4] = true; // Make space 4 available
			}
			if (!boardSpaces [2].GetComponent<Spaces> ().GetIsTaken()) // If space 2 is not taken
			{
				availability [2] = true; // Make space 2 available
			}
			if (!boardSpaces [8].GetComponent<Spaces> ().GetIsTaken()) // If space 8 is not taken
			{
				availability [8] = true; // Make space 8 available
			}
		}
		if (boardSpaces [6].GetComponent<Spaces> ().GetIsTaken() && boardSpaces [6].GetComponent<Spaces> ().GetWhoHasIt() == GetActivePlayer()) // If space 6 is taken by the active player
		{
			if (!boardSpaces [3].GetComponent<Spaces> ().GetIsTaken()) // If space 3 is not taken
			{
				availability [3] = true; // Make space 3 available
			}
			if (!boardSpaces [7].GetComponent<Spaces> ().GetIsTaken()) // If space 7 is not taken
			{
				availability [7] = true; // Make space 7 available
			}
		}
		if (boardSpaces [7].GetComponent<Spaces> ().GetIsTaken() && boardSpaces [7].GetComponent<Spaces> ().GetWhoHasIt() == GetActivePlayer()) // If space 7 is taken by the active player
		{
			if (!boardSpaces [6].GetComponent<Spaces> ().GetIsTaken()) // If space 7 is not taken
			{
				availability [6] = true; // Make space 6 available
			}
			if (!boardSpaces [4].GetComponent<Spaces> ().GetIsTaken()) // If space 4 is not taken
			{
				availability [4] = true; // Make space 4 available
			}
			if (!boardSpaces [8].GetComponent<Spaces> ().GetIsTaken()) // If space 8 is not taken
			{
				availability [8] = true; // Make space 8 available
			}
		}
		if (boardSpaces [8].GetComponent<Spaces> ().GetIsTaken() && boardSpaces [8].GetComponent<Spaces> ().GetWhoHasIt() == GetActivePlayer()) // If space 8 is taken by the active player
		{
			if (!boardSpaces [5].GetComponent<Spaces> ().GetIsTaken()) // If space 5 is not taken
			{
				availability [5] = true; // Make space 5 available
			}
			if (!boardSpaces [7].GetComponent<Spaces> ().GetIsTaken()) // If space 7 is not taken
			{
				availability [7] = true; // Make space 7 available
			}
		}


		// Sends out the result
		GetComponentInParent<Attribute> ().SetResult (availability);

	}
}
