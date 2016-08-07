using UnityEngine;
using System.Collections;

public class cellcenteraction : MonoBehaviour {

	public void quickraycast()
	{

		if(Physics.Raycast(transform.position,transform.InverseTransformVector(Vector3.forward),Mathf.Infinity,1<<LayerMask.NameToLayer("movers")))
		{
			
		}
	}
}
