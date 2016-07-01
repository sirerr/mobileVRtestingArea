using UnityEngine;
using System.Collections;

public class centralaction : MonoBehaviour {

	public int attachpoints =0;

	public float attackpower = 0;


	void OnTriggerEnter(Collider col)
	{
		print("element entered the trigger");

		if(col.CompareTag("element"))
		{
			if(col.GetComponent<elementaction>().captured)
			{
				Vector3 point = col.ClosestPointOnBounds(transform.position);
				col.transform.position = point;
				col.GetComponent<elementaction>().canuse = false;
			}
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
