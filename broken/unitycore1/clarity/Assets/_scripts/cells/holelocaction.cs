using UnityEngine;
using System.Collections;

public class holelocaction : MonoBehaviour {

	//if it's occuppied
	public bool occupied = false;
	//being looked at
	public bool acklook = false;
	//
	public int power =0;

	public virtual void OnCollisionEnter(Collision col)
	{
		if(col.transform.CompareTag("corecenter") && col.transform.GetComponent<centralaction>().centralstate ==2)
		{
			col.rigidbody.isKinematic = true;
			col.transform.position = transform.position;
			occupied = true;
			power+= col.transform.GetComponent<centralaction>().collectedpower;
			transform.parent.GetComponent<cellaction>().addedpower+=power;
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
