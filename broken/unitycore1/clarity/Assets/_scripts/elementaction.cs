using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class elementaction : MonoBehaviour {

	IGvrGazePointer gazepoint;

	public bool canuse = true;
	public bool captured = false;

	private Collider col;
	private Rigidbody rbody;

	public float elementpower = 0;


	void OnEnable()
	{
		col = GetComponent<Collider>();
		rbody = GetComponent<Rigidbody>();
	}

	public virtual void setloc(Transform par)
	{
		transform.parent = par;
		canuse = false;
		captured = true;
		col.enabled = false;
		rbody.isKinematic = true;

	}

 
	// Update is called once per frame
	void Update () {

	
	}
}
