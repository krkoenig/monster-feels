using UnityEngine;
using System;
using System.Collections.Generic;

public class Assault : OffensiveSkill
{
		// Create with all of the skill info.
		public Assault (Character user) : base(user)
		{
				path = new List<int> () {3}; // do
				id = 3; // do
				updateRange ();
				apCost = 2;
		}
		
		// Updates the range to:
		// Bow -> range 5
		// Knife -> range 1
		private void updateRange ()
		{
				range = 5;
		}
		
		public override void showSkill ()
		{
				updateRange ();
				base.showSkill ();
		}

		// Use the skill
		public override void use (Tile targetTile)
		{
				updateRange ();

				if (inRange (range, targetTile.getPosition ().x, targetTile.getPosition ().y) && 
						user.hasAP (apCost) &&
						targetTile.getOccupant () != null) {

						Character target = targetTile.getOccupant ();
						if (user.isAlly != target.isAlly) {
						
								int damage = 25 + (user.pAtk / 2) - target.pDef;
								target.dealDamage (damage);

								base.use (targetTile);
						}
				}

		}
}