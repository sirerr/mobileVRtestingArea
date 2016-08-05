using UnityEngine;
using System.Collections;

public class centraltest0 : centralaction{


	public override void Awake ()
	{

		rbody = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();

		base.Awake ();
	}
		
	public override void Update ()
	{
		base.Update ();
	}

	public override void becomeactive ()
	{
		base.becomeactive ();
	}

	public override void OnCollisionEnter (Collision col)
	{
		base.OnCollisionEnter (col);
	}

	public override void tohole (Transform holepoint)
	{
		base.tohole (holepoint);
	}

	public override void grabbed (Transform centercollect)
	{
		base.grabbed (centercollect);
	}

	public override void centralstateaction ()
	{
		base.centralstateaction ();
	}
}
