using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

	public GameObject teamAUnit;
	public GameObject teamBUnit;

	public float damage = 5;
	public float unitDistance;

	public Units unit;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        unitDistance = Vector3.Distance(teamAUnit.transform.position, teamBUnit.transform.position);

		Attack();
	}

	public void Attack()
	{
		if(unit.tag != tag)
		{
			if (Input.GetKeyDown(KeyCode.A) && unitDistance < 3)
			{
				unit.health -= damage + Random.Range(-0.5f, 0.5f);
			}
		}
	}
}
