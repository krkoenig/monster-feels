using UnityEngine;
using System;
using System.Collections.Generic;

public class Slash : OffensiveSkill
{
		// Create with all of the skill info.
		public Slash (Character user) : base(user)
		{
				path = new List<int> () {3};
				id = 0;
				range = 1;
				apCost = 2;
		icon = (Texture2D)Resources.Load ("Icons/Skill/fighter/slash");
		}

		// Use the skill
		public override void use (Tile targetTile)
		{
				if (inRange (range, targetTile.getPosition ().x, targetTile.getPosition ().y) && 
						user.hasAP (apCost) &&
						targetTile.getOccupant () != null) {

						Character target = targetTile.getOccupant ();
						if (user.isAlly != target.isAlly) {
						
								int damage = 20 + user.pAtk - target.pDef;
								target.dealDamage (damage);

								base.use (targetTile);
						}
				}

		}
}