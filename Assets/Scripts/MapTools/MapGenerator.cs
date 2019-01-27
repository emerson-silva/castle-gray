using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public const int TILE_COMMON_GROUND = 0;
	public const int TILE_COMMON_WALL = 1;
	public const int TILE_DESTROYABLE_WALL = 2;
	public const int TILE_SPAWN_PLAYER = 3;
	public const int TILE_SPAWN_COMMON_ENEMY = 4;
	public const int TILE_SPAWN_ROGUE_ENEMY = 5;
	public const int TILE_SPAWN_MAGE_ENEMY = 6;
	public const int TILE_SPAWN_WARRIOR_ENEMY = 7;
	public const int TILE_NEXT_FASE = 8;

	public Vector2 mapSize;
	public ArrayList tiles = new ArrayList();

	public GameObject[] walls;

	public Transform communGround;
	public Transform communWall;
	public Transform destroyableWall;

	public Transform player;
	public Transform commonEnemy;
	public Transform rogueEnemy;
	public Transform mageEnemy;
	public Transform warriorEnemy;
	public Transform nextFase;

	private System.Random random;

	private int[,] mapConfig = new int[,] {
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 3, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 4, 1},
		{1, 0, 0, 0, 0, 1, 1, 1, 2, 1, 0, 1, 0, 0, 1, 0, 1, 1},
		{1, 0, 0, 1, 0, 0, 2, 1, 4, 0, 0, 0, 0, 0, 1, 0, 0, 1},
		{1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 2, 1, 0, 0, 1, 0, 1},
		{1, 0, 2, 4, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 1, 1, 0, 1},
		{1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 8, 1, 2, 1},
		{1, 0, 0, 0, 0, 1, 4, 2, 0, 1, 0, 0, 1, 1, 4, 2, 0, 1},
		{1, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 2, 0, 0, 8, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
	};

	// Use this for initialization
	void Start ()
	{
		//should i remove this var??? Probably yes!
		random = new System.Random();
		//GenerateMapRandomly();
		GenerateMapBasedOnProperties();
	}

	private void GenerateMapRandomly()
	{
		Debug.Log("Generating Map Randomly...");
		Transform tile;
		int tileNum;

		//ToDo: make this better

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
		Debug.Log("Generating Map Complete");
	}

	private void GenerateMapBasedOnProperties()
	{
		Debug.Log("Generating Map based on properties...");

		Vector2 mapDimension = new Vector2(mapConfig.GetLength(0), mapConfig.GetLength(1));

		for (int x=0; x<mapDimension.x; x++) {
			for (int y=0; y<mapDimension.y; y++) {
				Vector3 tilePosition = new Vector3(-mapSize.y/2 + 0.5f + y, -mapDimension.x/2 + 0.5f + x, 0);
				int objectCode = mapConfig[x,y];
				InstantiateTile(objectCode, tilePosition);
			}
		}
		Debug.Log("Generating Map Complete");
	}

	private void InstantiateTile(int objectCode, Vector3 tilePosition) {
		Transform objectToInstantiate = communGround;
		switch (objectCode) {
			case TILE_COMMON_WALL:
				objectToInstantiate = communWall;
				break;
			case TILE_DESTROYABLE_WALL:
				objectToInstantiate = destroyableWall;
				break;
			default:
				InstantiateObject(objectToInstantiate, tilePosition);
				tilePosition.z = -1f;
				switch(objectCode) {
					case TILE_SPAWN_COMMON_ENEMY:
						objectToInstantiate = commonEnemy;
						break;
					case TILE_SPAWN_MAGE_ENEMY:
						objectToInstantiate = mageEnemy;
						break;
					case TILE_SPAWN_ROGUE_ENEMY:
						objectToInstantiate = rogueEnemy;
						break;
					case TILE_SPAWN_WARRIOR_ENEMY:
						objectToInstantiate = warriorEnemy;
						break;
					case TILE_SPAWN_PLAYER:
						objectToInstantiate = player;
						break;
					default:
						return;
				}
				break;
		}
		InstantiateObject(objectToInstantiate, tilePosition);
	}

	private void InstantiateObject (Transform objectToInstantiate, Vector3 position)
	{
		Transform newTile = Instantiate(objectToInstantiate, position, Quaternion.identity) as Transform;
		tiles.Add(newTile);
	}

}
