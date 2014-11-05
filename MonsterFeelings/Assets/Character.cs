using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

		private TileMap tileMap;
		private Tile currentTile;

		public string name;
		
		// 100 xp = 1 level
		public int level;
		public int xp;

		// Orc, Goblin, Ogre, Troll
		public string charRace;	

		// Rogue, Warrior, Thief
		public string charClass;
		
		// Assassin, Archer, Blademaster, Barbarian, Guardian, 
		// Spellsword, Cleric, Invoker, Spellthief
		private string advanceClass;

		// Str, Dex, Con, Int, Wis
		public List<int> abilitites;

		// HP, Physical Attack, Physical Defense, Magical Attack, Magical Defense, Initiative	
		public List<int> stats;
	
		//public Item[] inventory;
		private static int INVENTORY_SIZE = 6;
		//public Armor armor;
		//public Weapon weapon;

		//public List<Skill> skills;
	
		public bool isAlly;

		private List<Tile> pastPos;

		private int currMP;
		private static int TOT_MP = 5;

		private List<GameObject> moveTracker;

		// Use this for initialization
		void Start ()
		{
				// Fine the TileMap.
				GameObject obj = GameObject.Find ("TileMap");
				tileMap = obj.GetComponent<TileMap> ();

				currentTile = tileMap.getTile (Mathf.FloorToInt (transform.position.x), Mathf.FloorToInt (transform.position.y));

				// Set the character to have full MP.		
				currMP = TOT_MP;

				// Record it's starting position as a position it has been in.
				pastPos = new List<Tile> ();
				pastPos.Add (currentTile);

				moveTracker = new List<GameObject> ();
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		public void endTurn ()
		{
				// Reset it.
				currMP = TOT_MP;
				pastPos.Clear ();
				pastPos.Add (currentTile);
		
				int numMoved = moveTracker.Count;
				for (int i = 0; i < numMoved; i++) {
						Destroy (moveTracker [0]);
						moveTracker.RemoveAt (0);
				}
		}

		public void move (Tile targetTile)
		{				
				if ((Mathf.Abs (targetTile.getPosition ().x - transform.position.x) == 1 && Mathf.Abs (targetTile.getPosition ().y - transform.position.y) == 0) ||
						(Mathf.Abs (targetTile.getPosition ().x - transform.position.x) == 0 && Mathf.Abs (targetTile.getPosition ().y - transform.position.y) == 1)) {

						if (pastPos [0].Equals (targetTile)) {
								
								// Remove you last position.
								pastPos.RemoveAt (0);

								// Remove the tracker at your last position.
								Destroy (moveTracker [0]);
								moveTracker.RemoveAt (0);
								
								// Adjust your MP cost.	
								currMP += currentTile.getMpCost ();
							
								// Make the movement.
								makeMovement (targetTile);
				
								// else if the terrain is passable, there isn't an occupant, and you have enough MP...
						} else if (targetTile.getMpCost () != 0 && 
								targetTile.getOccupant () == null &&
								currMP - targetTile.getMpCost () >= 0) {
							
								// Keep track of your last position.
								pastPos.Insert (0, currentTile);

								// Create and setup the movement tracking.
								// TODO: Have arrows that face a direction.
								GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
								quad.renderer.material.color = Color.cyan;
								quad.transform.localScale = new Vector3 (.5f, .5f, 1f);
								quad.transform.position = new Vector3 (transform.position.x + .5f, transform.position.y + .5f, -1);
								moveTracker.Insert (0, quad);
								
								// Adjust your MP cost.	
								currMP -= targetTile.getMpCost ();

								// Make the movement.
								makeMovement (targetTile);
						}
				}
		}

		private void makeMovement (Tile targetTile)
		{
				// Set your current tile to null.
				currentTile.setOccupant (null);	

				// Set currentTile and move
				transform.position = targetTile.getPosition ();
				currentTile = targetTile;

				// Become this tile's occupant.
				targetTile.setOccupant (this);
		}

		public void useSkill (int skillNum)
		{	
				Debug.Log (skillNum);
				// Use the skill at skillNum in skills
		}
}
