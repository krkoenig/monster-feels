using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls : MonoBehaviour
{
		
		private Queue queue;
		private TileMap tileMap;
		public  GUISkin  newSkin;
	

		// Use this for initialization
		void Start ()
		{
				GameObject obj = GameObject.Find ("TileMap");
				tileMap = obj.GetComponent<TileMap> ();
				
				// Create a queue to run the game with.
				queue = new Queue ();
				Camera.main.GetComponent<CameraController> ().moveToActive (queue.getActiveCharacter ().getPosition ());

		}
	
		// Update is called once per frame
		void Update ()
		{			
				queue.moveActiveTile ();
				// Used to find where the mouse is.
				int[] mouse = getMouseLoc ();

				// Check for any keyboard keys being pressed.
				// 1-6 are skills
				// Space ends the turn.
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
						queue.getActiveCharacter ().showSkill (0);
				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
						queue.getActiveCharacter ().showSkill (1);
				} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
						queue.getActiveCharacter ().showSkill (2);
				} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
						queue.getActiveCharacter ().showSkill (3);

				} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
						queue.getActiveCharacter ().showSkill (4);

				} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
						queue.getActiveCharacter ().showSkill (5);

				} else if (Input.GetKeyDown (KeyCode.Space)) {
						queue.getActiveCharacter ().endTurn ();
						queue.nextCharacter ();
						Camera.main.GetComponent<CameraController> ().moveToActive (queue.getActiveCharacter ().getPosition ());
				}

				if (isOnTileMap (mouse [0], mouse [1])) {
						
						Tile targetTile = tileMap.getTile (mouse [0], mouse [1]);

						// If the left mouse button is pressed, do a move action.
						// If the right mouse button is pressed and a skill is being shown.
						if (Input.GetMouseButton (0)) {
								queue.getActiveCharacter ().move (targetTile);
						} else if (Input.GetMouseButton (1) && queue.getActiveCharacter ().ShownSkill != -1) {
								queue.getActiveCharacter ().useSkill (queue.getActiveCharacter ().ShownSkill, targetTile);
						}
				}	

				queue.removeDead ();
				victoryDefeat ();
		}


		void OnGUI ()
		{
				queue.removeDead ();

				GUI.skin = newSkin;
				// Make a background box
				GUI.Box (new Rect (0, 0, Screen.width, 64), "");

				List<Character> chars = queue.listAll ();
				for (int i = 0; i < chars.Count; i++) {
						if (i * 64 <= Screen.width) {
								Texture t = chars [i].GetComponent<SpriteRenderer> ().sprite.texture;
								GUI.DrawTexture (new Rect (i * 64, 0, 64, 64), t);
						}
				}

				int difference = 5 - chars [0].getSkills ().Count;
				Texture2D tex;
				string charClass = chars [0].getClass ();
		
				//make initiative queue of next 5 people
				for (int i = 0; i < 5; i++) {
						if (i * 64 <= Screen.width) {	
								if (i < chars.Count) {
										Texture t = chars [i].GetComponent<SpriteRenderer> ().sprite.texture;
										GUI.DrawTexture (new Rect (i * 64, 0, 64, 64), t);
								}
						}
				}
				//list active character's skills if it's not an enemy
				if (chars [0].isAlly == true) {
						for (int i = 0; i < 7; i++) {
								if (i < chars [0].getSkills ().Count) {
										tex = chars [0].getSkills () [i].getIcon ();
										GUI.DrawTexture (new Rect ((i + 8) * 64, 0, 64, 64), tex);
								}
								//list AP and MP
								if (i == 5) {
										int ap = chars [0].getCurrentAP ();
										int mp = chars [0].getCurrentMP ();
										GUI.TextArea (new Rect ((i + 8) * 64, 0, 64, 64), "AP: " + ap + " MP: " + mp);
								}
						}
				}
		}
	
		// Grabs the location of the mouse.
		public static int[] getMouseLoc ()
		{
				// Mouse location
				// Convert the mouse's screen coordinates to world coordinates.
				Vector3 mousePos = Input.mousePosition;
				mousePos.z = 1.0f;
				mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		
				// Convert them into int's.
				int mouseX = Mathf.FloorToInt (mousePos.x);
				int mouseY = Mathf.FloorToInt (mousePos.y);

				return new int[2] {mouseX, mouseY};
		}

		// Checks to see if the mouse is  currently on the tilemap.
		private bool isOnTileMap (int mouseX, int mouseY)
		{
				if (mouseX < tileMap.mapX && mouseY >= 0 &&
						mouseY < tileMap.mapY && mouseX >= 0) {
						return true;
				}
				return false;
		}
		
		private void victoryDefeat ()
		{
				int counter = 0;
				List<Character> chars = queue.listAll ();
				for (int i = 0; i < chars.Count; i++) {
						if (chars [i].isAlly) {
								counter++;
						}
				}
				
				if (counter == 0) {
						Application.LoadLevel (0);
				} else if (counter == chars.Count) {
						Application.LoadLevel (0);
				}
		}
}
