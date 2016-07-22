using UnityEngine;
using System.Collections;

public class holecenterhitboxaction : MonoBehaviour {

	private bool occupied = false;

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("center") && !occupied)
		{
			if(col.GetComponent<centralaction>().isgrabbed)
			{
				occupied = true;
				col.transform.position = transform.position;
				col.transform.rotation = transform.rotation;
				col.transform.parent =null;
				col.GetComponent<centralaction>().setinplace = true;
				col.GetComponent<centralaction>().setinspot(transform);
			}
		}

	}

}
