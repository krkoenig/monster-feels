﻿using UnityEngine;
using System;
using System.Collections.Generic;

abstract public class Skill
{
		protected int timesUpgraded;
		protected List<int> path;
		protected int id;
		protected int skillPosition;
		protected int range;
		protected Character user;
		public bool isAcquired;

		abstract public void use (Tile targetTile);

		public int getRange ()
		{
				return range;
		}

		public void upgrade ()
		{
				if (timesUpgraded < 2) {
						timesUpgraded++;
				}
		}
}