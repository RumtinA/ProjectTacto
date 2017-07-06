using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	public string nameOfButton;

	public void CallMenu(Button theButton)
	{
		GameObject.Find ("Menu Manager").GetComponent<MenuManager> ().buttonSelected (theButton, nameOfButton);
	}

	public void CallMenuPlay(Button theButton)
	{
		GameObject.Find ("Menu Manager").GetComponent<MenuManager> ().buttonSelectedPlay (theButton, nameOfButton);
	}

	public void CallMenuMuteMusc(Button thebutton)
	{
		bool t = GameObject.Find ("Menu Manager").GetComponent<MenuManager> ().SetMusicOptions (thebutton);
		if (!t)
			GameObject.Find("Music Button(Clone)").GetComponentInChildren<Text> ().text = "Off";
		else
			GameObject.Find("Music Button(Clone)").GetComponentInChildren<Text> ().text = "On";
	}

	public void CallMenuMuteFx(Button thebutton)
	{
		bool t = GameObject.Find ("Menu Manager").GetComponent<MenuManager> ().SetSoundFXOptions (thebutton);
		if (!t)
			gameObject.GetComponentInChildren<Text> ().text = "Off";
		else
			gameObject.GetComponentInChildren<Text> ().text = "On";
	}
}
