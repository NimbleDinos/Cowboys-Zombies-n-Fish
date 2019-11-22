using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
	public static float zombieSpawnerTime = 60.0f;
	[Min(0.0f)]
	public static float timeTillZombieSpawn;

	public static float inputReceiveTime = 15.0f;
	[Min(0.0f)]
	public static float timeTillReceiveInput;

	private void Start()
	{
		timeTillZombieSpawn = zombieSpawnerTime;
		timeTillReceiveInput = inputReceiveTime;
	}

	private void Update()
	{
		timeTillZombieSpawn -= Time.deltaTime;
		timeTillReceiveInput -= Time.deltaTime;
	}
}
