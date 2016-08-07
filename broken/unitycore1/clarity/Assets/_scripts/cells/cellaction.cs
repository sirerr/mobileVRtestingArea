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
	public List<GameObject> turners = new List<GameObject>();
	//spawn location
	public Transform spawnloc;
	//
	private GameObject currentmover;
	public GameObject centerobj;


	void Awake()
	{
		int counter = 0;
		for(int i =0; i<transform.childCount;i++)
		{
			if(transform.GetChild(i).CompareTag("movers"))
			{
				turners.Add( transform.GetChild(i).gameObject);
				counter++;
			}
		}
			
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
