using UnityEngine;
using System.Collections;

public class jumppointaction : MonoBehaviour {

	public int objlistlocationint = 0;
	public Transform childpoint;

	private Transform parenttrans;

	void Awake()
	{
		childpoint = transform.GetChild(0).transform;	
		parenttrans = transform.parent;
	}

	public virtual Vector3 playerhere()
	{
		
		parenttrans.GetComponent<areamanager>().currentjumpoint = objlistlocationint;
		gmanager.lastjumplocation = childpoint.position;
		gmanager.lastjumprotation = childpoint.rotation;

		return childpoint.position;
	}

	public Quaternion rotateplayer()
	{

		return childpoint.rotation;
	}
}
