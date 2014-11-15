using UnityEngine;
using System;
using System.Collections.Generic;

public class Slash : Skill
{
		public Slash (Character user)
		{
				timesUpgraded = 0;
				path = new List<int> () {3};
				id = 0;
				skillPosition = 1;
				range = 1;
				isAcquired = false; 
				isShown = false;
				this.user = user;
				rangeSquares = new List<GameObject> ();
		}

		public override void use (Tile targetTile)
		{
				float userX = user.transform.position.x;
				float userY = user.transform.position.y;
				float targetX = targetTile.getPosition ().x;
				float targetY = targetTile.getPosition ().y;

				if (userX + range == targetX && userY == targetY ||
						userY + range == targetY && userX == targetX ||
						userX - range == targetX && userY == targetY ||
						userY - range == targetY && userX == targetX) {
						Debug.Log ("Slash " + timesUpgraded);
				}
		}

		public override void showSkill ()
		{
				rangeSquares.Add (createRangeSquare (1f, 0f));
				rangeSquares.Add (createRangeSquare (-1f, 0f));
				rangeSquares.Add (createRangeSquare (0f, -1f));
				rangeSquares.Add (createRangeSquare (0f, 1f));
				isShown = true;
		}
}