using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public Button playButton, optionsButton, creditsButton;
	private bool animStarted;

	void Start()
	{
		animStarted = false;
	}
		
	public void buttonSelected(Button selectedButton)
	{
		if (animStarted)
			return;
		animStarted = true;
		GameObject other1, other2;
		selectedButton.interactable = false;
		if (selectedButton.name.Equals("Play Button"))
		{
			other1 = (GameObject.Find ("Options Button"));
			other2 = (GameObject.Find ("Credits Button"));
		}
		else if (selectedButton.name.Equals("Options Button"))
		{
			other1 =  (GameObject.Find ("Play Button"));
			other2 = (GameObject.Find ("Credits Button"));
		}
		else
		{
			other1 =  (GameObject.Find ("Options Button"));
			other2 = (GameObject.Find ("Play Button"));
		}
		StartCoroutine (KillButton(selectedButton, other1, other2));

	}

	private IEnumerator KillButton(Button selectedButton, GameObject other1, GameObject other2)
	{
		selectedButton.animator.Play ("ButtonAnim");
	//	other1.GetComponent<Animator> ().Play ("SwirlAway");
	//	other2.GetComponent<Animator> ().Play ("SwirlAway");
		yield return new WaitForSeconds (1.0f);
		DestroyObject (selectedButton.gameObject);
		DestroyObject (other1);
		DestroyObject (other2);
		animStarted = false;
	} 
}
