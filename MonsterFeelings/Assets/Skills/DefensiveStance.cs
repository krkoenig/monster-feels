using UnityEngine;
using System;
using System.Collections.Generic;

public class DefensiveStance : Skill
{
		// Create with all of the skill info.
		public DefensiveStance (Character user) : base(user)
		{
				path = new List<int> () {4,5};
				id = 2;
				range = 0;
				apCost = 4;
		}

		// Use the skill
		public override void use (Tile targetTile)
		{
				if (inRange (range, targetTile.getPosition ().x, targetTile.getPosition ().y) && 
						user.hasAP (apCost)) {

						user.addBuff (new PDefBuff (true, 3, user));
						
						base.use (targetTile);
				}
		}
}