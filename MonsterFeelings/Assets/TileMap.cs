using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class TileMap : MonoBehaviour
{

		public int mapX;
		public int mapY;
		public float tileSize;		
		public TextAsset tileDataSource;
		
		private int[,] tileData;

		public void Start ()
		{
				// Build TileData
				tileData = new int[mapX, mapY];
				setTileData ();

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
								vertices [y * vertX + x] = new Vector3 (x * tileSize, y * tileSize, 0);
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
		
				// Assign the mesh to the filter and collider
				MeshFilter meshFilter = GetComponent<MeshFilter> ();
				MeshCollider meshCollider = GetComponent<MeshCollider> ();
		
				meshFilter.mesh = mesh;
				meshCollider.sharedMesh = mesh;

				Debug.Log ("Done Mesh!");
		}

		private void setTileData ()
		{
				string[] lines = tileDataSource.text.Split ("\n" [0]);
				
				// Get Data from CSV
				for (int y = 0; y < lines.Length - 1; y++) {
						string[] row = lines [y].Split (',');
						for (int x = 0; x < row.Length; x++) {
								tileData [x, mapY - 1 - y] = Convert.ToInt32 (row [x]);
						}
				}				
		}

		public int getTileData (int x, int y)
		{
				return tileData [x, y];
		}

		public float getTileSize ()
		{
				return tileSize;
		}

		
}


	
