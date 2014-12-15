using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FighterAI : MonoBehaviour
{
		//LinkedList<Tile> targets;
		Pathfinding thing;
		public Character me;
		Tile currentTile;
		int AP;
		Tile targ;
		Queue que;
		TileMap tiley;

		void start ()
		{
				//targets = new LinkedList<Tile> ();
				thing = new Pathfinding ();
				//me = new Character ();
				currentTile = tiley.getTile ((int)me.getPosition ().x, (int)me.getPosition ().y);
				AP = me.getCurrentAP ();
				GameObject obj = GameObject.Find ("TileMap");
				Controls controller = obj.GetComponent<Controls> ();
				que = controller.queue;
				tiley = obj.GetComponent<TileMap> ();
		}

		void update ()
		{
				//if its my turn
				if (que.getActiveCharacter () == me) {
						LinkedList<Tile> options = new LinkedList<Tile> ();
						LinkedList<Tile> blank = new LinkedList<Tile> ();
						options = thing.close_search (currentTile, me.getCurrentMP ());
						if (options.Equals (blank)) {
								options = thing.far_search (currentTile, me.getCurrentMP ());
						}
						Movement (BestPath (thing.pathfind (currentTile, options, me.getCurrentMP ())));
						targ = wasd (currentTile);
						if (targ != currentTile) { //finds the target; if there isn't one don't attack
								Attack (AP, targ);
						}
						me.endTurn ();
				}
				//run close search
				//if close search is blank, run far search
				//feed the list of tiles to pathfind

				//for each path in the list
				//save the one with the lowest cost

				//move the path with the lowest cost
				//do the attack
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
				foreach (Tile tyler in path) {
						me.move (tyler);
						//WaitForSeconds(.5);
				}
		}

		LinkedList<Tile> BestPath (ArrayList options)
		{
				int cost = 9999;
				LinkedList<Tile> best = new LinkedList<Tile> ();
				foreach (Path poth in options) {
						if (poth.cost < cost) {
								cost = poth.cost;
								best = poth.route;
						}
				}
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
