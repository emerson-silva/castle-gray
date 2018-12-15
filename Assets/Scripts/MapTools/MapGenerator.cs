using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public Vector2 mapSize;
	public Transform tilePrefab;
	public ArrayList tiles = new ArrayList();

	// Use this for initialization
	void Start ()
	{
		GenerateMap();
	}

	private void GenerateMap()
	{
		Debug.Log("Generating Map");
		for (int x=0 ; x<mapSize.x; x++) {
			for (int y=0; y<mapSize.y; y++) {
				Vector3 tilePosition = new Vector3(-mapSize.x/2 + 0.5f + x, -mapSize.y/2 + 0.5f + y, 0);
				Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as Transform;
				tiles.Add(newTile);
			}
		}
		Debug.Log("Generating Map");
	}
	
}
