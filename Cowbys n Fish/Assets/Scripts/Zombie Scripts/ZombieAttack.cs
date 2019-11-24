using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
	public float attackDamage = 2.5f;
	public float enemyDistance;
	public float zombHealth = 4;

	public Vector3Int zombPosition;

	public Units unit;

	public BarricadeHealth barricadeHealth;

	public bool chooseTarget = false;

	// Start is called before the first frame update
	void Start()
    {
		zombPosition = new Vector3Int((int)transform.position.x, 1, (int)transform.position.z);
	}

	private void Update()
	{
	}
	public void Attack()
	{

		if (WorldHandler.tileObjects[zombPosition.x, zombPosition.z + 1] != null)
		{
			barricadeHealth = WorldHandler.tileObjects[zombPosition.x, zombPosition.z + 1].GetComponent<BarricadeHealth>();
			unit = WorldHandler.tileObjects[zombPosition.x, zombPosition.z + 1].GetComponent<Units>();
			if (barricadeHealth != null)
			{
				barricadeHealth.health -= attackDamage;
			}
			if(unit != null)
			{
				unit.health -= attackDamage;
			}
		}
		else if (WorldHandler.tileObjects[zombPosition.x, zombPosition.z - 1] != null)
		{
			barricadeHealth = WorldHandler.tileObjects[zombPosition.x, zombPosition.z - 1].GetComponent<BarricadeHealth>();
			unit = WorldHandler.tileObjects[zombPosition.x, zombPosition.z - 1].GetComponent<Units>();
			if (barricadeHealth != null)
			{
				barricadeHealth.health -= attackDamage;
			}
			if (unit != null)
			{
				unit.health -= attackDamage;
			}
		}
		else if (WorldHandler.tileObjects[zombPosition.x + 1, zombPosition.z] != null)
		{
			barricadeHealth = WorldHandler.tileObjects[zombPosition.x + 1, zombPosition.z].GetComponent<BarricadeHealth>();
			unit = WorldHandler.tileObjects[zombPosition.x + 1, zombPosition.z].GetComponent<Units>();
			if (barricadeHealth != null)
			{
				barricadeHealth.health -= attackDamage;
			}
			if (unit != null)
			{
				unit.health -= attackDamage;
			}
		}
		else if (WorldHandler.tileObjects[zombPosition.x -1, zombPosition.z] != null)
		{
			barricadeHealth = WorldHandler.tileObjects[zombPosition.x - 1, zombPosition.z].GetComponent<BarricadeHealth>();
			unit = WorldHandler.tileObjects[zombPosition.x - 1, zombPosition.z].GetComponent<Units>();
			if (barricadeHealth != null)
			{
				barricadeHealth.health -= attackDamage;
			}
			if (unit != null)
			{
				unit.health -= attackDamage;
			}
		}
	}
}
