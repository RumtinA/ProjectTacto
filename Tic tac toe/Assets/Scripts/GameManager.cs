using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private string[] cardListRed;
	private string[] cardListBlue;
	private int playerColor;
	private int enemyColor;
	private int first;
	private int second;
	private int third;
	private int fourth;
	private int fifth;
	public GameObject[] prefabulous;

	// Use this for initialization
	public GameManager (int p, int e) {
	}
		

	void Start ()
	{
		SetPlayerColor (0);
		SetEnemyColor (1);
		cardListRed = new string[15];
		cardListBlue = new string[15];
		SetCards ();
		DealCardsToPlayer ();
		first = 666;
		second = 666;
		third = 666;
		fourth = 666;
		fifth = 666;
		prefabulous = new GameObject[15];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetCards()
	{
		
	}

	void DealCardsToPlayer ()
	{
		string[] deck;
		if (GetPlayerColor() == 0) 
		{
			deck = GetCardListRed ();
		} 
		else 
		{
			deck = GetCardListBlue ();
		}
		first = Random.Range (0, 15);
		Debug.Log (first);
		second = first;
		Debug.Log (second);
		while (second == first)
		{
			second = Random.Range (0, 15);
		}
		Debug.Log (second);
		third = second;
		Debug.Log (third);
		while (third == second || third == first) 
		{
			third = Random.Range (0, 15);
		}
		Debug.Log (third);
		fourth = third;
		Debug.Log (fourth);
		while (fourth == third || fourth == second || fourth == first) 
		{
			fourth = Random.Range (0, 15);
		}
		Debug.Log (fourth);
		fifth = fourth;
		Debug.Log (fifth);
		while (fifth == fourth || fifth == third || fifth == second || fifth == first) 
		{
			fifth = Random.Range (0, 15);
		}
		Debug.Log (fifth);

		Spawn (first, second, third, fourth, fifth);	
	}

	void Spawn (int n1, int n2, int n3, int n4, int n5)
	{
		Instantiate (prefabulous [n1]);
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

	string[] GetCardListRed ()
	{
		return cardListRed;
	}

	string[] GetCardListBlue ()
	{
		return cardListBlue;
	}
}
