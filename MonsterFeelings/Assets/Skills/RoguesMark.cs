using UnityEngine;
using System;
using System.Collections.Generic;

public class RoguesMark : Skill
{
		// Create with all of the skill info.
		public RoguesMark (Character user) : base(user)
		{
				path = new List<int> () {3}; // do
				id = 4; // do
				updateRange ();
				updateAPCost ();
		icon = (Texture2D)Resources.Load ("Icons/Skill/rogue/rogues-mark");
		}
	
		// Updates the range to:
		// upgraded twice = 5
		// otherwise 1
		private void updateRange ()
		{
				if (timesUpgraded == 2) {
						range = 5;
				} else {
						range = 1;
				}

		}
		
		//  Updates the AP to:
		// upgraded once or twice = 1
		// else 2
		private void updateAPCost ()
		{
				if (timesUpgraded >= 1) {
						apCost = 1;
				} else {
						apCost = 2;
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
				updateAPCost ();
		
				if (inRange (range, targetTile.getPosition ().x, targetTile.getPosition ().y) && 
						user.hasAP (apCost) &&
						targetTile.getOccupant () != null) {
			
						Character target = targetTile.getOccupant ();
						if (user.isAlly != target.isAlly) {
				
								target.addBuff (new DmgBuff (false, 2, target));
				
								base.use (targetTile);
						}
				}
		
		}
}