using UnityEngine;
using System;
using System.Collections.Generic;

public class ArcaneBrilliance : OffensiveSkill
{
		// Create with all of the skill info.
		public ArcaneBrilliance (Character user) : base(user)
		{
				path = new List<int> () {3}; // do
				id = 4; // do
				apCost = 4;
				range = 0;
				icon = (Texture2D)Resources.Load ("Icons/Skill/mage/Arcane-Brilliance");
		}
	
		// Use the skill
		public override void use (Tile targetTile)
		{
				if (inRange (range, targetTile.getPosition ().x, targetTile.getPosition ().y) && 
						user.hasAP (apCost)) {

				
						
						
						if (timesUpgraded == 2) {
								hitEnemies ();
						}

						if (timesUpgraded >= 1) {
								user.extraAP = 1;
						}

						user.addBuff (new MAtkBuff (true, 3, user));
						

						base.use (targetTile);
				}
		
		}

		private void hitEnemies ()
		{
				GameObject obj = GameObject.Find ("TileMap");
				TileMap tileMap = obj.GetComponent<TileMap> ();

				Vector3 pos = user.getPosition ();
				
				List<Character> nearbyEnemies = new List<Character> ();

				for (int i = 1; i <= 5; i++) {
						for (int x = -i; x <= i; x++) {
								int y = i - Math.Abs (x);
								if (x + (int)pos.x >= 0 && x + (int)pos.x < tileMap.mapX) {

										if (y + (int)pos.y < tileMap.mapY) {
												Character c = tileMap.getTile (x + (int)pos.x, y + (int)pos.y).getOccupant ();
												if (c != null) {
														if (c.isAlly != user.isAlly) {
																nearbyEnemies.Add (c);
														}
												}
										}

										if (-y + (int)pos.y >= 0) {
												Character c = tileMap.getTile (x + (int)pos.x, -y + (int)pos.y).getOccupant ();
												if (c != null) {
														if (c.isAlly != user.isAlly) {
																nearbyEnemies.Add (c);
														}
												}
										}
								}
						}
				}


				for (int i = 0; i < 3; i++) {
						if (i < nearbyEnemies.Count) {
								int damage = 10 + (user.mAtk / 2) - nearbyEnemies [i].mDef;
								nearbyEnemies [i].dealDamage (damage);
						}
				}
		}
}