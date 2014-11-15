using UnityEngine;
using System;
using System.Collections.Generic;

public class PowerAttack : Skill
{
		public PowerAttack (Character user)
		{
				timesUpgraded = 0;
				path = new List<int> () {2};
				id = 1;
				skillPosition = 2;
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
						Debug.Log ("Power Attack " + timesUpgraded);
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