using UnityEngine;
using System.Collections;

public class Tile
{
		private int terrain;
		private int mpCost;
		private Character occupant;
		private Vector3 position;

		public Tile (Vector3 position, int terrain)
		{
				// Initialze values.
				this.position = position;	
				this.terrain = terrain;
				
				// Set the MP Cost.
				switch (terrain) {
				case 0:
						mpCost = 1;
						break;
				case 1:
						mpCost = 2;
						break;
				default:
						mpCost = 0;
						break;
				}
		}
			
		public Vector3 getPosition ()
		{
				return position;
		}

		public void setOccupant (Character occupant)
		{
				this.occupant = occupant;
		}

		public Character getOccupant ()
		{
				return occupant;
		}

		public int getTerrain ()
		{
				return terrain;
		}
		
		public int getMpCost ()
		{
				return mpCost;
		}
}
