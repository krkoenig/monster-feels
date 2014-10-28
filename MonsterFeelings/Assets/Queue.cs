using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Queue : MonoBehaviour
{
		private Character[] charObjects;
		private LinkedList<Character> characters;

		// Use this for initialization
		void Start ()
		{
				charObjects = (Character[])Object.FindObjectsOfType<Character> ();
				characters = new LinkedList<Character> ();
				foreach (Character data in charObjects) {
						characters.AddLast (data.GetComponent<Character> ());			
				}
		}

		// Update is called once per frame
		void Update ()
		{
				characters.First.Value.setCurrentcharacter (true);

				// When space is pressed, the next character can move.
				if (Input.GetKeyDown ("space")) {
						characters.First.Value.setCurrentcharacter (false);						
						characters.AddLast (characters.First.Value);
						characters.RemoveFirst ();
				}
		}

}