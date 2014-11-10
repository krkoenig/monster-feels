using UnityEngine;
using System.Collections;

public class Menu_GUI : MonoBehaviour
{

		void OnGUI ()
		{
				// Make a background box
				GUI.Box (new Rect (10, 10, 220, 100), "This is the Start Menu");
				//(x pos, y pos, x width, y width)

				// Make the first button. If it is pressed, the first level will be loaded
				if (GUI.Button (new Rect (20, 40, 200, 20), "One Character")) {
						Application.LoadLevel (1);
				}
		
				// Make the second button.
				if (GUI.Button (new Rect (20, 70, 200, 20), "Two Characters")) {
						Application.LoadLevel (2);
				}
		}
}

//To add more levels, just follow the format
//To change the level name, just change what's in the quotes in Application.LoadLevel("")