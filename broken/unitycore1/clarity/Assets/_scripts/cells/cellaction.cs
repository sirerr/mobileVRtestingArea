using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cellaction : MonoBehaviour {
	//required energy to have before being fine to leave
	public int requiredpower = 0;
	//returns to the area start point
	public GameObject returnobj;
	//being looked at
	public bool acklook;
	//
	public bool celldone = false;
	//
	public int addedpower =0;

	public List<GameObject> turners = new List<GameObject>();

	public Transform spawnloc;

	void Awake()
	{
		
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
	}

	public virtual void rotatecell()
	{


	}
}
