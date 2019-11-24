using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

	public GameObject bucketA;
	public GameObject bucketB;

	public GameObject[] unitAs;
	public GameObject[] unitBs;

	public bool gainBuffA;
	public bool gainBuffB;
	public bool gainDebuffA;
	public bool gainDebuffB;

	public Units unit;

    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Buff()
	{
		if (bucketA.GetComponent<FishBucket>().FishInPool > 5)
		{
			gainBuffA = true;
		}
		if (bucketB.GetComponent<FishBucket>().FishInPool > 5)
		{
			gainBuffB = true;
		}
		if (bucketA.GetComponent<FishBucket>().FishInPool < 2)
		{
			gainDebuffA = true;
		}
		if (bucketB.GetComponent<FishBucket>().FishInPool > 5)
		{
			gainDebuffB = true;
		}
	}
}
