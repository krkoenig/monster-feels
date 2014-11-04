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

		// Update is called once per frame
		void Update ()
		{
				// Makes the first character in the queue the active character.
				characters.First.Value.setIsTurn (true);

				// When space is pressed, the next character can move.
				if (Input.GetKeyDown ("space")) {
						characters.First.Value.setIsTurn (false);						
						characters.AddLast (characters.First.Value);
						characters.RemoveFirst ();
				}
		}

}