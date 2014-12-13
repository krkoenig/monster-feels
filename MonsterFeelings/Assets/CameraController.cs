using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

		private float leftBound;
		private float topBound;
		private float bottomBound;
		private float rightBound;
		
		private float sizeX;
		private float sizeY;
		
		const int moveWidth = 20;
		const float moveSpeed = 0.5f;
				
		// Use this for initialization
		void Start ()
		{
				GameObject obj = GameObject.Find ("TileMap");
				TileMap tilemap = obj.GetComponent<TileMap> ();
				
				sizeX = (float)tilemap.mapX;
				sizeY = (float)tilemap.mapY;
		
				calcBounds ();
		
				Vector3 pos = new Vector3 (0.0f, 0.0f, transform.position.z);
				pos.x = Mathf.Clamp (pos.x, leftBound, rightBound);
				pos.y = Mathf.Clamp (pos.y, bottomBound, topBound);
				transform.position = pos;
		
		}
	
		// Update is called once per frame
		void Update ()
		{
				calcBounds ();
		
				Vector3 pos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				
				if ((Input.mousePosition.y >= Screen.height - moveWidth && Input.mousePosition.y <= Screen.height) || Input.GetKey (KeyCode.UpArrow)) {
						pos.y += moveSpeed;
				} 
				if ((Input.mousePosition.y <= moveWidth && Input.mousePosition.y >= 0) || Input.GetKey (KeyCode.DownArrow)) {
						pos.y -= moveSpeed;
				}
				if ((Input.mousePosition.x <= moveWidth && Input.mousePosition.x >= 0) || Input.GetKey (KeyCode.LeftArrow)) {
						pos.x -= moveSpeed;
				}
				if ((Input.mousePosition.x >= Screen.width - moveWidth && Input.mousePosition.x <= Screen.width) || Input.GetKey (KeyCode.RightArrow)) {
						pos.x += moveSpeed;
				}
				
				pos.x = Mathf.Clamp (pos.x, leftBound, rightBound);
				pos.y = Mathf.Clamp (pos.y, bottomBound, topBound);
				transform.position = pos;
		}

		public void moveToActive (Vector3 pos)
		{
				calcBounds ();
		
				pos.z = transform.position.z;
		
				pos.x = Mathf.Clamp (pos.x, leftBound, rightBound);
				pos.y = Mathf.Clamp (pos.y, bottomBound, topBound);
				transform.position = pos;
		}
		
		private void calcBounds ()
		{
		
				float vertExtent = Camera.main.orthographicSize; 
				float horzExtent = vertExtent * Screen.width / Screen.height;
		
				leftBound = horzExtent;
				rightBound = sizeX - horzExtent;
				bottomBound = vertExtent;
				topBound = sizeY - vertExtent;
		}
	
		void OnLevelWasLoaded ()
		{
				Start ();
		}
}
