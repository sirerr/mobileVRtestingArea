using UnityEngine;
using System.Collections;

public class centralaction : MonoBehaviour {

	//elements added to the central object
	public int elementadded;
	// power needed to be complete
	public float neededpower=0;
	//power collected by the center object
	public float collectedpower = 0;

	//for central mode
	public bool fullpower = false;


	public Rigidbody rbody;
	public Collider col;

	public int centralstate =0;
	public float forceamount =5f;


	void Awake()
	{
	//	createelementhitlocs();
	
		rbody = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
	

	}

	public virtual void centralstateaction()
	{
		if(centralstate == 0)
		{
			becomeactive();
		}

		if(centralstate ==1)
		{
			
		}
	}



		
	public virtual void becomeactive()
	{
		centralstate =1;
		rbody.velocity = Vector3.zero;
		print("working");
		//also expand the circle around it and make it look good
	}
		

	public virtual void poweredup()
	{
		//do action when fully powered
	}

	public virtual void Update()
	{

		if(collectedpower == neededpower)
		{
			fullpower = true;
			poweredup();
		}
	}

}
