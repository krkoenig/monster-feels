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
public class PDefBuff : Buff
{
		public PDefBuff (bool isGood, int duration, Character owner) : base (isGood, duration, owner)
		{

		}

		public override void calculate ()
		{
				if (isGood) {
						owner.pDef += owner.pDef / 2;
				} else {
						if (owner.pDef < 10) {
								owner.pDef = 0;
						} else {
								owner.pDef -= 10;
						}
				}
		}
}


