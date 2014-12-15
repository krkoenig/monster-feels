using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FighterAI : MonoBehaviour
{
		//LinkedList<Tile> targets;
		Pathfinding thing;
		Character me;
		Tile currentTile;
		int AP;
		int MP;
		Tile targ;
		Queue que;
		TileMap tiley; 
		LinkedList<Path> paths;
		LinkedList<Tile> theone;

		void Start ()
		{

		GameObject obj = GameObject.Find ("TileMap");
		Controls controller = obj.GetComponent<Controls> ();
		que = controller.queue;
		tiley = obj.GetComponent<TileMap> ();

		//targets = new LinkedList<Tile> ();
		//thing = new Pathfinding ();  actually need something here
		//thing = Pathfinding.Instantiate; this is promising
		//me = me.getSelf ();
		me = GetComponent <Character>();
		thing = GetComponent<Pathfinding>();
		currentTile = new Tile(tiley.getTile((int)me.getPosition().x, (int)me.getPosition().y).getPosition(), tiley.getTile((int)me.getPosition().x, (int)me.getPosition().y).getTerrain());
		//tiley.getTile ((int)me.getPosition ().x, (int)me.getPosition().y)		
		AP = me.getCurrentAP();
		MP = me.getCurrentMP();
		paths = new LinkedList<Path>();
		theone = new LinkedList<Tile>();
		}

		void Update ()
		{
				//if its my turn
				if (que.getActiveCharacter() == me) 
				{
					Debug.Log ("My Turn!");
						LinkedList<Tile> options = new LinkedList<Tile> ();
						LinkedList<Tile> blank = new LinkedList<Tile> ();
					//Debug.Log(currentTile.getPosition());
						options = thing.far_search (currentTile, MP);  //just going to far search straight away
						paths = thing.finder(currentTile, options, MP);
						theone = BestPath (paths);
						Movement (theone);
						targ = wasd (currentTile);
			//Debug.Log(targ.getPosition() + " is the target");
						if (targ != currentTile) { //finds the target; if there isn't one don't attack
								Attack (AP, targ);
						}
					//
			que.getActiveCharacter ().endTurn ();
			que.nextCharacter ();	

			//Debug.Log("NOT ENDING");
				}
		}

		void Attack (int AP, Tile target)
		{
				switch (AP) {
				case 2:
						me.useSkill (0, target);
						//me.loseAP (2);
						break;
				case 3:
						me.useSkill (0, target);
						//me.loseAP (2);
						break;
				case 4:
						me.useSkill (0, target);
			//yield WaitForSeconds(.75);
						me.useSkill (0, target);
						//me.loseAP (4);
						break;
				case 5:
						me.useSkill (0, target);
			//WaitForSeconds(1);
						me.useSkill (0, target);
						//me.loseAP (4);
						break;
				case 6:
						me.useSkill (0, target);
			//WaitForSeconds(1);
						me.useSkill (0, target);
			//WaitForSeconds(1);
						me.useSkill (0, target);
						//me.loseAP (4);
						break;
				}
		}

		void Movement (LinkedList<Tile> path)
		{
				foreach (Tile tyler in path) 
				{
					//Debug.Log("walk");
						me.move (tyler);
						//WaitForSeconds(.5);
				}
		}

		LinkedList<Tile> BestPath (LinkedList<Path> options)
		{
				int newcost = 9999;
				LinkedList<Tile> best = new LinkedList<Tile> ();
				int i = 0;
				foreach (Path poth in options) 
				{
			i++;
			Debug.Log(i + " + " + poth.cost);
			//Debug.Log(poth.cost);
					if (poth.cost < newcost) 
					{
						newcost = poth.cost;
						best = poth.route;
				//Debug.Log(newcost + " " + best.Last.Value.getPosition());
					}
				}
		Debug.Log (newcost);
				return best;
		}

		Tile wasd (Tile current)
		{
				int x = (int)current.getPosition ().x;
				int y = (int)current.getPosition ().y;
				Tile target;
				if (tiley.getTile (x + 1, y).getOccupant () != null && tiley.getTile (x + 1, y).getOccupant ().isAlly == true && tiley.getTile (x + 1, y).getOccupant ().isStealthed == false) {
						target = tiley.getTile (x + 1, y);
				} else if (tiley.getTile (x, y + 1).getOccupant () != null && tiley.getTile (x, y + 1).getOccupant ().isAlly == true && tiley.getTile (x, y + 1).getOccupant ().isStealthed == false) {
						target = tiley.getTile (x, y + 1);
				} else if (tiley.getTile (x - 1, y).getOccupant () != null && tiley.getTile (x - 1, y).getOccupant ().isAlly == true && tiley.getTile (x - 1, y).getOccupant ().isStealthed == false) {
						target = tiley.getTile (x - 1, y);
				} else if (tiley.getTile (x, y - 1).getOccupant () != null && tiley.getTile (x, y - 1).getOccupant ().isAlly == true && tiley.getTile (x, y - 1).getOccupant ().isStealthed == false) {
						target = tiley.getTile (x, y - 1);
				} else {
						target = current;
				}
				return target;
		}
}
