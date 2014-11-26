﻿using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{
		
		private Queue queue;
		private TileMap tileMap;

		// Use this for initialization
		void Start ()
		{
				GameObject obj = GameObject.Find ("TileMap");
				tileMap = obj.GetComponent<TileMap> ();
				
				// Create a queue to run the game with.
				queue = new Queue ();
		}
	
		// Update is called once per frame
		void Update ()
		{			
				// Used to find where the mouse is.
				int[] mouse = getMouseLoc ();

				// Used to see if a skill is currently active.
				bool isSkillActive = false;

				// Check for any keyboard keys being pressed.
				// 1-6 are skills
				// Space ends the turn.
				if (Input.GetKeyDown (KeyCode.Alpha1)) {
						isSkillActive = true;
						queue.getActiveCharacter ().showSkill (0);
				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
						isSkillActive = true;
						queue.getActiveCharacter ().showSkill (1);
				} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
						isSkillActive = true;
						queue.getActiveCharacter ().showSkill (2);
				} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
						isSkillActive = true;
						queue.getActiveCharacter ().showSkill (3);

				} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
						isSkillActive = true;
						queue.getActiveCharacter ().showSkill (4);

				} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
						isSkillActive = true;
						queue.getActiveCharacter ().showSkill (5);

				} else if (Input.GetKeyDown (KeyCode.Space)) {
						queue.getActiveCharacter ().endTurn ();
						queue.nextCharacter ();
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
		}

		// Grabs the location of the mouse.
		private int[] getMouseLoc ()
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
}
