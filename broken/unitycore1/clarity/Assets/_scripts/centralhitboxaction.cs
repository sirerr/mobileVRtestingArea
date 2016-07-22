using UnityEngine;
using System.Collections;

public class centralhitboxaction : MonoBehaviour {

	private bool occupied = false;
	private Transform par;

	void OnEnable()
	{
		par = transform.parent;

	}

	void OnTriggerEnter(Collider col)
	{
		print("element entered the trigger");

		if(col.CompareTag("element") && !occupied)
		{
			if(col.GetComponent<elementaction>().captured)
			{
				occupied = true;
				col.transform.position = transform.position;
			//	col.transform.rotation = transform.rotation;
				col.transform.parent =null;
				col.GetComponent<elementaction>().setloc(transform.parent);
				par.GetComponent<centralaction>().elementaddedcounter++;
				par.GetComponent<centralaction>().collectedpower += col.GetComponent<elementaction>().elementpower;
			}
		}

	}
}
