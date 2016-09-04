using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cellmakeraction : MonoBehaviour {

	private float gathertimer =0;
	public float gathertimerlimit =10;
	private float gathercooldown =0;
	public float gathercooldownlimit =10;
	public List <GameObject> childobjs = new List<GameObject>();

	public bool allgathered = false;
	public bool startgather = true;
	public bool readytogather = false;

	public void gatherchildren()
	{
		if(!allgathered)
		{
			for(int i =0;i<transform.childCount;i++)
			{

				childobjs.Add(transform.GetChild(i).gameObject);
			}
		}
		allgathered = true;
		startgather = true;
		StartCoroutine(gatherchildrenup());

	}

	IEnumerator gatherchildrenup()
	{
		yield return new WaitForSeconds(gathertimerlimit);

	}

	void Update()
	{
		if(startgather)
		{
			
		}
	}

}
