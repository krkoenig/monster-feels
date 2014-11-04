using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{

		private Queue queue;
		private TileMap tileMap;

		// Use this for initialization
		void Start ()
		{
				GameObject obj = GameObject.Find ("TileMap");
				tileMap = obj.GetComponent<TileMap> ();
				queue = obj.GetComponent<Queue> ();
		}
	
		// Update is called once per frame
		void Update ()
		{			
				// Mouse location
				// Convert the mouse's screen coordinates to world coordinates.
				Vector3 mousePos = Input.mousePosition;
				mousePos.z = 1.0f;
				mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			
				// Convert them into int's.
				int mouseX = Mathf.FloorToInt (mousePos.x);
				int mouseY = Mathf.FloorToInt (mousePos.y);

				if (mouseX < tileMap.mapX && mouseY >= 0 &&
						mouseY < tileMap.mapY && mouseX >= 0) {
						
						Tile targetTile = tileMap.getTile (mouseX, mouseY);

						if (Input.GetMouseButtonDown (0)) {
								queue.getActiveCharacter ().move (targetTile);
						}
				}

		}
}
