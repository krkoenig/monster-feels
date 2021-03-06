using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {
	
	// Use this for initialization
	
	void OnGUI(){
		const int buttonWidth = 200;
		const int buttonHeight = 40;
		
		GUI.backgroundColor = Color.black;
		GUI.contentColor = Color.white;
		
		// Make continue button. If it is pressed, the character creation will be loaded
//		if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 3) - (buttonHeight / 2)-50, buttonWidth, buttonHeight), "Continue")) {
//			Application.LoadLevel (1);
//		}

		if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), (Screen.height/2 - buttonHeight)+100, buttonWidth, buttonHeight), "Next Level")) {
			Application.LoadLevel ("Level2");
		}

		if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), (Screen.height/2 - buttonHeight)+150, buttonWidth, buttonHeight), "Main Menu")) {
			Application.LoadLevel ("MainMenu");
		}
	}
}