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

		// HP, Physical Attack, Physical Defense, Magical Attack, Magical Defense, Initiative, AP	
		public List<int> stats;

		private int currAP;
	
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

		public int shownSkill;

		// Use this for initialization
		void Start ()
		{
				// Fine the TileMap.
				GameObject obj = GameObject.Find ("TileMap");
				tileMap = obj.GetComponent<TileMap> ();

				currentTile = tileMap.getTile (Mathf.FloorToInt (transform.position.x), Mathf.FloorToInt (transform.position.y));

				// Set the character to have full MP.		
				currMP = TOT_MP;

				currAP = stats [6];

				// Record it's starting position as a position it has been in.
				pastPos = new List<Tile> ();
				pastPos.Add (currentTile);

				moveTracker = new List<GameObject> ();

				skillMap = new SkillMap (acquiredSkills, this);

				shownSkill = -1;
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
				currAP = stats [6];

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
				hideAllSkills ();
				shownSkill = skillNum;
				skillMap.getAcquiredSkills () [skillNum].showSkill ();
		}
		
		private void hideAllSkills ()
		{
				shownSkill = -1;
				
				List<Skill> acquired = skillMap.getAcquiredSkills ();
				foreach (Skill s in acquired) {
						if (s.isShown) {
								s.hideSkill ();
						}
				}
		}

		public int getShownSkill ()
		{
				return shownSkill;
		}

		public void checkDead ()
		{
				Debug.Log (stats [0]);

				if (stats [0] <= 0) {
						Debug.Log ("DEAD!");
						Destroy (this);

				}
				
		}

		public int getHP ()
		{
				return stats [0];
		}

		public void changeHP (int changeHP)
		{
				stats [0] -= changeHP;
		}

		public int getPAtk ()
		{
				return stats [1];
		}

		public int getMAtk ()
		{
				return stats [3];
		}

		public int getPDef ()
		{
				return stats [2];
		}

		public int getMDef ()
		{
				return stats [4];
		}

		public int getInit ()
		{
				return stats [5];
		}

		public int getAP ()
		{
				return currAP;
		}

		public void changeAP (int changeAP)
		{
				currAP -= changeAP;
		}
}
