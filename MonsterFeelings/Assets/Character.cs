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

		public string acquiredSkills;
		private SkillMap skillMap;
	
		public bool isAlly;

		private List<Tile> pastPos;

		private int currMP;
		private static int TOT_MP = 5;

		private List<GameObject> moveTracker;
		private List<GameObject> rangeSquares;
		private int skillShown;

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

				skillMap = new SkillMap (acquiredSkills, this);
			
				rangeSquares = new List<GameObject> ();

				skillShown = -1;
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

				foreach (GameObject square in moveTracker) {
						Destroy (square);
				}

				moveTracker.Clear ();

				hideAllSkills ();	
		}

		public void move (Tile targetTile)
		{
				hideAllSkills ();
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

		public void useSkill (int skillNum, Tile targetTile)
		{	
				hideAllSkills ();
				skillMap.getAcquiredSkills () [skillNum].use (targetTile);
		}

		public void showSkill (int skillNum)
		{
				if (skillShown != skillNum) {
						hideAllSkills ();
				} else {
						return;
				}

				skillShown = skillNum;
				int range = skillMap.getAcquiredSkills () [skillNum].getRange ();

				switch (range) {
				case 0:
						rangeSquares.Add (createRangeSquare (0f, 0f));
						break;
				case 1:
						rangeSquares.Add (createRangeSquare (1f, 0f));
						rangeSquares.Add (createRangeSquare (-1f, 0f));
						rangeSquares.Add (createRangeSquare (0f, -1f));
						rangeSquares.Add (createRangeSquare (0f, 1f));
						break;
				case 2:
						rangeSquares.Add (createRangeSquare (1f, 0f));
						rangeSquares.Add (createRangeSquare (-1f, 0f));
						rangeSquares.Add (createRangeSquare (0f, -1f));
						rangeSquares.Add (createRangeSquare (0f, 1f));
						rangeSquares.Add (createRangeSquare (2f, 0f));
						rangeSquares.Add (createRangeSquare (-2f, 0f));
						rangeSquares.Add (createRangeSquare (0f, 2f));
						rangeSquares.Add (createRangeSquare (0f, -2f));
						rangeSquares.Add (createRangeSquare (1f, 1f));
						rangeSquares.Add (createRangeSquare (1f, -1f));
						rangeSquares.Add (createRangeSquare (-1f, 1f));
						rangeSquares.Add (createRangeSquare (-1f, -1f));
						break;
				}
				
		}

		private GameObject createRangeSquare (float x, float y)
		{
				GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
				quad.renderer.material.color = Color.cyan;
				quad.transform.localScale = new Vector3 (.25f, .25f, 1f);
				quad.transform.position = new Vector3 (transform.position.x - x + .5f, transform.position.y - y + .5f, -2);
				return quad;
		}

		private void hideAllSkills ()
		{
				skillShown = -1;
				foreach (GameObject square in rangeSquares) {
						Destroy (square);
				}
				rangeSquares.Clear ();
		}

		public int isSkillShown ()
		{
				return skillShown;
		}
}
