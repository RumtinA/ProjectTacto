using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimCenter : Attribute {

	public void ActivateCode()
	{
		GameObject[] boardSpaces = GetComponentInParent<Attribute> ().GetBoardSpaces ();
		bool[] availability = new bool[9];
		for (int x = 0; x > 9; x++) 
		{
			availability [x] = false;
		}


		if (boardSpaces [4].GetComponent<Spaces> ().GetIsTaken() == false) 
		{
			availability [4] = true;
		}

		GetComponentInParent<Attribute> ().SetResult (availability);

	}
}
