using System;
using System.Collections.Generic;

abstract public class Skill
{
		protected int timesUpgraded;
		protected List<int> path;
		protected int id;
		protected int skillPosition;
		public bool isAcquired;
			

		abstract public void use ();

		public void upgrade ()
		{
				if (timesUpgraded < 2) {
						timesUpgraded++;
				}
		}
}