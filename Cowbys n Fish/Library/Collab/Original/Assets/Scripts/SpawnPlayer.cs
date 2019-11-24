using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer: MonoBehaviour
{
	public int teamACount = 0, teamBCount = 0;

	public GameObject nakedCowboyPrefab, fisherPrefab, fighterPrefab, builderPrefab;
	
	public Transform teamAHolder, teamBHolder;

	public Dictionary<long, GameObject> playerListAlive = new Dictionary<long, GameObject>();

	public int spawnRange = 4;
	
	public void SpawnNewPlayer(long playerID)
	{
		if (teamACount > teamBCount)
		{
			Vector2 randPosFloat = Random.insideUnitCircle * 4;
			Vector3Int playerPosInt = new Vector3Int((int)95 + (int)randPosFloat.x, 1, (int)25 + (int)randPosFloat.y);
			WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z] = Instantiate(nakedCowboyPrefab, playerPosInt, Quaternion.identity, teamBHolder);
			WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].GetComponent<Units>().playerID = playerID;
			WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].tag = "UnitB";
			playerListAlive.Add(playerID, WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z]);
            teamBCount++;
			//Spawn team b
		}
		else
		{
			Vector2 randPosFloat = Random.insideUnitCircle * 4;
			Vector3Int playerPosInt = new Vector3Int((int)5 + (int)randPosFloat.x, 1, (int)25 + (int)randPosFloat.y);
			WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z] = Instantiate(nakedCowboyPrefab, playerPosInt, Quaternion.identity, teamAHolder);
			WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].GetComponent<Units>().playerID = playerID;
			WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].tag = "UnitA";
			playerListAlive.Add(playerID, WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z]);
			teamACount++;
			//spawn team a
		}
	}

	public void ChangeRole(long playerID, string newClass)
	{
		if (playerListAlive.ContainsKey(playerID))
		{
			Vector3Int playerPosInt = new Vector3Int((int)playerListAlive[playerID].transform.position.x, 1, (int)playerListAlive[playerID].transform.position.z);
			string tag = WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].tag;
			playerListAlive.Remove(playerID);
			Destroy(WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z]);

			if (newClass == "fisher")
			{
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z] = Instantiate(fisherPrefab, playerPosInt, Quaternion.identity, teamAHolder);
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].GetComponent<Units>().playerID = playerID;
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].tag = tag;
				playerListAlive.Add(playerID, WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z]);
			}
			else if (newClass == "builder")
			{
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z] = Instantiate(builderPrefab, playerPosInt, Quaternion.identity, teamAHolder);
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].GetComponent<Units>().playerID = playerID;
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].tag = tag;
				playerListAlive.Add(playerID, WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z]);
			}
			else if (newClass == "fighter")
			{
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z] = Instantiate(fighterPrefab, playerPosInt, Quaternion.identity, teamAHolder);
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].GetComponent<Units>().playerID = playerID;
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].tag = tag;
				playerListAlive.Add(playerID, WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z]);
			}
			else if (newClass == "nakedCowboy")
			{
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z] = Instantiate(nakedCowboyPrefab, playerPosInt, Quaternion.identity, teamAHolder);
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].GetComponent<Units>().playerID = playerID;
				WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z].tag = tag;
				playerListAlive.Add(playerID, WorldHandler.tileObjects[playerPosInt.x, playerPosInt.z]);
			}
		}
		else
		{
			Debug.LogError("This player doesnt exist, cant assign role");
		}
	}

	public void MoveUnit(long playerID, int x, int y)
	{
		Units unit = playerListAlive[playerID].GetComponent<Units>();

		unit.Move(x, y);
	}

	public void PerformUnitAction(long playerID) 
	{
		RoleParentClass unitClass = playerListAlive[playerID].GetComponent<RoleParentClass>();

		unitClass.PerformAction();
	}
	
}
