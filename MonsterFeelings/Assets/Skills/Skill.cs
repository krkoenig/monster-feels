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

		// All skill must have a way to be used.
		virtual public void use (Tile targetTile)
		{
				// Adjust your AP.
				user.changeAP (apCost);
		
				// End the user's movement.
				user.endMovement ();
		}

		// Shows the range of the skill.
		public void showSkill ()
		{
				// Different amount of squares are created depending on the range.
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
				}

				isShown = true;
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
}