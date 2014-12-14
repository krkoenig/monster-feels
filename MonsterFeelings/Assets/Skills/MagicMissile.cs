using UnityEngine;
using System;
using System.Collections.Generic;

public class MagicMissile : OffensiveSkill
{
		// Create with all of the skill info.
		public MagicMissile (Character user) : base(user)
		{
				path = new List<int> () {3}; // do
				id = 3; // do
				range = 5;
				apCost = 2;
		icon = (Texture2D)Resources.Load ("Icons/Skill/mage/magic-missile");
		}
		
		public override void showSkill ()
		{
				base.showSkill ();
		}

		// Use the skill
		public override void use (Tile targetTile)
		{
				if (inRange (range, targetTile.getPosition ().x, targetTile.getPosition ().y) && 
						user.hasAP (apCost) &&
						targetTile.getOccupant () != null) {

						Character target = targetTile.getOccupant ();
						if (user.isAlly != target.isAlly) {
						
								int damage = 20 + (user.mAtk * 3 / 4) - target.mDef;
								target.dealDamage (damage);

								base.use (targetTile);
						}
				}

		}
}