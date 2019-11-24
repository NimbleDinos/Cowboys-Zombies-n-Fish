using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
	public float health = 5;

	public int moveDistance = 1;

	public Vector3Int position;
	public Vector3 playerDirection;

	public long playerID;
    public string playerName;

	Quaternion playerRotation;

	public BuffManager buff;

	// Start is called before the first frame update
	void Start()
    {
		position = new Vector3Int((int)transform.position.x, 1, (int)transform.position.z);

		playerDirection = transform.forward;

		playerRotation = transform.rotation;

		buff = FindObjectOfType<BuffManager>();
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
			FindObjectOfType<SpawnPlayer>().playerList.Remove(playerID);
			FindObjectOfType<SpawnPlayer>().playerList.Add(playerID, null);
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
		if (Vector2Int.Distance(new Vector2Int(position.x, position.z), new Vector2Int(x,y)) <= moveDistance)
		{
			if (WorldHandler.tileObjects[x, y] == null)
			{
				WorldHandler.tileObjects[position.x, position.z] = null;
				position.x = x;
				position.z = y;
				transform.position = position;
				WorldHandler.tileObjects[position.x, position.z] = gameObject;
			}
		}
		//TODO raycast nearest valid pos if target isnt clear
	}
}
