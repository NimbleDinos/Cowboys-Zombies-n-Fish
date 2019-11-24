using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
	public static int[,] worldGenTileHolder;
	public static GameObject[,] tileObjects; //If object assigned then tile is full, otherwise tile is empty
	public Transform terrainHolder, teamAHolder, teamBHolder;
	public FishBucket fishBucketA, fishBucketB;
	public BuffManager buff;

	public static int worldXSize = 100;
	public static int worldZSize = 50;

	public int baseHeight = 10;
	public int baseWidth = 25;

	[Range(0.0f, 1.0f)]
	public float waterChance;
	public int waterSmoothIterations = 3;
	public int waterHeight = 1;

	public GameObject groundPrefab, waterPrefab, wallPrefab, fishBucketPrefab, zombieSpawnLocationPrefab, unitPrefab;

	public Material waterNearMat, waterMidMat, waterFarMat;

	private void Start()
	{
		tileObjects = new GameObject[worldXSize, worldZSize];
		worldGenTileHolder = new int[worldXSize, worldZSize];

		SpawnWorld();
	}

	private void SpawnWorld()
	{
		SpawnWall();
		SpawnTopBase();
		SpawnBottomBase();
		SpawnWater();

		//Spawn zombie spawners
		{
			Vector3Int zombieSpawnLocation = new Vector3Int(worldXSize / 2, 1, worldZSize / 2);
			Instantiate(zombieSpawnLocationPrefab, zombieSpawnLocation, Quaternion.identity);
		}

		//SpawnUnits();
	}

	private void SpawnUnits()
	{
		//Spawn Fisher for team b
		{
			Vector3Int tilePos = new Vector3Int(worldXSize - (baseHeight / 2) - 1, 1, (baseWidth / 2));
			tileObjects[tilePos.x, tilePos.z] = Instantiate(unitPrefab, tilePos, Quaternion.identity, teamBHolder);
			tileObjects[tilePos.x, tilePos.z].tag = "UnitB";
			tileObjects[tilePos.x, tilePos.z].GetComponent<Fisher>().fishpool = fishBucketB;
		}
	}

	private void SpawnWall()
	{
		//Top
		for (int i = 0; i < worldXSize; i++)
		{
			Vector3Int tilePos = new Vector3Int(i, 1, worldZSize - 1);
			tileObjects[tilePos.x, tilePos.z] = Instantiate(wallPrefab, tilePos, Quaternion.identity, terrainHolder);
		}

		//Bottom
		for (int i = 0; i < worldXSize; i++)
		{
			Vector3Int tilePos = new Vector3Int(i, 1, 0);
			tileObjects[tilePos.x, tilePos.z] = Instantiate(wallPrefab, tilePos, Quaternion.identity, terrainHolder);
		}

		//Left
		for (int i = 0; i < worldZSize; i++)
		{
			Vector3Int tilePos = new Vector3Int(0, 1, i);
			tileObjects[tilePos.x, tilePos.z] = Instantiate(wallPrefab, tilePos, Quaternion.identity, terrainHolder);
		}

		//Right
		for (int i = 0; i < worldZSize; i++)
		{
			Vector3Int tilePos = new Vector3Int(worldXSize - 1, 1, i);
			tileObjects[tilePos.x, tilePos.z] = Instantiate(wallPrefab, tilePos, Quaternion.identity, terrainHolder);
		}
	}

	private void SpawnTopBase()
	{
		for (int i = 0; i < worldZSize; i++)
		{
			for (int j = worldXSize - (baseHeight + 1); j < worldXSize; j++)
			{
				Vector3Int tilePos = new Vector3Int(j, 0, i);
				Instantiate(groundPrefab, tilePos, Quaternion.identity, terrainHolder);
			}
		}
		
		for (int i = 0; i < baseWidth; i++)
		{
			for (int j = worldXSize - (baseHeight + 1); j < worldXSize; j++)
			{
					if ((i == baseWidth - 1) || (j == worldXSize - (baseHeight + 1)) && (i < (((int)baseWidth / 2) - 1) || (i > (((int)baseWidth / 2) + 1))) && tileObjects[j, i] == null)
				{
					Vector3Int tilePos = new Vector3Int(j, 1, i);
					tileObjects[j, i] = Instantiate(wallPrefab, tilePos, Quaternion.identity, terrainHolder);
				}
			}
		}

		//Spawn Fish Bucket
		{
			Vector3Int tilePos = new Vector3Int(worldXSize - (baseHeight / 2), 1, (baseWidth / 2));
			tileObjects[tilePos.x, tilePos.z] = Instantiate(fishBucketPrefab, tilePos, Quaternion.identity, teamBHolder);
			tileObjects[tilePos.x, tilePos.z].tag = "BucketB";
			fishBucketB = tileObjects[tilePos.x, tilePos.z].GetComponent<FishBucket>();
			buff.bucketB = tileObjects[tilePos.x, tilePos.z];
		}
	}

	private void SpawnBottomBase()
	{
		for (int i = 0; i < worldZSize; i++)
		{
			for (int j = 0; j < baseHeight + 1; j++)
			{
				Vector3Int tilePos = new Vector3Int(j, 0, i);
				Instantiate(groundPrefab, tilePos, Quaternion.identity, terrainHolder);
			}
		}


		for (int i = 0; i < baseWidth; i++)
		{
			for (int j = 0; j < baseHeight + 1; j++)
			{
				if (((i == baseWidth - 1) || (j == baseHeight)) && (i < (((int)baseWidth/2) - 1) || (i > (((int)baseWidth / 2) + 1))) && tileObjects[j, i] == null)
				{
					Vector3Int tilePos = new Vector3Int(j, 1, i);
					tileObjects[j, i] = Instantiate(wallPrefab, tilePos, Quaternion.identity, terrainHolder);
				}
			}
		}

		//Spawn Fish Bucket
		{
			Vector3Int tilePos = new Vector3Int(baseHeight/2, 1, (baseWidth / 2));
			tileObjects[tilePos.x, tilePos.z] = Instantiate(fishBucketPrefab, tilePos, Quaternion.identity, terrainHolder);
			tileObjects[tilePos.x, tilePos.z].tag = "BucketA";
			fishBucketA = tileObjects[tilePos.x, tilePos.z].GetComponent<FishBucket>();
			buff.bucketA = tileObjects[tilePos.x, tilePos.z];
		}
	}
	
	private void SpawnWater()
	{
		for (int i = 0; i < worldZSize; i++)
		{
			for (int j = baseHeight + 1 + waterHeight; j < worldXSize - (baseHeight + 1 + waterHeight); j++)
			{
				if (worldGenTileHolder[j, i] == 0)
				{
					float waterRand = Random.Range(0.0f, 1.0f);
					if (waterRand <= waterChance)
					{
						worldGenTileHolder[j, i] = 1; //water tile
					}
				}
			}
		}

		for (int k= 0; k < waterSmoothIterations; k++)
		{
			for (int i = 0; i < worldZSize; i++)
			{
				for (int j = baseHeight + 1; j < worldXSize - (baseHeight + 1); j++)
				{
					SmoothWater(j, i);
				}
			}
		}

		for (int i = 0; i < worldZSize; i++)
		{
			for (int j = baseHeight + 1; j < worldXSize - (baseHeight + 1); j++)
			{
				Vector3Int tilePos = new Vector3Int(j, 0, i);

				if (worldGenTileHolder[j, i] == 0)
				{
					Instantiate(groundPrefab, tilePos, Quaternion.identity, terrainHolder);
				}
				else if (worldGenTileHolder[j, i] == 1)
				{
					tileObjects[j,i] = Instantiate(waterPrefab, tilePos, Quaternion.identity, terrainHolder);
					WaterLevels wl = tileObjects[j, i].GetComponent<WaterLevels>();

					if (j < 25)
					{
						wl.fishAmount = 1;
						wl.mr.material = waterNearMat;
					}
					else if (j < 40)
					{
						wl.fishAmount = 3;
						wl.mr.material = waterMidMat;
					}
					else if (j < 60)
					{
						wl.fishAmount = 6;
						wl.mr.material = waterFarMat;
					}
					else if (j < 75)
					{
						wl.fishAmount = 3;
						wl.mr.material = waterMidMat;
					}
					else if (j <= 100)
					{
						wl.fishAmount = 1;
						wl.mr.material = waterNearMat;
					}

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
	   
}
