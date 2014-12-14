using UnityEngine;
using System;
using System.Collections.Generic;

abstract public class Skill
{
		// The number of times the skill was upgraded.
		protected int timesUpgraded; 

		// Used for graphing the skillmap.
		protected List<int> path; 
		protected int id;

		// The user of this skill.
		protected Character user;

		// Whether or not this is an acquired skill.
		public bool isAcquired;

		// Whether or not the skill is currently being shown.
		public bool isShown;

		// The range of the skill and a list to show the range.
		protected int range;
		protected List<GameObject> rangeSquares;

		// The ap cost of the skill.
		protected int apCost;

		public Skill (Character user)
		{
				timesUpgraded = 0;
				isAcquired = false; 
				isShown = false;
				this.user = user;
				rangeSquares = new List<GameObject> ();
		}

		// All skill must have a way to be used.
		public virtual void use (Tile targetTile)
		{
				// Adjust your AP.
				user.loseAP (apCost);
		
				// End the user's movement.
				user.endMovement ();
		}

		// Shows the range of the skill.
		public virtual void showSkill ()
		{
				if (user.hasAP (apCost)) {
						// Different amount of squares are created depending on the range.
						switch (range) {
						case 0:
								rangeSquares.Add (createRangeSquare (0f, 0f));
								break;
						default:
								for (float x = 0f; x <= range; x++) {
										for (float y = 0f; y <= range; y++) {
												if (x + y <= range) {
														if (x + y == 0) {
																rangeSquares.Add (createRangeSquare (x, y));
														} else if (x == 0) {
																rangeSquares.Add (createRangeSquare (x, y));
																rangeSquares.Add (createRangeSquare (x, -y));
														} else if (y == 0) {
																rangeSquares.Add (createRangeSquare (x, y));
																rangeSquares.Add (createRangeSquare (-x, y));
														} else {
																rangeSquares.Add (createRangeSquare (x, y));
																rangeSquares.Add (createRangeSquare (x, -y));
																rangeSquares.Add (createRangeSquare (-x, y));
																rangeSquares.Add (createRangeSquare (-x, -y));
														}
							
												}
										}
								}
								break;
						}
						
						isShown = true;
				}
		}

		// Hides the range of the skill from being shown.
		public void hideSkill ()
		{		
				isShown = false;
				foreach (GameObject s in rangeSquares) {
						UnityEngine.Object.Destroy (s);
				}
				rangeSquares.Clear ();
		}

		// Upgrades the skill if it is leveled up.
		public void upgrade ()
		{
				if (timesUpgraded < 2) {
						timesUpgraded++;
				}
		}

		// Creates the quads for showing the range.
		// @ TODO
		// Create a prefab for this.
		protected GameObject createRangeSquare (float x, float y)
		{
				GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
				quad.renderer.material.color = Color.cyan;
				quad.transform.localScale = new Vector3 (.25f, .25f, 1f);
				quad.transform.position = new Vector3 (user.transform.position.x - x + .5f, user.transform.position.y - y + .5f, -2);
				return quad;
		}
		
		protected bool inRange (int r, float targetX, float targetY)
		{
				float userX = user.transform.position.x;
				float userY = user.transform.position.y;
				
				if (Mathf.Abs (userX - targetX) + Mathf.Abs (userY - targetY) <= range) {
						return true;
				} else {
						return false;
				}	
		}
}

abstract public class OffensiveSkill : Skill
{

		public OffensiveSkill (Character user) : base(user)
		{

		}

		public override void use (Tile targetTile)
		{
				base.use (targetTile);
				user.breakStealth ();
		}	
		
		public override void showSkill ()
		{
				base.showSkill ();
		}

}