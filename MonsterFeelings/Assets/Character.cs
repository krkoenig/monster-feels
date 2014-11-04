using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

		private TileMap tileMap;
		public Vector3 position;

		private string name;
		
		private int level;
		private int xp;

		// Str, Dex, Con, Int, Wis
		private List<int> abilitites;

		// HP, PhyAtk, PhyDef, MagAtk, MagDef	
		private List<float> stats;

		//private List<Item> inventory;
		//private Armor armor;
		//private Weapon weapon;

		//private List<Skill> skills;		

		private bool isTurn;
		private List<Vector3> pastPos;
		private int currMP;
		private static int TOT_MP = 5;

		private List<GameObject> moveQuads;
		private int moveCounter;

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

				moveQuads = new List<GameObject> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				// Actions do on the character's turn.
				if (isTurn) {
					
						// Left Mouse Button: Move
						// Right Mouse Button: Attack;
						if (Input.GetMouseButton (0)) {
								move ();
						} else if (Input.GetMouseButton (1)) {
								attack ();
						}

				} 

				if (isTurn) {
						// Make the character green if it is active.
						MeshRenderer rend = GetComponentInChildren<MeshRenderer> ();	
						rend.material.color = Color.green;
				} else {
						// ...otherwise make it blue.
						MeshRenderer rend = GetComponentInChildren<MeshRenderer> ();	
						rend.material.color = Color.blue;
				}

				if (Input.GetKeyDown ("space")) {
						// Reset the character's movement.
						currMP = TOT_MP;
						pastPos.Clear ();
						pastPos.Add (position);

						int numMoved = moveQuads.Count;
						for (int i = 0; i < numMoved; i++) {
								Destroy (moveQuads [0]);
								moveQuads.RemoveAt (0);
						}
				}
		}

		private void move ()
		{				
				// Mouse location
				// Convert the mouse's screen coordinates to world coordinates.
				Vector3 mousePos = Input.mousePosition;
				mousePos.z = 1.0f;
				mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			
				// Convert them into int's.
				int mouseX = Mathf.FloorToInt (mousePos.x);
				int mouseY = Mathf.FloorToInt (mousePos.y);
			
				// If the mouse is on the screen and within 1 tile of the character...
				if (mouseX < tileMap.mapX && mouseX >= 0 && mouseY < tileMap.mapY && mouseY >= 0 &&
						((Math.Abs (mouseX - Mathf.FloorToInt (position.x)) == 1 && Math.Abs (mouseY - Mathf.FloorToInt (position.y)) == 0) ||
						(Math.Abs (mouseX - Mathf.FloorToInt (position.x)) == 0 && Math.Abs (mouseY - Mathf.FloorToInt (position.y)) == 1))) {

						// Find the MP cost of moving into that tile.
						Tile targetTile = tileMap.getTile (mouseX, mouseY);
						int mpCost = targetTile.getMpCost ();

						Tile currTile = tileMap.getTile (Mathf.FloorToInt (position.x), Mathf.FloorToInt (position.y));
				
						if (pastPos [0].x == mouseX && pastPos [0].y == mouseY) {
								
								// Set your current tile to null.
								currTile.setOccupant (null);
				
								// Move.							
								position.x = mouseX;
								position.y = mouseY;
								transform.position = position;
				
								pastPos.RemoveAt (0);
								
								Destroy (moveQuads [0]);
								moveQuads.RemoveAt (0);

								// Adjust your MP cost.	
								currMP += currTile.getMpCost ();
				
								// Become this tile's occupant.
								targetTile.setOccupant (this);
								Debug.Log (currMP);
				
								// else if the terrain is passable, there isn't an occupant, and you have enough MP...
						} else if (mpCost != 0 && 
								targetTile.getOccupant () == null &&
								currMP - mpCost >= 0) {
												
								// Set your current tile to null.
								currTile.setOccupant (null);
								
								pastPos.Insert (0, position);

								GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
								quad.renderer.material.color = Color.cyan;
								quad.transform.localScale = new Vector3 (.5f, .5f, 1f);
								quad.transform.position = new Vector3 (position.x + .5f, position.y + .5f, -1);
								
								moveQuads.Insert (0, quad);
								Debug.Log ("Num of Movement Quads: " + moveQuads.Count);

								// Move.						
								position.x = mouseX;
								position.y = mouseY;
								transform.position = position;

								// Adjust your MP cost.	
								currMP -= mpCost;
												
								// Become this tile's occupant.
								targetTile.setOccupant (this);
								Debug.Log ("MP " + currMP);
						}
				}
		}

		private void attack ()
		{	
	
	
		}

		// Allows the queue to handle the active character.
		public void setIsTurn (bool val)
		{
				isTurn = val;
		}
}
