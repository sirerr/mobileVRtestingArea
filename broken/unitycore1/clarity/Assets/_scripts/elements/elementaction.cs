using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class elementaction : MonoBehaviour {

	IGvrGazePointer gazepoint;

	//captured by player at all
	public bool captured = false;

	// positive or negative energy
	public bool purestate = true;

	public Material puremat;
	public Material badmat;

	private Collider col;
	private Rigidbody rbody;
	private MeshRenderer meshren;

	public int elementpower = 0;

	public bool acklook = false;
	public float vel = 5f;

	void OnEnable()
	{
		col = GetComponent<Collider>();
		rbody = GetComponent<Rigidbody>();
		meshren = GetComponent<MeshRenderer>();

		if(!purestate)
		{
			meshren.material = badmat;
		}
		else
		{
			meshren.material = puremat;
		}
	}

 
	// Update is called once per frame
	public virtual	void Update () {

		if(playerinteraction.lookedatobj == transform.gameObject)
		{
			acklook = true;
		}
		else
		{
			acklook = false;
		}
	
	}

	public virtual void collected(Transform point)
	{
		if(purestate)
		{
			transform.position = point.position;

			col.enabled = false;
			meshren.enabled = false;
			rbody.isKinematic = true;
		
			captured =true;
		}

	}

	public virtual void cleanelement()
	{
		if(!purestate)
		{
			purestate = true;
			meshren.material = puremat;
		}
	}
		
	public virtual void letloose(Vector3 par)
	{
		Vector3 dir = (par - transform.position) * vel;
		rbody.velocity = dir;

		col.enabled = true;
		meshren.enabled = true;
		rbody.isKinematic = false;

	}
}
