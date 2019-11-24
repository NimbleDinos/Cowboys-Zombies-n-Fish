using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : RoleParentClass
{

	public GameObject teamAUnit;
	public GameObject teamBUnit;

	public float attackDamage = 5;

	public float unitDistance;

	public Units thisUnit;

	public Units unit;
	public BarricadeHealth barricadeHealth;

	private void Start()
	{
		thisUnit = GetComponent<Units>();
	}

	public void Attack()
	{
		if (WorldHandler.tileObjects[thisUnit.position.x, thisUnit.position.z + 1] != null)
		{
			barricadeHealth = WorldHandler.tileObjects[thisUnit.position.x, thisUnit.position.z + 1].GetComponent<BarricadeHealth>();
			unit = WorldHandler.tileObjects[thisUnit.position.x, thisUnit.position.z + 1].GetComponent<Units>();
			if (barricadeHealth != null)
			{
				barricadeHealth.health -= attackDamage;
			}
			if (unit != null)
			{
				if (unit.tag != tag)
				{
					unit.health -= attackDamage;
				}
			}
		}

		if (WorldHandler.tileObjects[thisUnit.position.x, thisUnit.position.z - 1] != null)
		{
			barricadeHealth = WorldHandler.tileObjects[thisUnit.position.x, thisUnit.position.z - 1].GetComponent<BarricadeHealth>();
			unit = WorldHandler.tileObjects[thisUnit.position.x, thisUnit.position.z - 1].GetComponent<Units>();
			if (barricadeHealth != null)
			{
				barricadeHealth.health -= attackDamage;
			}
			if (unit != null)
			{
				if (unit.tag != tag)
				{
					unit.health -= attackDamage;
				}
			}
		}

		if (WorldHandler.tileObjects[thisUnit.position.x + 1, thisUnit.position.z] != null)
		{
			barricadeHealth = WorldHandler.tileObjects[thisUnit.position.x + 1, thisUnit.position.z].GetComponent<BarricadeHealth>();
			unit = WorldHandler.tileObjects[thisUnit.position.x + 1, thisUnit.position.z].GetComponent<Units>();
			if (barricadeHealth != null)
			{
				barricadeHealth.health -= attackDamage;
			}
			if (unit != null)
			{
				if (unit.tag != tag)
				{
					unit.health -= attackDamage;
				}
			}
		}

		if (WorldHandler.tileObjects[thisUnit.position.x - 1, thisUnit.position.z] != null)
		{
			barricadeHealth = WorldHandler.tileObjects[thisUnit.position.x - 1, thisUnit.position.z].GetComponent<BarricadeHealth>();
			unit = WorldHandler.tileObjects[thisUnit.position.x - 1, thisUnit.position.z].GetComponent<Units>();
			if (barricadeHealth != null)
			{
				barricadeHealth.health -= attackDamage;
			}
			if (unit != null)
			{
				if (unit.tag != tag)
				{
					unit.health -= attackDamage;
				}	
			}
		}
	}

	public override void PerformAction()
	{
		base.PerformAction();

		Attack();
	}
}
