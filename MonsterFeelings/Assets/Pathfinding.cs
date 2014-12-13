using UnityEngine;
using System.Collections;

public class Pathfinding : MonoBehaviour {

	//public TileMap tiley = Tilemap.tiles;
	public int mapsizex = 24;
	public int mapsizey = 23;
	TileMap tiley;


	// Update is called once per frame
	void Start () 
	{
		GameObject obj = GameObject.Find ("TileMap");
		tiley = obj.GetComponent<TileMap> ();
	}

	//searches within x spaces of the given tile for an enemy.
	//if nothing is found, calls far_search
	Tile close_search (Tile origin, int MP) 
	{
		int xcoord = (int)origin.getPosition ().x;
		int ycoord = (int)origin.getPosition ().y;
		Tile up = tiley.getTile (xcoord, ycoord + 1);//TileMap.getTile(xcoord, ycoord); // = origin.getPosition ();
		Tile right = tiley.getTile (xcoord + 1, ycoord); //to the right
		Tile bot = tiley.getTile (xcoord, ycoord - 1); //underneath
		Tile left = tiley.getTile (xcoord - 1, ycoord); //to the left

		Tile target = origin; //its where the nearest enemy is.  by default its the tile the character starts on
		Tile iter; //an iterator tile to reference
		bool found = false;
		//i need to search in a diamond pattern in a growing radius around the origin
		int i = 1;
		while (i < MP + 1 && found == false) 
		{ 	//for all the tiles in a radius 'i' around the origin
			for (int k = 0; k < i; k++)
			{
				iter = up; //starts up, and moves down and to the right
				if (iter.getPosition().x + k <= mapsizex && iter.getPosition().y - k >= 0)
				{
					iter = tiley.getTile((int)iter.getPosition().x + k, (int)iter.getPosition().y + k);

					if (iter.getOccupant() != null && found == false)
					{
						if (iter.getOccupant().isAlly == false)
						{
							target = iter;
							found = true;
						}
					}
				}
			}
			for (int k = 0; k < i; k++)
			{
				iter = right; //start at the right, move down and to the left
				if (iter.getPosition().x - k >= 0 && iter.getPosition().y - k >= 0)
				{
					iter = tiley.getTile((int)iter.getPosition().x - k, (int)iter.getPosition().y + k);
					
					if (iter.getOccupant() != null && found == false)
					{
						if (iter.getOccupant().isAlly == false)
						{
							target = iter;
							found = true;
						}
					}
				}
			}
			for (int k = 0; k < i; k++)
			{
				iter = bot; //start at the bottom now we're here
				if (iter.getPosition().x - k >= 0 && iter.getPosition().y + k <= mapsizey)
				{
					iter = tiley.getTile((int)iter.getPosition().x - k, (int)iter.getPosition().y + k);
					
					if (iter.getOccupant() != null && found == false)
					{
						if (iter.getOccupant().isAlly == false)
						{
							target = iter;
							found = true;
						}
					}
				}
			}
			for (int k = 0; k < i; k++)
			{
				iter = left; //start at the left, and move upwards and rightwards
				if (iter.getPosition().x + k <= mapsizex && iter.getPosition().y + k <= mapsizey)
				{
					iter = tiley.getTile((int)iter.getPosition().x - k, (int)iter.getPosition().y + k);
					
					if (iter.getOccupant() != null && found == false)
					{
						if (iter.getOccupant().isAlly == false)
						{
							target = iter;
							found = true;
						}
					}
				}
			}
			i++;
		}
		if (target.getPosition() == origin.getPosition()) 
		{
			target = far_search(origin, MP);
		}
		return target;
	}

