﻿using UnityEngine;
using System;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class TileMap : MonoBehaviour
{

		// Height and width of the TileMap.
		public int mapX;
		public int mapY;

		// The CSV file that tells the tilemap the data of each tile.
		public TextAsset terrainCSV;
		
		// A 2D array representing the TileMap to store each tile's terrain data.
		private Tile[,] tiles;

		public void Start ()
		{
				// Build tiles
				tiles = new Tile[mapX, mapY];

				createTiles ();
				setTileOccupants ();

				// Build the Mesh
				BuildMesh ();				
		}
	
		public void BuildMesh ()
		{
				// Get number of Triangles
				int numTiles = mapX * mapY;
				int numTris = numTiles * 2;

				// Get number of vertexes
				int vertX = mapX + 1;
				int vertY = mapY + 1;
				int numVerts = vertX * vertY;

				// Generate mesh data
				Vector3[] vertices = new Vector3[numVerts];
				Vector3[] normals = new Vector3[numVerts];
				Vector2[] uv = new Vector2[numVerts];
				int[] triangles = new int[numTris * 3];

				// Fill mesh data
				for (int y = 0; y < vertY; y++) {
						for (int x = 0; x < vertX; x++) {
								vertices [y * vertX + x] = new Vector3 (x, y, 0);
								normals [y * vertX + x] = Vector3.up;
								uv [y * vertX + x] = new Vector2 ((float)x / mapX, (float)y / mapY);
						}
				}
				
				// Set triangles
				for (int y = 0; y < mapY; y++) {
						for (int x = 0; x < mapX; x++) {
								int squareIndex = y * mapX + x;
								int triOffset = squareIndex * 6;

								triangles [triOffset + 0] = y * vertX + x + 1;
								triangles [triOffset + 1] = y * vertX + x;
								triangles [triOffset + 2] = y * vertX + x + vertX;
		
								triangles [triOffset + 3] = y * vertX + x + 1;
								triangles [triOffset + 4] = y * vertX + x + vertX;
								triangles [triOffset + 5] = y * vertX + x + vertX + 1;
						}
				}

				// Create a new Mesh and populate with the data
				Mesh mesh = new Mesh ();
				mesh.vertices = vertices;
				mesh.triangles = triangles;
				mesh.normals = normals;
				mesh.uv = uv;
		
				// Assign the mesh to the filter.
				MeshFilter meshFilter = GetComponent<MeshFilter> ();

				meshFilter.mesh = mesh;
		}

		private void createTiles ()
		{
				// Gets each line of the CSV.
				string[] lines = terrainCSV.text.Split ("\n" [0]);
		
				for (int y = 0; y < mapY; y++) {
						string[] values = lines [mapY - 1 - y].Split (',');
						for (int x = 0; x < mapX; x++) {
								Vector3 pos = new Vector3 (x, y, -1.0f);
								int terrain = Convert.ToInt32 (values [x]);
								tiles [x, y] = new Tile (pos, terrain);
						}
				}	
		}

		// Set the occupants of each Tile.
		private void setTileOccupants ()
		{
				Character[] charObjects = FindObjectsOfType<Character> ();

				for (int i = 0; i < charObjects.Length; i++) {
						int x = Mathf.FloorToInt (charObjects [i].transform.position.x);
						int y = Mathf.FloorToInt (charObjects [i].transform.position.y);
					
						tiles [x, y].setOccupant (charObjects [i]);
				}
		}

		// Used when asking for data of a single tile.
		public Tile getTile (int x, int y)
		{
				return tiles [x, y];
		}		
}


	
