using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class Buff
{
		// The number of rounds left for the buff.
		protected int activeTime;

		// true: is a buff
		// false: is a debuff
		protected bool isGood;

		// The owner of the buff.
		protected Character user;
	 
		public Buff (bool isGood, int duration, Character user)
		{
				this.isGood = isGood;
				activeTime = duration;
				this.user = user;
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

		abstract public void calculate ();
}
