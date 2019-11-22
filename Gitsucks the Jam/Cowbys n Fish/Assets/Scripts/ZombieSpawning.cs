using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawning : MonoBehaviour
{
	public GameObject[] zombieSpawnPoints;
	public float zombieSpawnRangeDefault = 10.0f;
	public float[] zomebieSpawnRange;
	public int zombieSpawnAmountDefault = 10;
	public int[] zomebieSpawnAmount;

	private void Update()
	{
		if (TimeHandler.timeTillZombieSpawn <= 0.0f)
		{
			TimeHandler.timeTillZombieSpawn = TimeHandler.zombieSpawnerTime;
			SpawnZombies();
		}
	}

	private void SpawnZombies()
	{
		for (int i = 0; i < zombieSpawnPoints.Length; i++)
		{
			float usedSpawnRange;
			if (zomebieSpawnRange[i] != 0)
			{
				usedSpawnRange = zomebieSpawnRange[i];
			}
			else
			{
				usedSpawnRange = zombieSpawnRangeDefault;
			}

			int usedSpawnAmount;
			if (zomebieSpawnRange[i] != 0)
			{
				usedSpawnAmount = zomebieSpawnAmount[i];
			}
			else
			{
				usedSpawnAmount = zombieSpawnAmountDefault;
			}

			SpawnZombiesAtPoint(i, usedSpawnRange, usedSpawnAmount);
		}
	}

	private void SpawnZombiesAtPoint(int point, float distance, int amount)
	{
		Vector2Int spawnPos = new Vector2Int((int)zombieSpawnPoints[point].transform.position.x, (int)zombieSpawnPoints[point].transform.position.z);
		for (int i = 0; i < amount; i++)
		{
			Vector2 zombiePositionFloat = spawnPos + Random.insideUnitCircle * distance;

			Vector2Int zombiePosInt = new Vector2Int((int)zombiePositionFloat.x, (int)zombiePositionFloat.y);

			if (zombiePosInt.x < WorldHandler.worldXSize && zombiePosInt.y < WorldHandler.worldZSize)
			{
				if (WorldHandler.tileObjects[zombiePosInt.x, zombiePosInt.y] == null)
				{
					//TODO spawn zombie
				}
				else
				{
					Debug.Log("Position Not Valid");
				}
			}
			else
			{
				Debug.Log("Position out of world bounds");
			}
		}
	}
}
