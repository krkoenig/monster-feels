using UnityEngine;
using System.Collections;

abstract public class Skill
{
		protected int numUpgraded;
		protected int path;
		protected int id;
		protected int skillPosition;
		public bool isAcquired;
			

		abstract public void use ();
}