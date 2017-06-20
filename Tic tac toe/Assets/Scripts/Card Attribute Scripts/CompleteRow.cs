using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteRow : Attribute {

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
		if (boardSpaces [0].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [0].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 0 is taken and by the active player
		{
			if (boardSpaces [1].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [1].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 1 is taken and by the active player
			{
				availability [2] = true;
			}
		}
		if (boardSpaces [1].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [1].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 1 is taken and by the active player
		{
			if (boardSpaces [2].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [2].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 2 is taken and by the active player
			{
				availability [0] = true;
			}
		}
		if (boardSpaces [3].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [3].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 3 is taken and by the active player
		{
			if (boardSpaces [4].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [4].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 4 is taken and by the active player
			{
				availability [5] = true;
			}
		}
		if (boardSpaces [4].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [4].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 4 is taken and by the active player
		{
			if (boardSpaces [5].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [5].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 5 is taken and by the active player
			{
				availability [3] = true;
			}
		}
		if (boardSpaces [6].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [6].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 6 is taken and by the active player
		{
			if (boardSpaces [7].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [7].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 7 is taken and by the active player
			{
				availability [8] = true;
			}
		}
		if (boardSpaces [8].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [8].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 8 is taken and by the active player
		{
			if (boardSpaces [7].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [7].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 7 is taken and by the active player
			{
				availability [6] = true;
			}
		}
		if (boardSpaces [0].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [0].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 0 is taken and by the active player
		{
			if (boardSpaces [3].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [3].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 3 is taken and by the active player
			{
				availability [6] = true;
			}
		}
		if (boardSpaces [3].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [3].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 3 is taken and by the active player
		{
			if (boardSpaces [6].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [6].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 6 is taken and by the active player
			{
				availability [0] = true;
			}
		}
		if (boardSpaces [1].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [1].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 1 is taken and by the active player
		{
			if (boardSpaces [4].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [4].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 4 is taken and by the active player
			{
				availability [7] = true;
			}
		}
		if (boardSpaces [7].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [7].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 7 is taken and by the active player
		{
			if (boardSpaces [4].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [4].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 4 is taken and by the active player
			{
				availability [1] = true;
			}
		}
		if (boardSpaces [2].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [2].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 2 is taken and by the active player
		{
			if (boardSpaces [5].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [5].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 5 is taken and by the active player
			{
				availability [8] = true;
			}
		}
		if (boardSpaces [8].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [8].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 8 is taken and by the active player
		{
			if (boardSpaces [5].GetComponent<Spaces> ().GetIsTaken () && boardSpaces [5].GetComponent<Spaces> ().GetWhoHasIt () == GetActivePlayer ()) // If space 5 is taken and by the active player
			{
				availability [2] = true;
			}
		}


		// Sends out the result
		GetComponentInParent<Attribute> ().SetResult (availability);

	}
}
