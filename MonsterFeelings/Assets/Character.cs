using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

		private TileMap tileMap;
		public Vector3 position;

		private bool isTurn;
		private List<Vector3> pastPos;
		private int currMP;
		private static int TOT_MP = 5;

		// Use this for initialization
		void Start ()
		{
				// Fine the TileMap.
				GameObject obj = GameObject.Find ("TileMap");
				tileMap = obj.GetComponent<TileMap> ();

				// Set the character to its runtime position.
				transform.position = position;

				// Set the character to have full MP.		
				currMP = TOT_MP;

				// Record it's starting position as a position it has been in.
				pastPos = new List<Vector3> ();
				pastPos.Add (position);
				
				// By default, it isn't the charatcer's turn.
				isTurn = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				// If it's the character's turn and the user is trying to move.
				if (Input.GetMouseButton (0) &&
						isTurn) {
						move ();
						
				} 

				if (isTurn) {
						// Make the character green if it is active.
						MeshRenderer rend = GetComponentInChildren<MeshRenderer> ();	
						rend.material.color = Color.green;
						
						// Put the active character on top of any other characters sharing its space.
						position = transform.position;
						position.z = -2;
						transform.position = position;
				} else {
						// ...otherwise make it blue.
						MeshRenderer rend = GetComponentInChildren<MeshRenderer> ();	
						rend.material.color = Color.blue;

						// ..otherwise put it below the active character.
						position = transform.position;
						position.z = -1;
						transform.position = position;
				}

				if (Input.GetKeyDown ("space")) {
						// Reset the character's movement.
						currMP = TOT_MP;
						pastPos.Clear ();
						pastPos.Add (position);
				}

		}

		private void move ()
		{
				// Used for finding which tile the mouse is over.
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hitInfo;

				// If there is an intersection...
				if (tileMap.collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {

						// The tile that the mouse is at.
						int x = Mathf.FloorToInt (hitInfo.point.x);
						int y = Mathf.FloorToInt (hitInfo.point.y);

						// Can move left, right, up, or down.
						if (x == position.x && (y == position.y + 1 || y == position.y - 1) ||
								(y == position.y && (x == position.x + 1 || x == position.x - 1))) {
								
								// If moving back to a square that was moved this turn
								if (x == pastPos [0].x && y == pastPos [0].y) {

										// Handle terrain movement info.
										if (tileMap.getTileData (Convert.ToInt32 (position.x), Convert.ToInt32 (position.y)) == 1) {
												currMP += 2;
										} else {
												currMP++;
										}
										
										// Remove the position it was last at.
										pastPos.RemoveAt (0);
										
										position.x = x;
										position.y = y;

										transform.position = position;
								
										// If we aren't out of move.
								} else if (currMP != 0) {			

										// Handle terrain movement info.
										if (tileMap.getTileData (x, y) == 1) {
												
												// Don't move if you don't have enough MP.
												if (currMP < 2) {
														return;
												}						
												currMP -= 2;
										} else {
												currMP--;
										}

										// Add the new position to the past positions.
										pastPos.Insert (0, position);

										position.x = x;
										position.y = y;
				
										transform.position = position;
										
								}
						}
				}

		}

		// Allows the queue to handle the active character.
		public void setIsTurn (bool val)
		{
				isTurn = val;
		}
}
