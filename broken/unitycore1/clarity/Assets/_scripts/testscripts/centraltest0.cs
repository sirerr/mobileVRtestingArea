using UnityEngine;
using System.Collections;

public class centraltest0 : centralaction{


	void Awake()
	{
		createelementhitlocs();
		rbody = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();

	}


	public override void grabbed (Transform looker)
	{
		base.grabbed (looker);


	}

	public override void letgo ()
	{
		base.letgo ();

	}

	public override void setinspot (Transform par)
	{
		base.setinspot (par);
	}

	public override void Update ()
	{
		base.Update ();
	}
}
