using System;
using System.Collections.Generic;
public class StealthBuff : Buff
{
		int timesUpgraded;
		
		public StealthBuff (bool isGood, int duration, Character user, int timesUpgraded) : base (isGood, duration, user)
		{
		
		}
	
		public override void calculate ()
		{
				if (timesUpgraded == 2) {
						owner.ignoresTerrain = true;
				}
				
				if (timesUpgraded == 1) {
						owner.pAtk *= 3 / 2;
						owner.mAtk *= 3 / 2;
				}
		}
		
		public void end ()
		{
				activeTime = 0;
				owner.ignoresTerrain = false;
				owner.pAtk *= 2 / 3;
				owner.mAtk *= 2 / 3;
		}
}


