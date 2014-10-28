using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

		private TileMap tileMap;
		private float tileSize;
		public Vector3 position;

		private bool currentCharacter;
		private List<Vector3> pastPos;
		private int currMP;
		private static int TOT_MP = 5;

		// Use this for initialization
		void Start ()
		{
				// Fine the TileMap and get its tilesize.
				GameObject obj = GameObject.Find ("TileMap");
				tileMap = obj.GetComponent<TileMap> ();
				tileSize = tileMap.getTileSize ();

				// Set the character to its runtime position.
				transform.position = position;

				// Set the character to have full MP.		
				currMP = TOT_MP;

				// Build the Vector3 array containg all past positions.
				pastPos = new List<Vector3> ();
				pastPos.Add (position);

				// Set that the character isn't the active one.
				currentCharacter = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetMouseButton (0) &&
						currentCharacter) {
						move ();
						
				} 

				if (currentCharacter) {
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
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hitInfo;

				if (tileMap.collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {

						//Debug.Log (hitInfo.point);

						// X is positive, round to floor so it stays at the left of the tile.
						int x = Mathf.FloorToInt (hitInfo.point.x / tileSize);
						// Y is negative, round to ceiling so it stays at the top of the tile.			
						int y = Mathf.FloorToInt (hitInfo.point.y / tileSize);

						//Debug.Log ("Tile: " + x + " " + y);

						// Can move left or right
						if (x == position.x && (y == position.y + 1 || y == position.y - 1) ||
								(y == position.y && (x == position.x + 1 || x == position.x - 1))) {
								
								// If moving back to a square that was moved this turn
								if (x == pastPos [0].x && y == pastPos [0].y) {

										if (tileMap.getTileData (Convert.ToInt32 (position.x), Convert.ToInt32 (position.y)) == 1) {
												currMP += 2;
										} else {
												currMP++;
										}

										pastPos.RemoveAt (0);
										
										position.x = x;
										position.y = y;
					
										transform.position = position * tileSize;
										
								} else if (currMP != 0) {			

										if (tileMap.getTileData (x, y) == 1) {
												if (currMP < 2) {
														return;
												}						
												currMP -= 2;
										} else {
												currMP--;
										}

										pastPos.Insert (0, position);

										position.x = x;
										position.y = y;
				
										transform.position = position * tileSize;
										
								}
						}

						Debug.Log (currMP);
				}

		}

		public void setCurrentcharacter (bool val)
		{
				currentCharacter = val;
		}
}
