using UnityEngine;
using System.Collections;

public class holelocaction : MonoBehaviour {

	public bool occupied = false;

	public bool acklook = false;


	public virtual void OnCollisionEnter(Collision col)
	{
		if(col.transform.CompareTag("corecenter") && col.transform.GetComponent<centralaction>().centralstate ==2)
		{
			col.rigidbody.isKinematic = true;
			col.transform.position = transform.position;
			occupied = true;
			print(col.transform.name);
		}
	
	}

	public virtual void Update()
	{
		// will do things


		if(playerinteraction.lookedatobj == transform.gameObject)
		{
			acklook = true;
		}
		else
		{
			acklook = false;
		}

	}
}
