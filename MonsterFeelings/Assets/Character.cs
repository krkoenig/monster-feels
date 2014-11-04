using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

		private TileMap tileMap;
		private Tile currentTile;
		public Vector3 position;

		//private string name;
		
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

		private List<Tile> pastPos;
		private int currMP;
		private static int TOT_MP = 5;

		private List<GameObject> moveQuads;

		// Use this for initialization
		void Start ()
		{
				// Fine the TileMap.
				GameObject obj = GameObject.Find ("TileMap");
				tileMap = obj.GetComponent<TileMap> ();

				// Set the character to its runtime position.
				transform.position = position;
				currentTile = tileMap.getTile (Mathf.FloorToInt (position.x), Mathf.FloorToInt (position.y));

				// Set the character to have full MP.		
				currMP = TOT_MP;

				// Record it's starting position as a position it has been in.
				pastPos = new List<Tile> ();
				pastPos.Add (currentTile);

				moveQuads = new List<GameObject> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKeyDown ("space")) {
						// Reset the character's movement.
						currMP = TOT_MP;
						pastPos.Clear ();
						pastPos.Add (currentTile);

						int numMoved = moveQuads.Count;
						for (int i = 0; i < numMoved; i++) {
								Destroy (moveQuads [0]);
								moveQuads.RemoveAt (0);
						}
				}
		}

		public void move (Tile targetTile)
		{				
				// If the targetTile within 1 tile of the character...
				if (isNextTo (targetTile)) {

						int mpCost = targetTile.getMpCost ();

						if (pastPos [0].Equals (targetTile)) {
								
								pastPos.RemoveAt (0);
				
								Destroy (moveQuads [0]);
								moveQuads.RemoveAt (0);
								// Adjust your MP cost.	
								currMP += currentTile.getMpCost ();
								// Set your current tile to null.
								currentTile.setOccupant (null);
				
				
	
								// Move.							
								position = targetTile.getPosition ();
								transform.position = position;
								currentTile = targetTile;
								// Become this tile's occupant.
								targetTile.setOccupant (this);
				
								// else if the terrain is passable, there isn't an occupant, and you have enough MP...
						} else if (mpCost != 0 && 
								targetTile.getOccupant () == null &&
								currMP - mpCost >= 0) {
							
								pastPos.Insert (0, currentTile);

								GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
								quad.renderer.material.color = Color.cyan;
								quad.transform.localScale = new Vector3 (.5f, .5f, 1f);
								quad.transform.position = new Vector3 (position.x + .5f, position.y + .5f, -1);
								
								moveQuads.Insert (0, quad);
								// Adjust your MP cost.	
								currMP -= mpCost;


								// Set your current tile to null.
								currentTile.setOccupant (null);	
								// Move.						
								position = targetTile.getPosition ();
								transform.position = position;
								currentTile = targetTile;
								// Become this tile's occupant.
								targetTile.setOccupant (this);


								


												

						}
				}
		}

		private bool isNextTo (Tile targetTile)
		{
				int targetTileX = Mathf.FloorToInt (targetTile.getPosition ().x);
				int targetTileY = Mathf.FloorToInt (targetTile.getPosition ().y);
				
				if ((Math.Abs (targetTileX - Mathf.FloorToInt (position.x)) == 1 && Math.Abs (targetTileY - Mathf.FloorToInt (position.y)) == 0) ||
						(Math.Abs (targetTileX - Mathf.FloorToInt (position.x)) == 0 && Math.Abs (targetTileY - Mathf.FloorToInt (position.y)) == 1)) {
						return true;
				} 
				return false;
		}

		private void attack ()
		{	
	
	
		}
}
