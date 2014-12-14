using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Queue
{
		// The queue.
		private List<Character> characters;
		private GameObject activeTile;

		// Use this for initialization
		public Queue ()
		{
				// Grab all Characters from the scene and move them into the queue.
				// TODO: Sort using Intitiative.
				Character[] charObj = (Character[])Object.FindObjectsOfType<Character> ();
				characters = new List<Character> ();
				foreach (Character data in charObj) {
						characters.Add (data.GetComponent<Character> ());
				}		
				
				sortQueue ();	
				createActiveTile ();
		}

		// Find out who's turn it is.
		public Character getActiveCharacter ()
		{
				return characters [0];
		}

		public List<Character> listAll ()
		{
				return characters;
		}

		// Progress to the next character.
		public void nextCharacter ()
		{					
				characters.Add (characters [0]);
				characters.RemoveAt (0);
		}

		private void createActiveTile ()
		{
				activeTile = GameObject.CreatePrimitive (PrimitiveType.Quad);
				activeTile.renderer.material.color = new Color (255, 252, 0, .25f);
				activeTile.renderer.material.shader = Shader.Find ("Transparent/Diffuse");
				activeTile.transform.localScale = new Vector3 (1f, 1f, 1f);
				activeTile.transform.position = new Vector3 (characters [0].getPosition ().x + .5f, characters [0].getPosition ().y + .5f, -1);
		}

		public void moveActiveTile ()
		{
				activeTile.transform.position = new Vector3 (characters [0].getPosition ().x + .5f, characters [0].getPosition ().y + .5f, -1);
		}
		
		private void sortQueue ()
		{
				characters.Sort ((s2, s1) => s1.dexterity.CompareTo (s2.dexterity));
		}
		
		public void removeDead ()
		{
				for (int i = characters.Count - 1; i >=0; i--) {
						if (characters [i] == null) {
								characters.RemoveAt (i);
						}
				}
		}

}