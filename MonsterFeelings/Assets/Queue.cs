using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Queue
{
		// The queue.
		private LinkedList<Character> characters;

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
		}

		// Find out who's turn it is.
		public Character getActiveCharacter ()
		{
				return characters.First.Value;
		}

		// Progress to the next character.
		public void nextCharacter ()
		{					
				characters.AddLast (characters.First.Value);
				characters.RemoveFirst ();
		}

}