using UnityEngine;
using System;
using System.Collections.Generic;

public class PowerAttack : Skill
{

		// Create with all of the skill info.
		public PowerAttack (Character user)
		{
				timesUpgraded = 0;
				path = new List<int> () {2};
				id = 1;
				range = 1;
				isAcquired = false;
				isShown = false;
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

				// Check if the click was in range.
				if ((userX + range == targetX && userY == targetY ||
						userY + range == targetY && userX == targetX ||
						userX - range == targetX && userY == targetY ||
						userY - range == targetY && userX == targetX) && 
						user.ap >= apCost &&
						targetTile.getOccupant () != null) {
						
						Character target = targetTile.getOccupant ();
						if (user.isAlly != target.isAlly) {
								// Based on the skill level, do different things.
								switch (timesUpgraded) {
								case 0:
				// Later will add debuff.
				//user.addBuff(new PDefBuff(false,1));
										break;
								case 1:
				// Later will add debuffs.
				//user.addBuff(new PDefBuff(false,1));
				//target.addBuff(new PDefBuff(false,1));
										break;
								case 2:

				// Later will add debuff.
				//target.addBuff(new PDefBuff(false,1));
										break;
								}
						
								// Calculate and deal damage.
								int damage = 10 + (user.pAtk * 3) - target.pDef;
								target.changeHP (damage);

								base.use (targetTile);
						}
				}
		}
}