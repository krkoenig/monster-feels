﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

	//searches within x spaces of the given tile for all enemies.
	LinkedList<Tile> close_search (Tile origin, int MP) 
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
		LinkedList<Tile> listy;
		//i need to search in a diamond pattern in a growing radius around the origin
		for (int i = 0; i < MP + 1; i++) 
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
							listy.AddLast(iter);
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
							listy.AddLast(iter);
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
							listy.AddLast(iter);
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
							listy.AddLast(iter);
						}
					}
				}
			}
		}
		return listy;
	}

	LinkedList<Tile> far_search (Tile origin, int MP) //will search within 25 tiles to find a target, still attempting to find the closest one
	{
		int xcoord = (int)origin.getPosition ().x;
		int ycoord = (int)origin.getPosition ().y;
		Tile up = tiley.getTile (xcoord, ycoord + 1);//TileMap.getTile(xcoord, ycoord); // = origin.getPosition ();
		Tile right = tiley.getTile (xcoord + 1, ycoord); //to the right
		Tile bot = tiley.getTile (xcoord, ycoord - 1); //underneath
		Tile left = tiley.getTile (xcoord - 1, ycoord); //to the left
		LinkedList<Tile> listy;
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
							listy.AddLast(iter);

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
							listy.AddLast(iter);
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
							listy.AddLast(iter);
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
							listy.AddLast(iter);
						}
					}
				}
			}
		}
		return listy;
	}

	private struct Path
	{
		private LinkedList<Tile> route;
		private int cost;
	}

	void pathfind(Tile origin, LinkedList<Tile> listy, int MP)
	{
		int xcoord = (int)origin.getPosition ().x;
		int ycoord = (int)origin.getPosition ().y;
		Tile up = tiley.getTile (xcoord, ycoord + 1);//TileMap.getTile(xcoord, ycoord); // = origin.getPosition ();
		Tile right = tiley.getTile (xcoord + 1, ycoord); //to the right
		Tile bot = tiley.getTile (xcoord, ycoord - 1); //underneath
		Tile left = tiley.getTile (xcoord - 1, ycoord); //to the left
		//LinkedList<Path> Paths; //this will hold all the paths, from closest to farthest kinda
		Path pathss = new Path();


		foreach(Tile tyler in listy)
		{
			//int newx = Math.Abs(tyler.getPosition().x - origin.getPosition().x);
			//int newy = Math.Abs(tyler.getPosition().y - origin.getPosition().y);
			int[][] costs = tiley.getTileGrid();
			//2d arraylist
			//sort put these into their spots
			//then do a for loop trying different combinations, save the best combination
			/*
			for (int i = 0; i < abs(tyler.getPosition().x - origin.getPosition().x) + 1; i++)
			{
				for (int j = 0; j < abs(tyler.getPosition().y - origin.getPosition().y) + 1; j++)
				{
					iter = tiley.getTile(origin.getPosition().x + i, origin.getPosition().y + j);
					costs[i][j] = iter.getMpCost;
				}
			} */
			while (MP > 0)
			{
				Tile iter = origin;
				//each tile has a g value and an h value.
				//the g value is the cost of moving to that square.
				//the h value is the theoretical cost of moving to the goal
				//add those together to get the f value, which is used to find the shortest path
				for (int i = 0; i < MP; i++)
				{
					for (int j = 0; j < 4; j++)
					{
						int g;
						int h;
						int f;
						Tile temp;

						if (j == 0)
						{
							iter = tiley.getTile(iter.getPosition().x, iter.getPosition.y + 1);
							g = iter.getMpCost();
							h = Math.Abs (iter.getPosition().x - tyler.getPosition().x) + Math.Abs(iter.getPosition().y - tyler.getPosition().y);
							f = g + h;
							temp = iter;
						}
						else if (j == 1)
						{
							iter = tiley.getTile(iter.getPosition().x + 1, iter.getPosition.y - 1);
							int gtemp = iter.getMpCost();
							int htemp = Math.Abs (iter.getPosition().x - tyler.getPosition().x) + Math.Abs(iter.getPosition().y - tyler.getPosition().y);
							int ftemp = gtemp + htemp;
							if (ftemp < f)
							{
								g = gtemp;
								h = htemp;
								f = ftemp;
								temp = iter;
							}
						}
						else if (j == 2)
						{
							iter = tiley.getTile(iter.getPosition().x - 1, iter.getPosition.y - 1);
							int gtemp = iter.getMpCost();
							int htemp = Math.Abs (iter.getPosition().x - tyler.getPosition().x) + Math.Abs(iter.getPosition().y - tyler.getPosition().y);
							int ftemp = gtemp + htemp;
							if (ftemp < f)
							{
								g = gtemp;
								h = htemp;
								f = ftemp;
								temp = iter;
							}
						}
						else if (j == 3)
						{
							iter = tiley.getTile(iter.getPosition().x - 1, iter.getPosition.y + 1);
							int gtemp = iter.getMpCost();
							int htemp = Math.Abs (iter.getPosition().x - tyler.getPosition().x) + Math.Abs(iter.getPosition().y - tyler.getPosition().y);
							int ftemp = gtemp + htemp;
							if (ftemp < f)
							{
								g = gtemp;
								h = htemp;
								f = ftemp;
								temp = iter;
							}
						}
						//add the tile to the list (in the proper place in the arraylist), with its cost
						//choose the path with the lowest cost
						//pass that into the fighter ai
					}
				}
			}

		}
	}









}