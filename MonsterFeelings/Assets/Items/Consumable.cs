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
namespace AssemblyCSharp
{
		abstract public class Consumable : Item 
		{
				public int APcost {
						get;
						set;
				}

				public int nbObjects {
						get;
						set;
				}

		public Consumable (): base ()
		{
			APcost = 0;
			nbObjects = 0;
		}

		public Consumable(int _APcost, int _nb) :base(_des){
			this.APcost = _APcost;
			this.nbObjects = _nb;
		}

		public void useOneItem(){
			this.nbObjects --;
		}
		}
}

