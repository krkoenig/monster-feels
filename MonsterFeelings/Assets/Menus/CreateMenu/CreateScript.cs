using UnityEngine;
using System.Collections;
using System;

public class CreateScript : MonoBehaviour
{

		GUIContent[] comboBoxRace;
		private ComboBox comboRaceControl;
		GUIContent[] comboBoxClass;
		private ComboBox comboClassControl;
		private GUIStyle listStyle = new GUIStyle ();

		// Use this for initialization
		void Start ()
		{
				//Create the list style
				listStyle.normal.textColor = Color.white; 
				listStyle.onHover.background =
		listStyle.hover.background = new Texture2D (2, 2);
				listStyle.padding.left = listStyle.padding.right = listStyle.padding.top = listStyle.padding.bottom = 4;


				//////////////Race choosing //////////
				/// /Create the list of possible options
				this.comboBoxRace = new GUIContent[4];
				this.comboBoxRace [0] = new GUIContent ("Goblin");
				this.comboBoxRace [1] = new GUIContent ("Ogre");
				comboBoxRace [2] = new GUIContent ("Orc");
				comboBoxRace [3] = new GUIContent ("Troll");
				//Create the button to open the list
				this.comboRaceControl = new ComboBox (new Rect (50, 100, 120, 20), new GUIContent (""), comboBoxRace, "button", "box", listStyle);

				//////////////Class choosing //////////
				//Create the list of possible options
				this.comboBoxClass = new GUIContent[3];
				this.comboBoxClass [0] = new GUIContent ("Fighter");
				this.comboBoxClass [1] = new GUIContent ("Mage");
				comboBoxClass [2] = new GUIContent ("Rogue");
				//Create the button to open the list
				this.comboClassControl = new ComboBox (new Rect (50, 130, 120, 20), new GUIContent (""), comboBoxClass, "button", "box", listStyle);




		}
	
		// Update is called once per frame
		void OnGUI ()
		{
				GUI.Box (new Rect (32, 20, 185, Screen.height - 40), "");
				//Add the Race button
				comboRaceControl.Show ();
				//Add the Class button
				comboClassControl.Show ();
				//Add field to write the name of the character


		}
}
