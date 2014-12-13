using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Queue
{
		// The queue.
		private LinkedList<Character> characters;
		private GameObject activeTile;

		// Use this for initialization
		public Queue ()
		{
				// Grab all Characters from the scene and move them into the queue.
				// TODO: Sort using Intitiative.
				Character[] charObjects = (Character[])Object.FindObjectsOfType<Character> ();
				characters = new LinkedList<Character> ();
				foreach (Character data in charObjects) {
						characters.AddLast (data.GetComponent<Character> ());			
				}				
		createActiveTile ();
		}

		// Find out who's turn it is.
		public Character getActiveCharacter ()
		{
				return characters.First.Value;
		}

//		public ActiveTile getActiveTile()
//		{
//		return activeTile;
//		}

		public Character[] listAll ()
		{
				Character[] temp = new Character[characters.Count];
				characters.CopyTo (temp, 0);
				return temp;
		}

		// Progress to the next character.
		public void nextCharacter ()
		{					
				characters.AddLast (characters.First.Value);
				characters.RemoveFirst ();
		}

		private void createActiveTile()
		{
		activeTile = GameObject.CreatePrimitive (PrimitiveType.Quad);
		activeTile.renderer.material.color = new Color(255,252,0,.25f);
		activeTile.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		activeTile.transform.localScale = new Vector3 (1f, 1f, 1f);
		activeTile.transform.position = new Vector3 (characters.First.Value.getPosition().x + .5f, characters.First.Value.getPosition().y + .5f, -1);
		}

		public void moveActiveTile()
		{
		activeTile.transform.position = new Vector3 (characters.First.Value.getPosition().x + .5f, characters.First.Value.getPosition().y + .5f, -1);
		}

}