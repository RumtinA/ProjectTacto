using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaces : MonoBehaviour {

    public string name; //Name of Space
    public string type; //Type of Space (Center, Corner, Cross)
	private int whoHasIt;
    public int BoardPosition; //Position of space in relation to board, position numbers are 1-9
	private bool isTaken; //Is position already occupied?
	private bool isAvailable; //Is position available to select?
	private Animator animator;
	public GameObject manager;
	private int activePlayer;

	// Use this for initialization
	void Start ()
	{
		isTaken = false;
		isAvailable = false;
		animator = GetComponent<Animator> ();
		whoHasIt = -1;
		//gameObject.GetComponent<SpriteRenderer> ().sprite = isNotAvailableSprite;
	}
	
	// Update is called once per frame
	void Update () 
	{

        
	}

	public void SetIsTaken(bool b)
	{
		isTaken = b;
		SetIsAvailable (false);
		if (isTaken == true && manager.GetComponent<GameManagerPlay>().GetIsPlayerTurn()) 
		{
			whoHasIt = PlayerPrefs.GetInt ("Player Color");
			//gameObject.GetComponent<SpriteRenderer> ().sprite = isTakenSpriteRed;
			if (PlayerPrefs.GetInt ("Player Color") == 0) 
			{
				animator.SetBool ("isTakenRed", true);
			} 
			else 
			{
				animator.SetBool ("isTakenBlue", true);
			}
		} 
		else
		{
			whoHasIt = PlayerPrefs.GetInt("Opponent Color");
			if (PlayerPrefs.GetInt ("Opponent Color") == 0) 
			{
				animator.SetBool ("isTakenRed", true);
			}
			else
			{
			animator.SetBool ("isTakenBlue", true);
			}

		}
	}

	public bool GetIsTaken()
	{
		return isTaken;
	}

	public void SetIsAvailable(bool b)
	{
		animator = GetComponent<Animator> ();
		isAvailable = b;
		animator.SetBool ("isAvailable", b);
	}

	public bool GetIsAvailable()
	{
		return isAvailable;
	}

	void OnMouseDown()
	{
		if (animator.GetBool("isAvailable")) 
		{
			Debug.Log ("You pressed it!");
			SetIsTaken (true);
			manager.GetComponent<GameManagerPlay> ().turnDone ();
		}
	}

	public int GetWhoHasIt()
	{
		return whoHasIt;
	}

	public void SetActivePlayer(int i)
	{
		activePlayer = i;
	}

	public int GetActivePlayer()
	{
		return activePlayer;
	}
}
