using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cellaction : MonoBehaviour {
	//required energy to have before being fine to leave
	public int requiredpower = 10;
	//returns to the area start point
	public GameObject returnobj;
	//being looked at
	public bool acklook;
	//is the cell completed
	public bool celldone = false;
	//the power added by the cells
	public int addedpower =0;
	//list of turners

	//spawn location
	public Transform spawnloc;

	//rotation vars
	//default rotation
	private Vector3 defaultrotation;
	private Vector3 currentrotation;
	private Vector3 lastdirection;

	public bool dorotate = false;
	public bool gotvalues = false;

	public Vector3 raypoint;

	public virtual	void Awake()
	{
		defaultrotation = transform.rotation.eulerAngles;
			
	}

	public virtual void finishedcell()
	{
		returnobj.SetActive(true);
	}

	public virtual void Update()
	{
		if(playerinteraction.lookedatobj == transform.gameObject)
		{
			acklook = true;
		}
		else
		{
			acklook = false;

		}

		if(addedpower == requiredpower && !celldone)
		{
			celldone = true;
			finishedcell();
		}

		if(dorotate)
		{
			rotatecell();
		}
			
	}

	public virtual void getrotateready(Vector3 lookpoint)
	{
		if(!gotvalues)
		{		
			currentrotation = transform.rotation.eulerAngles;
			lastdirection = lookpoint - transform.position;
			gotvalues = true;
		}
	}

	public virtual void rotatecell()
	{
		Vector3 targetdir = raypoint - transform.position;

		Quaternion newdir = Quaternion.FromToRotation(lastdirection,targetdir);

		transform.rotation = newdir * transform.rotation;
		lastdirection = targetdir;

	}

	public virtual void leavecell()
	{


	}
}
