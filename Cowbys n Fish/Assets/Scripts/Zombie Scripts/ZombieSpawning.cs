using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawning : MonoBehaviour
{
	public float zombieSpawnRange = 10.0f;
	public int zombieSpawnAmount = 10;

	public GameObject zombiePrefab;
	public Transform zombieHolder;


	private void Start()
	{
		zombieHolder = GameObject.FindGameObjectWithTag("ZombieHolder").transform;

		SpawnZombiesAtPoint();
	}

	private void Update()
	{
		if (TimeHandler.timeTillZombieSpawn <= 0.0f)
		{
			Debug.Log("Spawning Zombies Now");
			TimeHandler.timeTillZombieSpawn = TimeHandler.zombieSpawnerTime;
			SpawnZombiesAtPoint();
		}
	}

	private void SpawnZombiesAtPoint()
	{
		Vector2Int spawnPos = new Vector2Int((int)transform.position.x, (int)transform.position.z);
		for (int i = 0; i < zombieSpawnAmount; i++)
		{
			Vector2 zombiePositionFloat = spawnPos + Random.insideUnitCircle * zombieSpawnRange;

			Vector3Int zombiePosInt = new Vector3Int((int)zombiePositionFloat.x, 1, (int)zombiePositionFloat.y);

			if (zombiePosInt.x < WorldHandler.worldXSize && zombiePosInt.y < WorldHandler.worldZSize && zombiePosInt.x >= 0 && zombiePosInt.y >= 0)
			{
				if (WorldHandler.tileObjects[zombiePosInt.x, zombiePosInt.z] == null)
				{
					//TODO spawn zombie
					WorldHandler.tileObjects[zombiePosInt.x, zombiePosInt.z] = Instantiate(zombiePrefab, zombiePosInt, Quaternion.identity, zombieHolder);
					TimeHandler.zombieHolder.Add(WorldHandler.tileObjects[zombiePosInt.x, zombiePosInt.z].GetComponent<ZombieMove>());
					Debug.Log("Spawned Zombie");
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
