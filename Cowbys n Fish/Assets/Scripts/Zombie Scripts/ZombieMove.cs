using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieMove : MonoBehaviour
{

	ZombieAttack zombie;
	private GameObject [] enemiesA, enemiesB, enemies;
	public float distance;
	private int target;
	public Vector3 direction;
	private Vector3 heading, lastloc;
	public string targetA, targetB;

	// Start is called before the first frame update
	void Start()
    {
		target = 0;
		distance = float.MaxValue;
		zombie = GetComponent<ZombieAttack>();
	}
	
	public void MoveZombie()
	{
		enemiesA = GameObject.FindGameObjectsWithTag(targetA);
		enemiesB = GameObject.FindGameObjectsWithTag(targetB);
		enemies = new GameObject[enemiesA.Length + enemiesB.Length];
		for (int i = 0; i < enemiesA.Length; i++)
		{
			enemies[i] = enemiesA[i];
		}

		for (int i = 0; i < enemiesB.Length; i++)
		{
			enemies[enemiesA.Length + i] = enemiesB[i];
		}

		search(enemies);
		calcDirection();
	}

	public void search(GameObject [] targets)
	{
		float discheck;
		for (int i = 0; i < targets.Length; i++)
		{
			discheck = Vector3.Distance(targets[i].transform.position, this.gameObject.transform.position);

			if (discheck < distance)
			{
				distance = discheck;
				target = i;
			}
		}
	}

	public void calcDirection()
	{
		heading = enemies[target].transform.position - this.gameObject.transform.position;
		distance = heading.magnitude;
		direction = heading / distance;


		lastloc = this.transform.position;

		if (direction.x < 0)
		{
			moveLeft();
		}
		else if (direction.x > 0)
		{
			moveRight();
		}
		else if(direction.z > 0)
		{
			moveUp();
		}
		else if (direction.z < 0)
		{
			moveDown();
		}


		if (lastloc == this.transform.position)
		{
			switch (Random.Range(0,4))
			{
				case 0:
					moveDown();
					break;
				case 1:
					moveLeft();
					break;
				case 2:
					moveRight();
					break;
				case 3:
					moveUp();
					break;
				default:
					break;
			}
		}
	}

	public void moveUp()
	{
		if (WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z + 1] == null)
		{
			WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z] = null;
			zombie.zombPosition.z += 1;
			transform.position = zombie.zombPosition;
			WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z] = gameObject;
		}
	}

	public void moveDown()
	{
		if (WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z - 1] == null)
		{
			WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z] = null;
			zombie.zombPosition.z -= 1;
			transform.position = zombie.zombPosition;
			WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z] = gameObject;
		}
	}

	public void moveRight()
	{
		if (WorldHandler.tileObjects[zombie.zombPosition.x + 1, zombie.zombPosition.z] == null)
		{
			WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z] = null;
			zombie.zombPosition.x += 1;
			transform.position = zombie.zombPosition;
			WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z] = gameObject;
		}
	}

	public void moveLeft()
	{
		if (WorldHandler.tileObjects[zombie.zombPosition.x - 1, zombie.zombPosition.z] == null)
		{
			WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z] = null;
			zombie.zombPosition.x -= 1;
			transform.position = zombie.zombPosition;
			WorldHandler.tileObjects[zombie.zombPosition.x, zombie.zombPosition.z] = gameObject;
		}
	}
}
