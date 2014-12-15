using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{

	//public TileMap tiley = Tilemap.tiles;
	public int mapsizex = 24;
	public int mapsizey = 23;
	TileMap tiley;

	public Pathfinding()
	{
	}

	// Update is called once per frame
	public void Start ()
	{
		GameObject obj = GameObject.Find ("TileMap");
		tiley = obj.GetComponent<TileMap> ();
	}

	//searches within x spaces of the given tile for all enemies.
	public LinkedList<Tile> close_search (Tile origin, int MP)
	{
		int xcoord = (int)origin.getPosition ().x;
		int ycoord = (int)origin.getPosition ().y;
		//int xcoo = xcoord - 2;
		//int ycoo = ycoord - 2;
		//Tile starter = new Tile(tiley.getTile(xcoo, ycoo).getPosition(), tiley.getTile(xcoo, ycoo).getTerrain());
		Tile iter = new Tile(tiley.getTile(xcoord, ycoord).getPosition(), tiley.getTile(xcoord, ycoord).getTerrain()); //an iterator tile to reference
		//bool found = false;
		LinkedList<Tile> listy = new LinkedList<Tile> ();
		//5x5 grid search
		for (int i = 0; i < 5; i++) 
		{
			for (int j = 0; j < 5; j++)
			{
				if (xcoord - 2 + i >= 0 && xcoord -2 + i <= mapsizex && ycoord - 2 + j >= 0 && ycoord - 2 + j <= mapsizey)
				{
					//Debug.Log(i + " " + j);
					iter = tiley.getTile(xcoord - 2 + i, ycoord - 2 + j);
					if (iter.getOccupant() != null && iter.getOccupant().isAlly == true && iter.getOccupant().isStealthed == false && iter != origin)
					{
						listy.AddLast(iter);
					}
				}
			}
		}
		return listy;
	}

	public LinkedList<Tile> far_search (Tile origin, int MP)
	{
		int xcoord = (int)origin.getPosition ().x;
		int ycoord = (int)origin.getPosition ().y;
		//int xcoo = xcoord - 2;
		//int ycoo = ycoord - 2;
		//Tile starter = new Tile(tiley.getTile(xcoo, ycoo).getPosition(), tiley.getTile(xcoo, ycoo).getTerrain());
		Tile iter = new Tile(tiley.getTile(xcoord, ycoord).getPosition(), tiley.getTile(xcoord, ycoord).getTerrain()); //an iterator tile to reference
		LinkedList<Tile> listy = new LinkedList<Tile> ();
		for (int i = 0; i < mapsizex + 1; i++) 
		{
			for (int j = 0; j < mapsizey + 1; j++)
			{
				if (i >= 0 && i <= mapsizex + 1 && j >= 0 && j <= mapsizey + 1) //don't really need this anymore...
				{
					//Debug.Log(i + " " + j);
					iter = tiley.getTile(i,j);
					if (iter.getOccupant() != null) //&& iter.getOccupant().isAlly == false && iter.getOccupant().isStealthed == false && iter != origin)
					{
						if (iter.getOccupant().isAlly == true)
						{
							if (iter.getOccupant().isStealthed == false)
							{
								if (iter.getPosition() != origin.getPosition())
								{
									listy.AddLast(iter);
									//Debug.Log(iter.getPosition());
								}
							}
						}
					}
				}
			}
		}
		return listy;
	}

	public LinkedList<Path> finder (Tile origin, LinkedList<Tile> listy, int MP)
	{
		int xcoord = (int)origin.getPosition ().x;
		int ycoord = (int)origin.getPosition ().y;
		LinkedList<Path> Paths = new LinkedList<Path>();


		foreach (Tile tyler in listy)
		{
			Tile iter = new Tile(tiley.getTile(xcoord, ycoord).getPosition(), tiley.getTile(xcoord, ycoord).getTerrain());
			Tile hold = new Tile(tiley.getTile(xcoord, ycoord).getPosition(), tiley.getTile(xcoord, ycoord).getTerrain());
			Path way = new Path (3);
			for (int i = 0; i < MP; i++)
			{
				int g = 10000;
				int h = 10000;
				int f = 10000;
				int gg = 0;
				int hh = 0; 
				int ff = 0;
				iter = new Tile(iter.getPosition() + new Vector3(0,1,0), iter.getTerrain());
				gg = iter.getMpCost();
				hh = Math.Abs((int)iter.getPosition().x - (int)tyler.getPosition().x) + Math.Abs((int)iter.getPosition().y - (int)tyler.getPosition().y);
				ff = gg + hh;
				if (ff < f)
				{
					g = gg;
					h = hh;
					f = ff;
					hold = iter;
				}
				iter = new Tile(iter.getPosition() + new Vector3(1,-1,0), iter.getTerrain());
				gg = iter.getMpCost();
				hh = Math.Abs((int)iter.getPosition().x - (int)tyler.getPosition().x) + Math.Abs((int)iter.getPosition().y - (int)tyler.getPosition().y);
				ff = gg + hh;
				if (ff < f)
				{
					g = gg;
					h = hh;
					f = ff;
					hold = iter;
				}
				iter = new Tile(iter.getPosition() + new Vector3(-1,-1,0), iter.getTerrain());
				gg = iter.getMpCost();
				hh = Math.Abs((int)iter.getPosition().x - (int)tyler.getPosition().x) + Math.Abs((int)iter.getPosition().y - (int)tyler.getPosition().y);
				ff = gg + hh;
				if (ff < f)
				{
					g = gg;
					h = hh;
					f = ff;
					hold = iter;
				}
				iter = new Tile(iter.getPosition() + new Vector3(-1,1,0), iter.getTerrain());
				gg = iter.getMpCost();
				hh = Math.Abs((int)iter.getPosition().x - (int)tyler.getPosition().x) + Math.Abs((int)iter.getPosition().y - (int)tyler.getPosition().y);
				ff = gg + hh;
				if (ff < f)
				{
					g = gg;
					h = hh;
					f = ff;
					hold = iter;
				}
				way.cost = way.cost + f;
				way.route.AddLast(hold);
			}
			Paths.AddLast(way);
		}
		return Paths;
	}

//	public LinkedList<Path> pathfind (Tile origin, LinkedList<Tile> listy, int MP)
//	{
//		int xcoord = (int)origin.getPosition ().x;
//		int ycoord = (int)origin.getPosition ().y;
//
//			LinkedList<Path> Paths = new LinkedList<Path>();
//			foreach (Tile tyler in listy) 
//			{
//				Tile iter = new Tile(tiley.getTile(xcoord, ycoord).getPosition(), tiley.getTile(xcoord, ycoord).getTerrain());
//				Path poth = new Path (5); //it needs an integer, which doesn't do anything
//
//				//iter = origin;
//				//each tile has a g value and an h value.
//				//the g value is the cost of moving to that square.
//				//the h value is the theoretical cost of moving to the goal
//				//add those together to get the f value, which is used to find the shortest path
//				for (int i = 0; i < MP; i++) 
//				{
//				int g = 0;
//				int h = 0;
//				int f = 0;
//				Tile temp = new Tile(tiley.getTile(xcoord, ycoord).getPosition(), tiley.getTile(xcoord, ycoord).getTerrain());	
//
//				for (int j = 0; j < 4; j++) 
//					{
////						int g = 0;
////						int h = 0;
////						int f = 0;
//						//Tile temp = new Tile(origin.getPosition(), origin.getTerrain());//origin; //this'll get overwritten quickly, but temp has to be assigned something
//						if (j == 0) 
//						{
//						//	Debug.Log("AAAAA");
//							//Debug.Log("j = " + j + " i = " + i + " tile = " + tyler.getPosition());
//							//iter = tiley.getTile ((int)iter.getPosition ().x, (int)iter.getPosition ().y + 1);
//						iter = new Tile(iter.getPosition() + new Vector3(0,1,0), iter.getTerrain());
//							//Debug.Log("iter position 1: " + iter.getPosition());
//							if (iter.getOccupant () != null) 
//							{
//								g = 9999;
//							} 
//							else 
//							{
//								g = iter.getMpCost ();
//							}
//							h = Math.Abs ((int)iter.getPosition ().x - (int)tyler.getPosition ().x) + Math.Abs ((int)iter.getPosition ().y - (int)tyler.getPosition ().y);
//							f = g + h;
//							temp = new Tile(iter.getPosition(), iter.getTerrain());
//						//Debug.Log("temp = " + temp.getPosition());
//						//Debug.Log("     " + g + " " + h + " " + f);
//						} 
//						else if (j == 1) 
//						{
//						iter = new Tile(iter.getPosition() + new Vector3(1,-1,0), iter.getTerrain());
//						//Debug.Log("iter position 2: " + iter.getPosition());
//							int gtemp;
//							if (iter.getOccupant () != null) 
//							{
//								gtemp = 9999;
//							} 
//							else 
//							{
//								gtemp = iter.getMpCost ();
//							}
//							int htemp = Math.Abs ((int)iter.getPosition ().x - (int)tyler.getPosition ().x) + Math.Abs ((int)iter.getPosition ().y - (int)tyler.getPosition ().y);
//							int ftemp = gtemp + htemp;
//							if (ftemp < f) 
//							{
//								g = gtemp;
//								h = htemp;
//								f = ftemp;
//								temp = new Tile(iter.getPosition(), iter.getTerrain());
//							}
//						//Debug.Log("temp = " + temp.getPosition());
//						//Debug.Log("     " + gtemp + " " + htemp + " " + ftemp);
//						} 
//						else if (j == 2) 
//						{
//						iter = new Tile(iter.getPosition() + new Vector3(-1,-1,0), iter.getTerrain());
//						//Debug.Log("iter position 3: " + iter.getPosition());
//							int gtemp;
//							if (iter.getOccupant () != null) 
//							{
//								gtemp = 9999;
//							} 
//							else 
//							{
//								gtemp = iter.getMpCost ();
//							}
//							int htemp = Math.Abs ((int)iter.getPosition ().x - (int)tyler.getPosition ().x) + Math.Abs ((int)iter.getPosition ().y - (int)tyler.getPosition ().y);
//							int ftemp = gtemp + htemp;
//							if (ftemp < f) 
//							{
//								g = gtemp;
//								h = htemp;
//								f = ftemp;
//								temp = new Tile(iter.getPosition(), iter.getTerrain());
//							}
//						//Debug.Log("temp = " + temp.getPosition());
//						//Debug.Log("     " + gtemp + " " + htemp + " " + ftemp);
//						} 
//						else if (j == 3) 
//						{
//						iter = new Tile(iter.getPosition() + new Vector3(-1,1,0), iter.getTerrain());
//						//Debug.Log("iter position 4: " + iter.getPosition());
//							int gtemp;
//							if (iter.getOccupant () != null) {
//									gtemp = 9999;
//							} else {
//									gtemp = iter.getMpCost ();
//							}							
//							int htemp = Math.Abs ((int)iter.getPosition ().x - (int)tyler.getPosition ().x) + Math.Abs ((int)iter.getPosition ().y - (int)tyler.getPosition ().y);
//							int ftemp = gtemp + htemp;
//							if (ftemp < f) 
//							{
//								g = gtemp;
//								h = htemp;
//								f = ftemp;
//								temp = new Tile(iter.getPosition(), iter.getTerrain());
//							}
//							poth.route.AddLast (temp);
//							poth.addcost (g);
//						//Debug.Log(temp.getPosition());
//							iter = new Tile(temp.getPosition(), temp.getTerrain());
//							if (g == 2) 
//							{
//								i++;
//							}
//						//Debug.Log("     " + gtemp + " " + htemp + " " + ftemp);
//						//Debug.Log(" the winner is    " + g + " " + h + " " + f);
//						//Debug.Log("temp = " + temp.getPosition());
//						}
//
//					}
//						Paths.AddLast(poth);
//
//				}
//			Debug.Log(Paths.Last.Value.cost);
//		}
//		return Paths;
//	}


}

public struct Path
{
		public LinkedList<Tile> route;
		public int cost;
	
		public Path (int i)
		{
				route = new LinkedList<Tile> ();
				cost = 0;
		}
		public void addition (Tile til)
		{
				route.AddLast (til);
		}
		public void addcost (int co)
		{
				cost = cost + co;
		}
}