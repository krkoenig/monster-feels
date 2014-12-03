using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

		// The character's name.
		public string name;
		
		// The level and experience of the character.
		public int level;
		public int xp;

		// Orc, Goblin, Ogre, Troll
		public string charRace;	

		// Rogue, Warrior, Thief
		public string charClass;
		
		// Assassin, Archer, Blademaster, Barbarian, Guardian, 
		// Spellsword, Cleric, Invoker, Spellthief
		private string advanceClass;

		// abilities
		public int strength;
		public int dexterity;
		public int constitution;
		public int intelligence;
		public int wisdom;
		public int vitality;

		//stats
		public int hp;
		public int pAtk;
		public int pDef;
		public int mAtk;
		public int mDef;
		public int init;
		private int ap;

		// The current AP and MP of the character.
		private int currAP;
		private int currMP;
		private static int TOT_MP = 5;
	
		//public Item[] inventory;
		private static int INVENTORY_SIZE = 6;
		//public Armor armor;
		//public Weapon weapon;

		// Information regarding the skills oh the character. 
		public string startSkills;
		private SkillMap skillMap;
		private List<Skill> acquiredSkills;

		// True if on the player's team.
		public bool isAlly;

		// Is used for keeping track of where the player has moved during a turn.
		private List<Tile> pastPos;
		private List<GameObject> moveTracker;

		// The position of the skill in the skillMap being shown. A value of -1 means no skill is shown.
		private int shownSkill;
		public int ShownSkill {
				get { return shownSkill;}
		}

		// List of Buffs
		private List<Buff> buffs;
		
		// Checking for whether the character ignores terrain
		public bool isStealthed;

		private TileMap tileMap;
		private Tile currentTile;

		// Use this for initialization
		void Start ()
		{
				// Find the TileMap.
				GameObject obj = GameObject.Find ("TileMap");
				tileMap = obj.GetComponent<TileMap> ();
	
				// Used for movement.
				currentTile = tileMap.getTile (Mathf.FloorToInt (transform.position.x), Mathf.FloorToInt (transform.position.y));

				calculateStats ();
				hp = 5 * vitality;

				// Set the character to have full MP and AP.		
				currMP = TOT_MP;
				currAP = ap;

				// Start the tracker for position the character has moved each turn.
				pastPos = new List<Tile> ();
				pastPos.Add (currentTile);
				moveTracker = new List<GameObject> ();

				// Build the character's skillmap.
				skillMap = new SkillMap (startSkills, this);
				acquiredSkills = skillMap.getAcquiredSkills ();

				// Create the Buff list.
				buffs = new List<Buff> ();

				// No skill is being shown at the start.
				shownSkill = -1;
				
				// No character starts ignoring terrain.
				isStealthed = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				// @ TODO
				// Check for levelup.
		}

		// Call whenever the turn is ending.
		public void endTurn ()
		{
				// Just in case a skill wasn't used, endMovement.
				endMovement ();

				// Reset MP and AP.
				currMP = TOT_MP;
				currAP = ap;

				// In case skills are being shown when the turn ends,
				// hide them.
				hideAllSkills ();	

				// Recalculate all stats
				calculateStats ();
		
				// Decrement all buffs
				// If a buff ends, make it inactive and delete it.
				for (int i = buffs.Count - 1; i >= 0; i--) {
						Buff b = buffs [i];
						if (!b.decrement ()) {
								buffs.RemoveAt (i);
						} else {
								b.calculate ();
						}
				}
		}

		// Call when a skill is used or the turn is over.
		public void endMovement ()
		{
				// Stop the player from being able to move more.
				currMP = 0;
				
				// Stop the player from being able to move to a
				// previous space.
				pastPos.Clear ();
				pastPos.Add (currentTile);

				// Remove the trail of movement for the player.
				foreach (GameObject square in moveTracker) {
						Destroy (square);
				}
				moveTracker.Clear ();
		}

		// Calculates all the stats of the character.
		private void calculateStats ()
		{
				pAtk = strength;
				pDef = constitution;
				mAtk = intelligence;
				mDef = wisdom;
				init = dexterity;
				ap = 2 + (dexterity / 5);
				isStealthed = false;
		}

		// Handle all movement.
		// Call when the character is going to move.
		public void move (Tile targetTile)
		{
				// Hide any skills being currently shown.
				hideAllSkills ();

				// Only move left, right, up, or down.
				if ((Mathf.Abs (targetTile.getPosition ().x - transform.position.x) == 1 && Mathf.Abs (targetTile.getPosition ().y - transform.position.y) == 0) ||
						(Mathf.Abs (targetTile.getPosition ().x - transform.position.x) == 0 && Mathf.Abs (targetTile.getPosition ().y - transform.position.y) == 1)) {

						// If moving into an old position...
						if (pastPos [0].Equals (targetTile)) {
								
								// Remove you last position.
								pastPos.RemoveAt (0);

								// Remove the tracker at your last position.
								Destroy (moveTracker [0]);
								moveTracker.RemoveAt (0);
								
								if (isStealthed) {
										currMP += 1;
								} else {
										// Adjust your MP cost.	
										currMP += currentTile.getMpCost ();
								}
							
								// Make the movement.
								makeMovement (targetTile);
				
								// else if the terrain is passable, there isn't an occupant, and you have enough MP...
						} else if (targetTile.getMpCost () != 0 && 
								targetTile.getOccupant () == null &&
								((isStealthed && currMP - 1 >= 0) || (!isStealthed && currMP - targetTile.getMpCost () >= 0))) {
							
								// Keep track of your last position.
								pastPos.Insert (0, currentTile);

								// Create and setup the movement tracking.
								// TODO: Have arrows that face a direction.
								GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
								quad.renderer.material.color = Color.cyan;
								quad.transform.localScale = new Vector3 (.5f, .5f, 1f);
								quad.transform.position = new Vector3 (transform.position.x + .5f, transform.position.y + .5f, -1);
								moveTracker.Insert (0, quad);
								
								if (isStealthed) {
										currMP -= 1;
								} else {
										// Adjust your MP cost.	
										currMP -= targetTile.getMpCost ();
								}

								// Make the movement.
								makeMovement (targetTile);
						}
				}
		}

		// Actually move the player.
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

		// Uses skillNum skill on targetTile.
		public void useSkill (int skillNum, Tile targetTile)
		{	
				// Remove vision of the range.
				hideAllSkills ();

				skillMap.getAcquiredSkills () [skillNum].use (targetTile);
		}

		// Shows the called skill.
		public void showSkill (int skillNum)
		{
				// Hide all skills.
				hideAllSkills ();

				// Set that a skill is being shown.
				shownSkill = skillNum;

				// Show it.
				skillMap.getAcquiredSkills () [skillNum].showSkill ();
		}
		
		// Hides all skills being shown.
		private void hideAllSkills ()
		{
				// Set that there is no skill ebing shown.
				shownSkill = -1;
				
				// Hide all of the player's skills.
				foreach (Skill s in acquiredSkills) {
						if (s.isShown) {
								s.hideSkill ();
						}
				}
		}

		// Changes the HP of the character and checks
		// to make sure the character isn't dead.
		public void dealDamage (int damage)
		{
				foreach (Buff b in buffs) {
						if (b is DmgBuff) {
								DmgBuff d = (DmgBuff)b;
								damage = d.damageAdjustment (damage);
						}
				}
		
				hp -= damage;
				checkDead ();
		}
		
		// Removes stealth from the user.
		public void breakStealth ()
		{
				calculateStats ();	
		
				// Recalculate all buffs, but remove stealth.
				for (int i = buffs.Count - 1; i >= 0; i--) {
						Buff b = buffs [i];
						if (b is StealthBuff) {
								buffs.RemoveAt (i);
						} else {
								b.calculate ();
						}
				}
		}
	
		// Checks to see if the character has 0 or less life
		// and handles all dieing information.
		private void checkDead ()
		{
				Debug.Log (hp + " HP");
		
				if (hp <= 0) {
						Debug.Log ("DEAD!");
				}
		
		}
	
		// Subtracts the cost of the skill.
		public void loseAP (int changeAP)
		{
				currAP -= changeAP;
		}
		
		// checks if there is enough AP for the action
		public bool hasAP (int cost)
		{
				if (currAP >= cost) {
						return true;
				} else {
						return false;
				}
		}

		// Adds a buff to the buff list.
		public void addBuff (Buff buff)
		{
				bool buffExists = false;
				Type buffType = buff.GetType ();
				foreach (Buff b in buffs) {
						if (b.GetType () == buffType) {
								b.addTime (buff);
								buffExists = true;
						}
				}

				if (!buffExists) {
						buffs.Add (buff);
						calculateStats ();
						foreach (Buff b in buffs) {
								b.calculate ();
						}
				}
		}
}
