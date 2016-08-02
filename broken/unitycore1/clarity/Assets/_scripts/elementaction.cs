using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class elementaction : MonoBehaviour {

	IGvrGazePointer gazepoint;

	public bool canuse = true;
	public bool captured = false;
	public bool purestate = true;

	private Collider col;
	private Rigidbody rbody;

	public float elementpower = 0;

	public bool acklook = false;
	public float vel = 5f;

	void OnEnable()
	{
		col = GetComponent<Collider>();
		rbody = GetComponent<Rigidbody>();
	}

	public virtual void setloc(Transform par)
	{
		playerinteraction.playerstate = 0;
		transform.parent = par;
		canuse = false;
		captured = true;
		col.enabled = false;
		rbody.isKinematic = true;

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
		//print("whats wrong");
		transform.position = point.position;
		GetComponent<Collider>().enabled =false;
		GetComponent<MeshRenderer>().enabled =false;
		GetComponent<Rigidbody>().isKinematic = true;

	}
		
	public virtual void letloose(Transform par, Vector3 loc)
	{
		transform.LookAt(loc);
		rbody.velocity = new Vector3(0,0,vel);
		transform.position = loc;
	//	transform.parent = par;
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;

	}
}
