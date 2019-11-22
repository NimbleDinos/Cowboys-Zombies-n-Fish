using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fisher : MonoBehaviour
{
	public int gainedFish;

	public bool isFishing = false;

	public GameObject fisher;
	public GameObject pond;

	public float distance;
	public float distance2;

	public FishBucket fishpool;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		distance = Vector3.Distance(fisher.transform.position, pond.transform.position);
		distance2 = Vector3.Distance(fisher.transform.position, fishpool.gameObject.transform.position);

		Fishing();
		dumpFish();
    }

	public IEnumerator Fishing()
	{
		if ( distance < 2 && Input.GetKeyDown(KeyCode.F))
		{
			isFishing = true;
		}

		if (isFishing == true)
		{
			yield return new WaitForSeconds(5);
			gainedFish++;
			isFishing = false;
		}
	}

	public void dumpFish()
	{
		if( distance2 < 2 && Input.GetKeyDown(KeyCode.E))
		{
			fishpool.FishInPool += gainedFish;
			gainedFish = 0;
		}
	}
}
