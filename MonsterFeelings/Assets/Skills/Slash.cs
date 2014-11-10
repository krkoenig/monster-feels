using UnityEngine;
using System;
using System.Collections.Generic;

public class Slash : Skill
{
		public Slash ()
		{
				timesUpgraded = 0;
				path = new List<int> () {1};
				id = 0;
				skillPosition = 1;
				isAcquired = false; 
		}

		public override void use ()
		{
				Debug.Log ("Slash " + timesUpgraded);
		}
}