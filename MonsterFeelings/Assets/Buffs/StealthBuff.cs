using System;
using System.Collections.Generic;
public class StealthBuff : Buff
{
		private int timesUpgraded;
		
		public StealthBuff (bool isGood, int duration, Character user, int timesUpgraded) : base (isGood, duration, user)
		{
				this.timesUpgraded = timesUpgraded;
		}
	
		public override void calculate ()
		{
				if (timesUpgraded == 2) {
						owner.isStealthed = true;
				}
				
				if (timesUpgraded >= 1) {
						owner.pAtk += owner.pAtk / 2;
						owner.mAtk += owner.mAtk / 2;
				}
		}
}


