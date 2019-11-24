using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : RoleParentClass
{
	//public int FishInPool;
	public int Barricade;
	public int Fish;

	public int barricadeDistance;

	public FishBucket fishPool;
	public GameObject barricadeObject;

	public Units unit;

	public Vector3 spawnPosForward;
	public Vector3 spawnPosBehind;
	public Vector3 spawnPosLeft;
	public Vector3 spawnPosRight;

	// Start is called before the first frame update
	void Start()
    {
		Fish = fishPool.FishInPool;

		unit = GetComponent<Units>();
		
		spawnPosForward = unit.position + unit.playerDirection;

		spawnPosLeft = unit.position - unit.transform.right;

		spawnPosBehind = unit.position - unit.playerDirection;

		spawnPosRight = unit.position + unit.transform.right;
	}

	public void buyBarricade()
	{
		if (fishPool.FishInPool >= 10)
		{
			Debug.LogError("Buying");
			Barricade++;
			fishPool.FishInPool -= 10;
		}
	}

	public void placeBarricade(string direction)
	{
		if (Barricade >= 1 && direction == "up")
		{
			Barricade--;
			Instantiate(barricadeObject, spawnPosForward , Quaternion.identity);
		}
		else if (Barricade >= 1 && direction == "left")
		{
			Barricade--;
			Instantiate(barricadeObject, spawnPosLeft, Quaternion.identity);
		}
		else if (Barricade >= 1 && direction == "down")
		{
			Barricade--;
			Instantiate(barricadeObject, spawnPosBehind, Quaternion.identity);
		}
		else if (Barricade >= 1 && direction == "right")
		{
			Barricade--;
			Instantiate(barricadeObject, spawnPosRight, Quaternion.identity);
		}
	}

	public  void PerformBuildAction(string buildable, string direction)
	{
		Debug.Log("Building Zone Reached");

		if (buildable == "barricade")
		{
			buyBarricade();

			placeBarricade(direction);
		}
	}
}