	Tile far_search (Tile origin, int MP) //will search within 25 tiles to find a target, still attempting to find the closest one
	{
		int xcoord = (int)origin.getPosition ().x;
		int ycoord = (int)origin.getPosition ().y;
		Tile up = tiley.getTile (xcoord, ycoord + 1);//TileMap.getTile(xcoord, ycoord); // = origin.getPosition ();
		Tile right = tiley.getTile (xcoord + 1, ycoord); //to the right
		Tile bot = tiley.getTile (xcoord, ycoord - 1); //underneath
		Tile left = tiley.getTile (xcoord - 1, ycoord); //to the left
		
		Tile target = origin; //its where the nearest enemy is.  by default its the tile the character starts on
		Tile iter; //an iterator tile to reference
		bool found = false;
		//i need to search in a diamond pattern in a growing radius around the origin
		int i = MP;
		while (found == false) 
		{ 	//for all the tiles in a radius 'i' around the origin
			for (int k = 0; k < i; k++)
			{
				iter = up; //starts up, and moves down and to the right
				if (iter.getPosition().x + k <= mapsizex && iter.getPosition().y - k >= 0)
				{
					iter = tiley.getTile((int)iter.getPosition().x + k, (int)iter.getPosition().y + k);
					
					if (iter.getOccupant() != null && found == false)
					{
						if (iter.getOccupant().isAlly == false)
						{
							target = iter;
							found = true;
						}
					}
				}
			}
			for (int k = 0; k < i; k++)
			{
				iter = right; //start at the right, move down and to the left
				if (iter.getPosition().x - k >= 0 && iter.getPosition().y - k >= 0)
				{
					iter = tiley.getTile((int)iter.getPosition().x - k, (int)iter.getPosition().y + k);
					
					if (iter.getOccupant() != null && found == false)
					{
						if (iter.getOccupant().isAlly == false)
						{
							target = iter;
							found = true;
						}
					}
				}
			}
			for (int k = 0; k < i; k++)
			{
				iter = bot; //start at the bottom now we're here
				if (iter.getPosition().x - k >= 0 && iter.getPosition().y + k <= mapsizey)
				{
					iter = tiley.getTile((int)iter.getPosition().x - k, (int)iter.getPosition().y + k);
					
					if (iter.getOccupant() != null && found == false)
					{
						if (iter.getOccupant().isAlly == false)
						{
							target = iter;
							found = true;
						}
					}
				}
			}
			for (int k = 0; k < i; k++)
			{
				iter = left; //start at the left, and move upwards and rightwards
				if (iter.getPosition().x + k <= mapsizex && iter.getPosition().y + k <= mapsizey)
				{
					iter = tiley.getTile((int)iter.getPosition().x - k, (int)iter.getPosition().y + k);
					
					if (iter.getOccupant() != null && found == false)
					{
						if (iter.getOccupant().isAlly == false)
						{
							target = iter;
							found = true;
						}
					}
				}
			}
			i++;
		}
		return target;
	}

	void pathfind(Tile origin, Tile target, int MP)
	{
		int xcoord = (int)origin.getPosition ().x;
		int ycoord = (int)origin.getPosition ().y;
		Tile up = tiley.getTile (xcoord, ycoord + 1);//TileMap.getTile(xcoord, ycoord); // = origin.getPosition ();
		Tile right = tiley.getTile (xcoord + 1, ycoord); //to the right
		Tile bot = tiley.getTile (xcoord, ycoord - 1); //underneath
		Tile left = tiley.getTile (xcoord - 1, ycoord); //to the left
		Tile iter; //an iterator tile to reference
		bool found = false;

		for (int i = 1; i < MP + 1; i++) { //for all the tiles in a radius 'i' around the origin
			for (int k = 0; k < i; k++)
			{
				iter = up;
				if (iter.getOccupant() != null && found == false)
				{
					if (iter.getOccupant().isAlly == false)
					{
						target = iter;
						found = true;
					}
				}
			}
			for (int k = 0; k < i; k++)
			{
				iter = right;
				if (iter.getOccupant() != null && found == false)
				{
					if (iter.getOccupant().isAlly == false)
					{
						target = iter;
						found = true;
					}
				}
			}
			for (int k = 0; k < i; k++)
			{
				iter = bot;
				if (iter.getOccupant() != null && found == false)
				{
					if (iter.getOccupant().isAlly == false)
					{
						target = iter;
						found = true;
					}
				}
			}
			for (int k = 0; k < i; k++)
			{
				iter = left;
				if (iter.getOccupant() != null && found == false)
				{
					if (iter.getOccupant().isAlly == false)
					{
						target = iter;
						found = true;
					}
				}
				
			}
		}
	}









}
