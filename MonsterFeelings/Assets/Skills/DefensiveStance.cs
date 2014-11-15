using UnityEngine;
using System;
using System.Collections.Generic;

public class DefensiveStance : Skill
{
		public DefensiveStance (Character user)
		{
				timesUpgraded = 0;
				path = new List<int> () {4,5};
				id = 2;
				skillPosition = 3;
				range = 0;
				isShown = false;
				isAcquired = false; 
				this.user = user;
				rangeSquares = new List<GameObject> ();

		}
	
		public override void use (Tile targetTile)
		{
				float userX = user.transform.position.x;
				float userY = user.transform.position.y;
				float targetX = targetTile.getPosition ().x;
				float targetY = targetTile.getPosition ().y;
		
				if (userX == targetX && userY == targetY) {
						Debug.Log ("DefensiveStance " + timesUpgraded);
				}
		}

		public override void showSkill ()
		{
				rangeSquares.Add (createRangeSquare (0f, 0f));
				isShown = true;
		}
}