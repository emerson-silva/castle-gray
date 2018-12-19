using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public Vector2 mapSize;
	public ArrayList tiles = new ArrayList();

	public Transform communGround;

	public GameObject[] walls;

	private System.Random random;

	// Use this for initialization
	void Start ()
	{
		random = new System.Random();
		GenerateMap();
	}

	private void GenerateMap()
	{
		Debug.Log("Generating Map");
		Transform tile;
		int tileNum;

		for (int x=0 ; x<mapSize.x; x++) {
			for (int y=0; y<mapSize.y; y++) {
				tileNum = random.Next(walls.Length+1);
				if (tileNum==0) {
					tile = communGround.transform;
				} else {
					tile = (walls.GetValue(tileNum-1) as GameObject).transform;
				}
				Vector3 tilePosition = new Vector3(-mapSize.x/2 + 0.5f + x, -mapSize.y/2 + 0.5f + y, 0);
				Transform newTile = Instantiate(tile, tilePosition, Quaternion.identity) as Transform;
				tiles.Add(newTile);
			}
		}
		Debug.Log("Generating Map");
	}

}
