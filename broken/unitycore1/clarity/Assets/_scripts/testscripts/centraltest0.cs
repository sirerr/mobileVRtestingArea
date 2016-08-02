using UnityEngine;
using System.Collections;

public class centraltest0 : centralaction{


	void Awake()
	{
		
		rbody = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();

	}
		

	public override void Update ()
	{
		base.Update ();
	}


}
