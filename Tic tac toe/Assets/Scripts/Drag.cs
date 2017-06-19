using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Drag : MonoBehaviour {

	private Vector3 offset;
	private Animator animator; // Grabs the animator
	private BoxCollider2D col;
	private bool isSelected;
	private bool isDragging;
	private bool singleClick;
	private Collider2D colliding;
	private bool rotated;
	private Cards thisCard;
	private Vector3 startPosition; // The starting position of the card
	private Vector3 startOffScreen;

	void Start()
	{
		offset = new Vector3 (0, 0, 0);
		col = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		isSelected = false;
		isDragging = false;
		singleClick = false;
		rotated = false;
		thisCard = gameObject.GetComponent<Cards> ();
		startOffScreen = gameObject.transform.position;
		startPosition = gameObject.transform.position;
		startPosition.x = gameObject.transform.position.x - 245.0f;
		Debug.Log (startPosition);
		StartCoroutine (MoveOverSeconds (gameObject, startPosition, 1.0f));
	}

	void OnMouseDown()
	{
		thisCard.SetIsStored (false);
		if (singleClick == false && rotated != true) 
		{ // If single click is false
			singleClick = true;  // Then it is possible to single click
			animator.SetBool ("singleClick", true); // Tells the animator that it could be a single click
		} 
		else // If single click is still true
		{
			singleClick = false; // then the item is no longer single clicked
			animator.SetBool("singleClick", false); // Tells the animator that it is no longer a single click
		}

		Debug.Log ("You have clicked on it!");
		if (rotated == true) 
		{
			transform.Rotate (Vector3.forward * 90);
			rotated = false;
		}
		offset = gameObject.transform.position - (new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		Cursor.visible = false;
		// FROM IDLE




		isSelected = true; // Becomes true because selected
		animator.SetBool ("isSelected", true); // Tells animator that the item is selected
	}

	void OnMouseUp()
	{
		isSelected = false; // No longer selected
		animator.SetBool ("isSelected", false); // Tells animator that the item is no longer selected
		// colliding.GetComponent<ProgramMat>().chooseOrRemoveCard(false, null);
		if (isDragging == true) // If it was being dragged
		{
			isDragging = false; // It's no longer being dragged
			animator.SetBool("isDragging", false);
		}
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);
		if (stateInfo.IsName("Currently Dragging"))
			{
				singleClick = false;
				animator.SetBool("singleClick", false);
			}
		if (col.IsTouching (colliding) && colliding.CompareTag ("Program") && thisCard.GetColor () == colliding.GetComponent<ProgramMat> ().GetColor ()) {
			Debug.Log ("Collision Success!");
			transform.position = colliding.gameObject.transform.position;
			transform.Rotate (Vector3.forward * -90);
			rotated = true;
			thisCard.SetIsStored (true);
			colliding.GetComponent<ProgramMat> ().chooseOrRemoveCard (true, gameObject);
		} else {
			gameObject.transform.position = GetStartPosition();
		}
		Cursor.visible = true;
	}
		

	void OnMouseDrag()
	{
		Debug.Log ("You're dragging!");
		isDragging = true;  // Becomes true because you're dragging
		animator.SetBool("isDragging", true);
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		Vector3 curPosition = (curScreenPoint) + offset;
		transform.position = curPosition;

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		colliding = other;
		Debug.Log ("It has collided");
	}

	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator MoveOverSpeed (Vector3 end, float speed)
	{
		// speed is 1 unit per second
		while (gameObject.transform.position != end) 
		{
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, end, speed * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
	}

	public IEnumerator MoveOverSeconds (Vector3 end, float seconds)
	{
		float elapsedTime = 0;
		Vector3 startingPos = gameObject.transform.position;
		while (elapsedTime < seconds) {
			gameObject.transform.position = Vector3.Lerp (startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		this.gameObject.transform.position = end;

	}

	public IEnumerator MoveOverSpeed (GameObject objectToMove, Vector3 end, float speed)
	{
		// speed is 1 unit per second
		while (objectToMove.transform.position != end) 
		{
			objectToMove.transform.position = Vector3.MoveTowards (objectToMove.transform.position, end, speed * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
	}

	public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
	{
		float elapsedTime = 0;
		Vector3 startingPos = objectToMove.transform.position;
		while (elapsedTime < seconds) {
			objectToMove.transform.position = Vector3.Lerp (startingPos, end, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		objectToMove.transform.position = end;
	}


	public Vector3 GetStartPosition()
	{
		return startPosition;
	}
}
