using UnityEngine;
using System;
using System.Collections.Generic;

public class Slash : Skill
{
		// Create with all of the skill info.
		public Slash (Character user)
		{
				timesUpgraded = 0;
				path = new List<int> () {3};
				id = 0;
				range = 1;
				isAcquired = false; 
				isShown = false;
				this.user = user;
				rangeSquares = new List<GameObject> ();
				apCost = 2;
		}

		// Use the skill
		public override void use (Tile targetTile)
		{
				float userX = user.transform.position.x;
				float userY = user.transform.position.y;
				
				float targetX = targetTile.getPosition ().x;
				float targetY = targetTile.getPosition ().y;

				if ((userX + range == targetX && userY == targetY ||
						userY + range == targetY && userX == targetX ||
						userX - range == targetX && userY == targetY ||
						userY - range == targetY && userX == targetX) && 
						user.getAP () >= apCost &&
						targetTile.getOccupant () != null) {

						Character target = targetTile.getOccupant ();
						if (user.isAlly != target.isAlly) {
						
								int damage = 20 + user.getPAtk () - target.getPDef ();
								target.changeHP (damage);

								base.use (targetTile);
						}
				}

		}
}