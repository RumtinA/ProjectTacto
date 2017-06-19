using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private int playerColor;
	private int enemyColor;
	private int first;
	private int second;
	private int third;
	private int fourth;
	private int fifth;
	public GameObject[] prefabulousRed;
	public GameObject[] prefabulousBlue;
	public GameObject[] squares;
	private GameObject card1;
	private GameObject card2;
	private GameObject card3;
	private GameObject card4;
	private GameObject card5;
	private GameObject square1;
	private GameObject square2;
	private GameObject square3;
	public GameObject background;
	private bool lockInVisible;
	private bool lockedIn;

	// Use this for initialization
	public GameManager (int p, int e) {
	}
		

	void Start ()
	{
		lockInVisible = false;
		lockedIn = false;
		SetPlayerColor (1);  // Default sets player to red
		SetEnemyColor (0); // Default sets enemy to blue
		if (GetPlayerColor () == 0) 		
		{
			background.transform.rotation = new Quaternion (0.0f, 0.0f, 180.0f, 0.0f);
			square1 = Instantiate (squares [0], new Vector3 (-84.0f, 53.0f, 101.0f), Quaternion.identity) as GameObject;
			square2 = Instantiate (squares [1], new Vector3 (0.0f, 53.0f, 101.0f), Quaternion.identity) as GameObject;
			square3 = Instantiate (squares [2], new Vector3 (84.0f, 53.0f, 101.0f), Quaternion.identity) as GameObject;
			PlayerPrefs.SetInt ("Player Color", 0);
		} 
		else 
		{
			square1 = Instantiate (squares [3], new Vector3 (-84.0f, 53.0f, 101.0f), Quaternion.identity) as GameObject;
			square2 = Instantiate (squares [4], new Vector3 (0.0f, 53.0f, 101.0f), Quaternion.identity) as GameObject;
			square3 = Instantiate (squares [5], new Vector3 (84.0f, 53.0f, 101.0f), Quaternion.identity) as GameObject;
			PlayerPrefs.SetInt ("Player Color", 1);
		}
		DealCardsToPlayer (); // Randomly decides what cards the player gets and then calls the Spawn() to spawn the cards
		first = 666; // Starting value for first
		second = 666; // Starting value for second
		third = 666; // Starting value for third
		fourth = 666; // Starting value for four
		fifth = 666; // Starting value for five
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (square1.GetComponent<ProgramMat> ().GetIsTaken () && square2.GetComponent<ProgramMat> ().GetIsTaken () && square3.GetComponent<ProgramMat> ().GetIsTaken () && lockInVisible == false) 
		{
			lockInVisible = true;
		
		}
	}

	void SetCards()
	{
		
	}

	void DealCardsToPlayer ()
	{
		first = Random.Range (0, 15);
	//	Debug.Log (first);
		second = first;
	//	Debug.Log (second);
		while (second == first)
		{
			second = Random.Range (0, 15);
		}
	//	Debug.Log (second);
		third = second;
	//	Debug.Log (third);
		while (third == second || third == first) 
		{
			third = Random.Range (0, 15);
		}
	//	Debug.Log (third);
		fourth = third;
	//	Debug.Log (fourth);
		while (fourth == third || fourth == second || fourth == first) 
		{
			fourth = Random.Range (0, 15);
		}
	//	Debug.Log (fourth);
		fifth = fourth;
	//	Debug.Log (fifth);
		while (fifth == fourth || fifth == third || fifth == second || fifth == first) 
		{
			fifth = Random.Range (0, 15);
		}
	//	Debug.Log (fifth);
		if (playerColor == 0) 
		{
			Spawn (prefabulousRed, first, second, third, fourth, fifth);
		} else 
		{
			Spawn (prefabulousBlue, first, second, third, fourth, fifth);
		}
	}

	void Spawn (GameObject[] prefabulous, int n1, int n2, int n3, int n4, int n5)
	{
		card1 = Instantiate (prefabulous[n1], new Vector3(175f, -50.0f, 108f), Quaternion.identity) as GameObject;
		card2 = Instantiate (prefabulous[n2], new Vector3(210f, -50.0f, 108f), Quaternion.identity) as GameObject;
		card3 = Instantiate (prefabulous[n3], new Vector3(245f, -50.0f, 108f), Quaternion.identity) as GameObject;
		card4 = Instantiate (prefabulous[n4], new Vector3(280f, -50.0f, 108f), Quaternion.identity) as GameObject;
		card5 = Instantiate (prefabulous[n5], new Vector3(315f, -50.0f, 108f), Quaternion.identity) as GameObject;
	}
		

	void SetPlayerColor (int n)
	{
		playerColor = n;
	}

	void SetEnemyColor (int n)
	{
		enemyColor = n;
	}

	int GetPlayerColor ()
	{
		return playerColor;
	}

	int GetEnemyColor ()
	{
		return enemyColor;
	}
		
}
