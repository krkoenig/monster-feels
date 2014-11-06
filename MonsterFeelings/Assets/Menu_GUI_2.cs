using UnityEngine;
using System.Collections;

public class Menu_GUI_2 : MonoBehaviour
{

		bool display_gui = false;

		void OnGUI ()
		{
				// Make a background box
				if (display_gui == true) {

						GUI.Box (new Rect (10, 10, 100, 90), "Menu");
			
						// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
						if (GUI.Button (new Rect (20, 40, 80, 20), "Level 1")) {
								Application.LoadLevel (1);
						}
			
						// Make the second button.
						if (GUI.Button (new Rect (20, 70, 80, 20), "Start Menu")) {
								Application.LoadLevel (0);
						}
				}
		}

		void Update ()
		{
				if (Input.GetKeyDown ("escape")) {
						display_gui = !display_gui;
				}

		}
}
