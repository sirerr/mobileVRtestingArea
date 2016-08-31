using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class areaenemycontrol : MonoBehaviour {

	//poolarea
	public GameObject poolareaobj;
	public List <Transform> poolareas = new List<Transform>();
	public int poolareacount = 5;
	private float randomXmin = -300;
	private float randomXmax = 300;
	private float randomymin = -300;
	private float randomymax = 210;
	private float randomZmin = -700;
	private float randomZmax = 700;

	//enemy object
	public List <GameObject> enemylist = new List<GameObject>();
	public List <enemyaction> enemyactionref = new List<enemyaction>();
	public Transform enemyspawnpoint;
	public int enemycount = 1;
	public GameObject enemyobj;

	//important locations
	/// <summary>
	/// these used to be static locations
	/// </summary>
	public  List<GameObject> importantobjs = new List<GameObject>();
	public  int importantobjcount = 0;


	public bool newfind = false;
	public int enemycallamount = 5;
	private float calltimer = 0;
	public float calltimercount =5;
	private bool readytocall = false;
	private bool called = false;

	//events for later
	public delegate void deathofenemy(int enemynum);
	public static event deathofenemy atdeath;

	public delegate void foundobj(Transform obj);
	public static event foundobj foundimportantobj;

	// Update is called once per frame
	public void Update () {

		importantobjcount = importantobjs.Count -1;

		if(readytocall)
		{
			if(!called)
			{
				called = true;
				talktoenemies();
				print(called);
			}

			calltimer += Time.deltaTime;

			if(calltimer>calltimercount)
			{
			//	print(calltimer);
				calltimer =0;
				called = false;
			//	print(called);
			}
		}

		if(importantobjs.Count>0 && newfind)
		{
			//
			newfind = false;
			enemiesgocorrupt();
		}


	}

	public void enemiesgocorrupt()
	{
		if(enemylist.Count>=enemycallamount)
		{
			for(int a =0;a<enemycallamount;a++)
			{
				int i = Random.Range(0,enemylist.Count);
				if(enemylist[i].GetComponent<enemyAI>().enemystate ==0)
				{
					enemylist[i].GetComponent<enemyAI>().importanttargets.Add(importantobjs[importantobjs.Count-1].transform);
				}else
				{
				//	a--;
				}

		
			}
		}
		else
		{
			for(int a =0;a<enemylist.Count;a++)
			{
				
				if(enemylist[a].GetComponent<enemyAI>().enemystate ==0)
				{
					enemylist[a].GetComponent<enemyAI>().importanttargets.Add(importantobjs[importantobjs.Count-1].transform);	
				}
			}
		}



	}

	public void talktoenemies()
	{
		int randomenemy = Random.Range(0,enemylist.Count -1);
	//	print("enemy # "+ randomenemy + " " + enemylist[randomenemy] );

		enemyactionref[randomenemy].makechildactive = true;
	
	}

	// Use this for initialization
	void Awake () {

		for(int i=1;i<=poolareacount;i++)
		{
			Vector3 vec3;
			vec3.x = Random.Range(randomXmin,randomXmax);
			vec3.y = Random.Range(randomymin,randomymax);
			vec3.z = Random.Range(randomZmin,randomZmax);

			GameObject enemypoolarea = Instantiate(poolareaobj,transform.position,transform.rotation) as GameObject;
			enemypoolarea.transform.SetParent(transform);
			enemypoolarea.transform.localPosition = vec3;
			poolareas.Add(enemypoolarea.transform);
	//		print("made and moved");
		}

		StartCoroutine(enemyreleasewait());
	}

	IEnumerator enemyreleasewait()
	{
		yield return new WaitForSeconds(2f);

		for(int i =1;i<=enemycount;i++)
		{
			releaseenemies(i);
			yield return new WaitForSeconds(.3f);
		}
		yield return new WaitForSeconds(1f);
		readytocall = true;

	}

	private void releaseenemies(int ecount)
	{
		//test code

			GameObject tester = Instantiate(enemyobj,enemyspawnpoint.position,Quaternion.identity) as GameObject;
			enemylist.Add(tester);
			tester.GetComponent<enemystats>().currentarea = transform;
			tester.GetComponent<enemyaction>().enemycontrolref = GetComponent<areaenemycontrol>();
			tester.GetComponent<enemyAI>().areacontrolref = GetComponent<areaenemycontrol>();
			enemyactionref.Add(tester.GetComponent<enemyaction>());
			for(int a =0;a<poolareas.Count;a++)
			{
				tester.GetComponent<enemyAI>().pooltargets.Add(poolareas[a]);
			}
			tester.GetComponent<enemyAI>().newlocation();


		//test code
	}

}
