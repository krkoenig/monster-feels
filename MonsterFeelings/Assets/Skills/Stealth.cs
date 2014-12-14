using UnityEngine;
using System;
using System.Collections.Generic;

public class Stealth : Skill
{
	// Create with all of the skill info.
	public Stealth (Character user) : base(user)
	{
		path = new List<int> () {3}; // do
		id = 5; // do
		range = 0;
		apCost = 4;
		icon = (Texture2D)Resources.Load ("Icons/Skill/rogue/stealth");
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
							
			user.addBuff (new StealthBuff (false, 5, user, timesUpgraded));
				
			base.use (targetTile);
		}
		
	}
}