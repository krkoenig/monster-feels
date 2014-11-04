using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Queue : MonoBehaviour
{
		// The queue.
		private LinkedList<Character> characters;

		// Use this for initialization
		void Start ()
		{
				// Grab all Characters from the scene and move them into the queue.
				// TODO: Sort using Intitiative.
				Character[] charObjects = (Character[])Object.FindObjectsOfType<Character> ();
				characters = new LinkedList<Character> ();
				foreach (Character data in charObjects) {
						characters.AddLast (data.GetComponent<Character> ());			
				}
		}

		public Character getActiveCharacter ()
		{
				return characters.First.Value;
		}

		public void nextCharacter ()
		{					
				characters.AddLast (characters.First.Value);
				characters.RemoveFirst ();
		}

}