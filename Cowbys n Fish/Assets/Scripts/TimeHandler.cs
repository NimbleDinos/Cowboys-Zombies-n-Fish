using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
	public static float zombieSpawnerTime = 60.0f;
	[Min(0.0f)]
	public static float timeTillZombieSpawn;

	public static float buffApplyTime = 60.0f;
	[Min(0.0f)]
	public static float timeTillApplyBuff;

	public static float inputReceiveTime = 5.0f;
	[Min(0.0f)]
	public static float timeTillReceiveInput;

	public static float respawnTime = 15.0f;
	[Min(0.0f)]
	public static float timeTillRespawn;

	public static List<ZombieMove> zombieHolder = new List<ZombieMove>();

	private void Start()
	{
		timeTillZombieSpawn = zombieSpawnerTime;
		timeTillReceiveInput = inputReceiveTime;
		timeTillApplyBuff = buffApplyTime;
		timeTillRespawn = respawnTime;
	}

	private void Update()
	{
		timeTillZombieSpawn -= Time.deltaTime;
		timeTillReceiveInput -= Time.deltaTime;
		timeTillApplyBuff -= Time.deltaTime;
		timeTillRespawn -= Time.deltaTime;

		if (timeTillReceiveInput <= 0.0f)
		{
			timeTillReceiveInput = inputReceiveTime;

			foreach (ZombieMove item in zombieHolder)
			{
				item.GetComponent<ZombieAttack>().Attack();
				item.MoveZombie();
			}
		}
	}
}
