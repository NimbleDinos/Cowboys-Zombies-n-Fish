using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
	public ZombieAttack zombieHealth;
	public Units unitHealth;

	public float trapDamage = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
		zombieHealth = GetComponent<ZombieAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnCollisionEnter(Collision collision)
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Zombie")
		{
			zombieHealth.zombHealth -= trapDamage;

			//If the GameObject's name matches the one you suggest, output this message in the console
			Debug.Log("Do something here");
		}
		else if(other.tag == "UnitA")
		{
			unitHealth.health -= trapDamage;
		}
		else if (other.tag == "UnitB")
		{
			unitHealth.health -= trapDamage;
		}
	}
}
