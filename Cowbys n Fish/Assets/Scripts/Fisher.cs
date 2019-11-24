using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisher : RoleParentClass
{
	public int gainedFish;

	public bool isFishing = false;
	
	public float distanceToBucket;

	public FishBucket fishpool;

	public WaterLevels nearestLevel;

	public WorldHandler world;

	private Units unit;

	// Start is called before the first frame update
	void Start()
    {
		unit = GetComponent<Units>();
		world = FindObjectOfType<WorldHandler>();

		if (tag == "UnitA")
		{
			fishpool = world.fishBucketA;
		}
		else
		{
			fishpool = world.fishBucketB;
		}
    }

    // Update is called once per frame
    void Update()
    {
		distanceToBucket = Vector3.Distance(transform.position, fishpool.gameObject.transform.position);
    }

	public void Fishing()
	{
		if (GetValidFishingSpot())
		{		
			isFishing = true;
			Debug.Log("Fishing Valid");
		}
		else
		{
			nearestLevel = null;
		}

		if (isFishing == true)
		{
			if (GetValidFishingSpot())
			{
				gainedFish+=nearestLevel.fishAmount;
				isFishing = false;
			}
			else
			{
				isFishing = false;
			}	
		}
	}

	private bool GetValidFishingSpot()
	{
		if (WorldHandler.worldGenTileHolder[unit.position.x, unit.position.z + 1] == 1)
		{
			nearestLevel = WorldHandler.tileObjects[unit.position.x, unit.position.z + 1].GetComponent<WaterLevels>();
			return true;
		}
		else if (WorldHandler.worldGenTileHolder[unit.position.x, unit.position.z - 1] == 1)
		{
			nearestLevel = WorldHandler.tileObjects[unit.position.x, unit.position.z - 1].GetComponent<WaterLevels>();
			return true;
		}
		else if (WorldHandler.worldGenTileHolder[unit.position.x + 1, unit.position.z] == 1)
		{
			nearestLevel = WorldHandler.tileObjects[unit.position.x + 1, unit.position.z].GetComponent<WaterLevels>();
			return true;
		}
		else if (WorldHandler.worldGenTileHolder[unit.position.x - 1, unit.position.z] == 1)
		{
			nearestLevel = WorldHandler.tileObjects[unit.position.x - 1, unit.position.z].GetComponent<WaterLevels>();
			return true;
		}

		return false;
	}

	public void dumpFish()
	{
		if( distanceToBucket < 2 && Input.GetKeyDown(KeyCode.E))
		{
			fishpool.FishInPool += gainedFish;
			gainedFish = 0;
		}
	}

	public override void PerformAction()
	{
		base.PerformAction();

		Fishing();
		dumpFish();
	}
}
