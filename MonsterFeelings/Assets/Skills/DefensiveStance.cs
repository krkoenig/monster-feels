using UnityEngine;
using System;
using System.Collections.Generic;

public class DefensiveStance : Skill
{
		// Create with all of the skill info.
		public DefensiveStance (Character user)
		{
				timesUpgraded = 0;
				path = new List<int> () {4,5};
				id = 2;
				range = 0;
				isShown = false;
				isAcquired = false; 
				this.user = user;
				rangeSquares = new List<GameObject> ();
				apCost = 4;

		}

		// Use the skill
		public override void use (Tile targetTile)
		{
				float userX = user.transform.position.x;
				float userY = user.transform.position.y;
				float targetX = targetTile.getPosition ().x;
				float targetY = targetTile.getPosition ().y;
		
				if (userX == targetX && userY == targetY && 
						user.getAP () >= apCost) {
						// Add buff later
						//user.addBuff(new PDefBuff(1,3));
						
						base.use (targetTile);
				}
		}
}