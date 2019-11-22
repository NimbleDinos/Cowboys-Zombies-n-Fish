using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
	//public int FishInPool;
	public int Barricade;

	public int Fish;

	public FishBucket fishPool;
	public GameObject barricadeObject;


	// Start is called before the first frame update
	void Start()
    {
		Fish = fishPool.FishInPool;
	}

    // Update is called once per frame
    void Update()
    {
		buyBarricade();
		placeBarricade();
	}

	public void buyBarricade()
	{
		if (fishPool.FishInPool >= 10 && Input.GetKeyDown(KeyCode.P))
		{
			Debug.LogError("Buying");
			Barricade++;
			fishPool.FishInPool -= 10;
		}
	}

	public void placeBarricade()
	{
		if (Barricade >= 1 && Input.GetKeyDown(KeyCode.L))
		{
			Barricade--;
			Instantiate(barricadeObject, transform.position, Quaternion.identity);
		}
	}
}
