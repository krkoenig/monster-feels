//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
public class ShieldBuff : Buff
{
		private Character caster;

		public ShieldBuff (bool isGood, int duration, Character owner, Character caster) : base (isGood, duration, owner)
		{
				this.caster = caster;
				name = "shield";
		}
	
		public override void calculate ()
		{
				owner.shield = caster.mAtk * 3 / 10 + 15;
		}
}