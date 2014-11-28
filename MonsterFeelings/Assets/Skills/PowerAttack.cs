using UnityEngine;
using System;
using System.Collections.Generic;

public class PowerAttack : Skill
{

		// Create with all of the skill info.
		public PowerAttack (Character user) : base (user)
		{
				path = new List<int> () {2};
				id = 1;
				range = 1;
				apCost = 4;

		}

		// Use the skill
		public override void use (Tile targetTile)
		{
				// Check if the click was in range.
				if (inRange (range, targetTile.getPosition ().x, targetTile.getPosition ().y) && 
						user.hasAP (apCost) &&
						targetTile.getOccupant () != null) {
						
						Character target = targetTile.getOccupant ();
						if (user.isAlly != target.isAlly) {
								// Based on the skill level, do different things.
								switch (timesUpgraded) {
								case 0:
										user.addBuff (new PDefBuff (false, 2, user));
										break;
								case 1:
										user.addBuff (new PDefBuff (false, 2, user));
										target.addBuff (new PDefBuff (false, 2, user));
										break;
								case 2:
										target.addBuff (new PDefBuff (false, 2, user));
										break;
								}
						
								// Calculate and deal damage.
								int damage = 10 + (user.pAtk * 3) - target.pDef;
								target.dealDamage (damage);

								base.use (targetTile);
						}
				}
		}
}