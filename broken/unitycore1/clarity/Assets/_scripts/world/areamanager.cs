using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class areamanager : MonoBehaviour {

	// player will always start at jump point 0

	private areaenemycontrol enemycontrolref;

	public int parentlevelint;
	public int areaint;

	public int currentjumpoint = 0;

	public int completearealevelamount =0;
	public int completearealevelamountlimit =50;

	public List <GameObject> jumplocs = new List<GameObject>();
	public List <Transform> celllocations = new List<Transform>();
	public List <bool> celllocationfilled = new List<bool>();
	public GameObject cellobj;
	private int cellspawnamount =0;
	public int cellspawnamountlimit =5;
	public int childcountint =0;

	public bool areaclear = false;

	public GameObject helperobj;
	public Transform helperlocation;

	void Awake()
	{
		enemycontrolref = GetComponent<areaenemycontrol>();
		gmanager.currentarea = transform.gameObject;

		//create help object, used in testing help object
		GameObject help = Instantiate(helperobj,helperlocation.position,Quaternion.identity) as GameObject;
		help.GetComponent<attackhelperaction>().areamanagerref = GetComponent<areamanager>();
		help.GetComponent<attackhelperaction>().areaenemyref =GetComponent<areaenemycontrol>();
		help.GetComponent<attackhelperaction>().enabled = true;
		//help.GetComponent<attackhelperaction>().startpos.position = helperlocation.position;


		//creating jumps and spawns
		childcountint = transform.childCount;
		for(int i =0; i<childcountint;i++)
		{
			if(transform.GetChild(i).CompareTag("jumppoint"))
			{
				jumplocs.Add(transform.GetChild(i).gameObject);
				jumplocs[i].GetComponent<jumppointaction>().objlistlocationint = i;
			}

			if(transform.GetChild(i).CompareTag("cellspawn"))
			{
				//print("working");
				celllocations.Add(transform.GetChild(i));
				Transform celllister = celllocations[celllocations.Count-1];
				int chooseint = Random.Range(-1,2);
				if((chooseint ==1 || chooseint ==0) && cellspawnamount<cellspawnamountlimit)
				{
					GameObject cell = Instantiate(cellobj,celllister.position,Quaternion.identity) as GameObject;
					cell.GetComponent<cellaction>().areamanagerref = GetComponent<areamanager>();
					cellspawnamount++;
					celllocationfilled.Add(true);
				}else if(chooseint ==-1 || chooseint ==2)
				{
					celllocationfilled.Add(false);
				}
					
			}
		}
			// double check the count in case there aren't enough
			for(int u =0;u<celllocations.Count;u++)
			{
			if(cellspawnamount<cellspawnamountlimit && !celllocationfilled[u])
				{
				GameObject cell = Instantiate(cellobj,celllocations[u].position,Quaternion.identity) as GameObject;
				cell.GetComponent<cellaction>().areamanagerref = GetComponent<areamanager>();
				cellspawnamount++;
				celllocationfilled.Add(true);
				}
			}

	
	}
		

	void Update()
	{

		if(completearealevelamount>=completearealevelamountlimit && !areaclear)
		{
			areaclear = true;
			enemycontrolref.destroyallenemies();
		}
	}



}
