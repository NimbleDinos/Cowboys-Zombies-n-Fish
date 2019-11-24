using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Units : MonoBehaviour
{
	public float health = 5;

	public int moveDistance = 20;

	public Vector3Int position;
	public Vector3 playerDirection;

	public SpawnPlayer spawnPlayer;

	public long playerID;
    public string playerName;

	public BuffManager buff;

	public TextMeshProUGUI nameText;

	// Start is called before the first frame update
	void Start()
    {
		position = new Vector3Int((int)transform.position.x, 1, (int)transform.position.z);

		playerDirection = transform.forward;

		buff = FindObjectOfType<BuffManager>();

		nameText.text = playerName;
	}

    // Update is called once per frame
    void Update()
    {
		Move(moveDistance);

		if (buff.gainBuffA == true && tag == "UnitA" && TimeHandler.timeTillApplyBuff <= 0.0f)
		{
			health += 2;
		}
		if (buff.gainBuffB == true && tag == "UnitB" && TimeHandler.timeTillApplyBuff <= 0.0f)
		{
			health += 2;
		}
		if (buff.gainDebuffA == true && tag == "UnitA" && TimeHandler.timeTillApplyBuff <= 0.0f && health>= 3)
		{
			health -= 2;
		}
		if (buff.gainDebuffB == true && tag == "UnitA" && TimeHandler.timeTillApplyBuff <= 0.0f && health >= 3)
		{
			health -= 2;
		}


		if (health <= 0)
		{
			spawnPlayer.playerListAlive.Remove(playerID);
			spawnPlayer.playerListAlive.Add(playerID, null);
			PlayerDetails pd = new PlayerDetails();
			pd.playerID = playerID;
			pd.playerName = playerName;
			pd.tag = tag;

			if (tag == "UnitA")
			{
				spawnPlayer.teamACount--;
			}

			if (tag == "UnitB")
			{
				spawnPlayer.teamBCount--;
			}

			spawnPlayer.deadPlayers.Add(pd);
			WorldHandler.tileObjects[(int)transform.position.x, (int)transform.position.z] = null;
			Destroy(gameObject);
		}
	}

	public void Move(int distance)
	{
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (WorldHandler.tileObjects[position.x, position.z + distance] == null)
			{
				WorldHandler.tileObjects[position.x, position.z] = null;
				position.z += distance;
				transform.position = position;
				WorldHandler.tileObjects[position.x, position.z] = gameObject;
			}	
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (WorldHandler.tileObjects[position.x, position.z - distance] == null)
			{
				WorldHandler.tileObjects[position.x, position.z] = null;
				position.z -= distance;
				transform.position = position;
				WorldHandler.tileObjects[position.x, position.z] = gameObject;
			}
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (WorldHandler.tileObjects[position.x - distance, position.z] == null)
			{
				WorldHandler.tileObjects[position.x, position.z] = null;
				position.x -= distance;
				transform.position = position;
				WorldHandler.tileObjects[position.x, position.z] = gameObject;
			}
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (WorldHandler.tileObjects[position.x + distance, position.z] == null)
			{
				WorldHandler.tileObjects[position.x, position.z] = null;
				position.x += distance;
				transform.position = position;
				WorldHandler.tileObjects[position.x, position.z] = gameObject;
			}
		}
	}

	public void Move(int x, int y)
	{
		float dist = Vector2Int.Distance(new Vector2Int(position.x, position.z), new Vector2Int(x, y));

		Debug.LogError("NewX: " + x + "; NewY: " + y);
		Debug.LogError("MyX: " + position.x + "; MyY: " + position.z);

		if (dist <= moveDistance)
		{
			if (WorldHandler.tileObjects[x, y] == null)
			{
				WorldHandler.tileObjects[position.x, position.z] = null;
				position.x = x;
				position.z = y;
				transform.position = position;
				WorldHandler.tileObjects[position.x, position.z] = gameObject;
				Debug.LogError("Moving");
			}
			else
			{
				Debug.LogError("Tile is occupied");
			}
		}
		//TODO raycast nearest valid pos if target isnt clear
	}
}
