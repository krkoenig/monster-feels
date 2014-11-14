using UnityEngine;
using System;
using System.Collections.Generic;

public class Slash : Skill
{
		public Slash (Character user)
		{
				timesUpgraded = 0;
				path = new List<int> () {1};
				id = 0;
				skillPosition = 1;
				range = 1;
				isAcquired = false; 
				this.user = user;
		}

		public override void use (Tile targetTile)
		{
				float userX = user.transform.position.x;
				float userY = user.transform.position.y;
				float targetX = targetTile.getPosition ().x;
				float targetY = targetTile.getPosition ().y;

				if (userX + range == targetX && userY == targetY ||
						userY + range == targetY && userX == targetX ||
						userX - range == targetX && userY == targetY ||
						userY - range == targetY && userX == targetX) {
						Debug.Log ("Slash " + timesUpgraded);
				}
		}
}