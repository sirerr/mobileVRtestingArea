using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class areamanager : MonoBehaviour {

	// player will always start at jump point 0

	public List <Transform> poolareas = new List<Transform>();
	public Transform enemyspawnpoint;

	public int parentlevelint;
	public int areaint;

	public int currentjumpoint = 0;

	public List <GameObject> jumplocs = new List<GameObject>();

	public int childcountint =0;

	void Awake()
	{
		childcountint = transform.childCount;
		for(int i =0; i<childcountint;i++)
		{
			if(transform.GetChild(i).CompareTag("jumppoint"))
			{
				jumplocs.Add(transform.GetChild(i).gameObject);
				jumplocs[i].GetComponent<jumppointaction>().objlistlocationint = i;
			}
		}
	}

 


}
