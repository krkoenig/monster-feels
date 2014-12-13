using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization

	void OnGUI(){
		const int buttonWidth = 200;
		const int buttonHeight = 40;

		GUI.backgroundColor = Color.black;
		GUI.contentColor = Color.white;
		GUI.Box (new Rect (Screen.width / 2 - (buttonWidth / 2)-10, (2 * Screen.height / 3)-50 - (buttonHeight / 2)-10, 220, 210), "");
		//(x pos, y pos, x width, y width)

		// Make the first button. If it is pressed, the first level will be loaded
		if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 3) - (buttonHeight / 2)-50, buttonWidth, buttonHeight), "New Game")) {
			Application.LoadLevel ("StoryIntroduction");
		}

		// Make the second button.
		if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 3) - (buttonHeight / 2), buttonWidth, buttonHeight), "Load Game")) {
			Application.LoadLevel (2);
		}
		// Make the third button.
		if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 3) - (buttonHeight / 2)+50, buttonWidth, buttonHeight), "Settings")) {
			Application.LoadLevel (2);
		}
		// Make the forth button.
		if (GUI.Button (new Rect (Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 3) - (buttonHeight / 2)+100, buttonWidth, buttonHeight), "Close Game")) {
			Application.Quit();
		}
	}
}

