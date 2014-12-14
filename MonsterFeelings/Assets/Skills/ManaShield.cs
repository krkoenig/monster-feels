using UnityEngine;
using System;
using System.Collections.Generic;

public class ManaShield : Skill
{
		// Create with all of the skill info.
		public ManaShield (Character user) : base(user)
		{
				path = new List<int> () {3}; // do
				id = 4; // do
				apCost = 3;
				updateRange ();
		icon = (Texture2D)Resources.Load ("Icons/Skill/mage/mana-shield");
		}
	
		// Updates the range to:
		// upgraded twice = 5
		// otherwise 1
		private void updateRange ()
		{
				if (timesUpgraded == 2) {
						range = 5;
				} else {
						range = 0;
				}

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
						if (user.isAlly == target.isAlly) {
				
								target.addBuff (new ShieldBuff (true, 3, target, user));
				
								base.use (targetTile);
						}
				}
		
		}
}