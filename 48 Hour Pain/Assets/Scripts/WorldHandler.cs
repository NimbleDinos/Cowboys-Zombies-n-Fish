using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
	public int[,] worldGenTileHolder;
	public static GameObject[,] tileObjects; //If object assigned then tile is full, otherwise tile is empty

	public static int worldXSize = 100;
	public static int worldZSize = 100;

	public int baseHeight = 15;
	public int baseWidth = 15;

	[Range(0.0f, 1.0f)]
	public float waterChance;
	public int waterSmoothIterations = 3;
	public int waterWidth = 15;
	public int waterHeight = 10;

	public GameObject groundPrefab, waterPrefab;

	private void Start()
	{
		tileObjects = new GameObject[worldXSize, worldZSize];
		worldGenTileHolder = new int[worldXSize, worldZSize];

		SpawnWorld();
	}

	private void SpawnWorld()
	{
		SpawnTopBase();
		SpawnBottomBase();
		SpawnWater();
	}

	private void SpawnTopBase()
	{
		for (int i = 0; i < worldXSize; i++)
		{
			for (int j = worldZSize - (baseHeight + 1); j < worldZSize; j++)
			{
				Vector3Int tilePos = new Vector3Int(i, 0, j);
				Instantiate(groundPrefab, tilePos, Quaternion.identity);
			}
		}
	}

	private void SpawnBottomBase()
	{
		for (int i = 0; i < worldXSize; i++)
		{
			for (int j = 0; j < baseHeight + 1; j++)
			{
				Vector3Int tilePos = new Vector3Int(i, 0, j);
				Instantiate(groundPrefab, tilePos, Quaternion.identity);
			}
		}
	}
	
	private void SpawnWater()
	{
		for (int i = waterWidth; i < worldXSize - waterWidth; i++)
		{
			for (int j = baseHeight + 1 + waterHeight; j < worldZSize - (baseHeight + 1 + waterHeight); j++)
			{
				if (worldGenTileHolder[i, j] == 0)
				{
					float waterRand = Random.Range(0.0f, 1.0f);
					if (waterRand <= waterChance)
					{
						worldGenTileHolder[i, j] = 1; //water tile
					}
				}
			}
		}

		for (int k= 0; k < waterSmoothIterations; k++)
		{
			for (int i = 0; i < worldXSize; i++)
			{
				for (int j = baseHeight + 1; j < worldZSize - (baseHeight + 1); j++)
				{
					SmoothWater(i, j);
				}
			}
		}
		
		for (int i = 0; i < worldXSize; i++)
		{
			for (int j = baseHeight + 1; j < worldZSize - (baseHeight + 1); j++)
			{
				Vector3Int tilePos = new Vector3Int(i, 0, j);

				if (worldGenTileHolder[i, j] == 0)
				{
					Instantiate(groundPrefab, tilePos, Quaternion.identity);
				}
				else if (worldGenTileHolder[i, j] == 1)
				{
					tileObjects[i,j] = Instantiate(waterPrefab, tilePos, Quaternion.identity);
				}
			}
		}
	}

	private void SmoothWater(int x, int z)
	{
		int surroundingTiles = GetSurroundingTiles(x, z);

		if (surroundingTiles < 4)
		{
			worldGenTileHolder[x, z] = 0;
		}
		else if (surroundingTiles > 4)
		{
			worldGenTileHolder[x, z] = 1;
		}
	}

	private int GetSurroundingTiles(int x, int z)
	{
		int neighbours = 0;

		for (int i = x - 1; i <= x + 1; i++)
		{
			for (int j = z - 1; j <= z + 1; j++)
			{
				if (!(i == x && j == z))
				{
					if (i >= 0 && i < worldXSize && j >= 0 && j < worldZSize)
					{
						if (worldGenTileHolder[i, j] == 1)
						{
							neighbours++;
						}
					}
					else
					{
						neighbours++;
					}
				}
			}
		}

		return neighbours;
	}

	//TODO assign initial positions of world objects


}
