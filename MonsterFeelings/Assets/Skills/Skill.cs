using UnityEngine;
using System;
using System.Collections.Generic;

abstract public class Skill
{
		protected int timesUpgraded;
		protected List<int> path;
		protected int id;
		protected int skillPosition;
		protected int range;
		protected Character user;
		public bool isAcquired;
		public bool isShown;
		protected List<GameObject> rangeSquares;

		abstract public void use (Tile targetTile);

		abstract public void showSkill ();

		public void hideSkill ()
		{		
				isShown = false;
				foreach (GameObject s in rangeSquares) {
						UnityEngine.Object.Destroy (s);
				}
				rangeSquares.Clear ();
		}

		public int getRange ()
		{
				return range;
		}

		public void upgrade ()
		{
				if (timesUpgraded < 2) {
						timesUpgraded++;
				}
		}

		protected GameObject createRangeSquare (float x, float y)
		{
				GameObject quad = GameObject.CreatePrimitive (PrimitiveType.Quad);
				quad.renderer.material.color = Color.cyan;
				quad.transform.localScale = new Vector3 (.25f, .25f, 1f);
				quad.transform.position = new Vector3 (user.transform.position.x - x + .5f, user.transform.position.y - y + .5f, -2);
				return quad;
		}
}