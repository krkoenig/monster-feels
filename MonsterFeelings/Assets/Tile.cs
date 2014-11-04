using UnityEngine;
using System.Collections;

public class Tile
{
		private int terrain;
		private int mpCost;
		private Character occupant;
		private int x;
		private int y;

		public Tile (int x, int y)
		{
				this.x = x;
				this.y = y;
		}

		public void setOccupant (Character occupant)
		{
				this.occupant = occupant;
		}

		public Character getOccupant ()
		{
				return occupant;
		}

		public void setTerrain (int terrain)
		{
				this.terrain = terrain;
				setMpCost ();
		}

		public int getTerrain ()
		{
				return terrain;
		}
		
		public int getMpCost ()
		{
				return mpCost;
		}

		private void setMpCost ()
		{
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
}
