using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class Buff
{
		// The number of rounds left for the buff.
		public int activeTime;

		// true: is a buff
		// false: is a debuff
		protected bool isGood;

		protected string name;

		// The owner of the buff.
		protected Character owner;
	 
		public Buff (bool isGood, int duration, Character owner)
		{
				this.isGood = isGood;
				activeTime = duration;
				this.owner = owner;
		}

		// Decrements the buff. To be called at the end of each character turn.
		// returns false if the buff has ended.
		public bool decrement ()
		{
				activeTime--;
				if (activeTime == 0) {
						return false;
				}
				return true;
		}

		public void addTime (Buff newBuff)
		{
				if (newBuff.isGood == isGood) {
						activeTime += newBuff.activeTime;
				} else {
						activeTime -= newBuff.activeTime;
						if (activeTime < 0) {
								activeTime = Mathf.Abs (activeTime);
								isGood = !isGood;
						}
				}
		}

		public string getPath ()
		{
				string path = "Icons/16x16/";
				if (isGood) {
						path += "Buff/";
				} else {
						path += "Debuff/";
				}
				path += name;
				return path;
		}

		abstract public void calculate ();
}
