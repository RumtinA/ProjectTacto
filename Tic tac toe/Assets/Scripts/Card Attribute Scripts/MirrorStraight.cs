using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorStraight : Attribute {

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
		if (boardSpaces [0].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[2].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[2].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [0] = true;
		}
		if (boardSpaces [2].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[0].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[0].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [2] = true;
		}
		if (boardSpaces [3].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[5].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[5].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [3] = true;
		}
		if (boardSpaces [5].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[3].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[3].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [5] = true;
		}
		if (boardSpaces [6].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[8].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[8].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [6] = true;
		}
		if (boardSpaces [8].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[6].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[6].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [8] = true;
		}
		if (boardSpaces [0].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[6].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[6].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [0] = true;
		}
		if (boardSpaces [6].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[0].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[0].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [6] = true;
		}
		if (boardSpaces [1].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[7].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[7].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [1] = true;
		}
		if (boardSpaces [7].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[1].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[1].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [7] = true;
		}
		if (boardSpaces [2].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[8].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[8].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [2] = true;
		}
		if (boardSpaces [8].GetComponent<Spaces> ().GetIsTaken() == false && boardSpaces[2].GetComponent<Spaces>().GetIsTaken() == true && boardSpaces[2].GetComponent<Spaces>().GetWhoHasIt() != GetActivePlayer())
		{
			availability [8] = true;
		}

		// Sends out the result
		GetComponentInParent<Attribute> ().SetResult (availability);

	}
}
