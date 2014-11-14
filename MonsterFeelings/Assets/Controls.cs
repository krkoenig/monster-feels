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
				
				queue = new Queue ();
		}
	
		// Update is called once per frame
		void Update ()
		{			
				int[] mouse = getMouseLoc ();
				
				int skill = -1;

				if (Input.GetKey (KeyCode.Alpha1)) {
						skill = 0;
				} else if (Input.GetKey (KeyCode.Alpha2)) {
						skill = 1;
				} else if (Input.GetKey (KeyCode.Alpha3)) {
						skill = 2;
				} else if (Input.GetKey (KeyCode.Alpha4)) {
						skill = 3;
				} else if (Input.GetKey (KeyCode.Alpha5)) {
						skill = 4;
				} else if (Input.GetKey (KeyCode.Alpha6)) {
						skill = 5;
				} else if (Input.GetKeyDown (KeyCode.Space)) {
						queue.getActiveCharacter ().endTurn ();
						queue.nextCharacter ();
				}

				if (skill != -1) {
						queue.getActiveCharacter ().showSkill (skill);
				}


				if (isOnScreen (mouse [0], mouse [1])) {
						
						Tile targetTile = tileMap.getTile (mouse [0], mouse [1]);

						if (Input.GetMouseButton (0)) {
								queue.getActiveCharacter ().move (targetTile);
						} else if (Input.GetMouseButton (1) && queue.getActiveCharacter ().isSkillShown () != -1) {
								queue.getActiveCharacter ().useSkill (queue.getActiveCharacter ().isSkillShown (), targetTile);
						}
				}			
		}

		public int[] getMouseLoc ()
		{
				// Mouse location
				// Convert the mouse's screen coordinates to world coordinates.
				Vector3 mousePos = Input.mousePosition;
				mousePos.z = 1.0f;
				mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		
				// Convert them into int's.
				int mouseX = Mathf.FloorToInt (mousePos.x);
				int mouseY = Mathf.FloorToInt (mousePos.y);

				return new int[2] {mouseX, mouseY};
		}

		private bool isOnScreen (int mouseX, int mouseY)
		{
				if (mouseX < tileMap.mapX && mouseY >= 0 &&
						mouseY < tileMap.mapY && mouseX >= 0) {
						return true;
				}
				return false;
		}
}
