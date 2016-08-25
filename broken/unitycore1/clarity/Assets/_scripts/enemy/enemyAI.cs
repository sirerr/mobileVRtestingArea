using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyAI : MonoBehaviour {

	private bool raycastobj = false;
	public enemystats statsref;
	public areaenemycontrol areacontrolref;
	public  List <Transform> pooltargets = new List<Transform>();
	public List <Transform> importanttargets = new List<Transform>();
	//
	public float movespeed = 5f;
	public float angularspeed = 100f;
	public float poolsafedistance = .5f;
	public float worldsafedistance = 1;
	public float playersafedistance = 3;


	// the states of the enemy
	public int enemystate=0;
	private int poolint =0;
	public bool busy = false;

	//enemy orientation
	private Quaternion startrotation;
	//raycasthit
	private RaycastHit hit;
	public Transform enemyfront;
//	private int emask = (1<< 8);
	private float TimePerPoolLocation =0;
	public float TimeLimitPerPoolLocation = 3;
	private float TimePerImportantLocation =0;
	public float TimeLimitPerImportantLocation = 2;


	void OnEnable()
	{
		statsref = GetComponent<enemystats>();
		poolsafedistance = Random.Range(5,20);
	//	print(poolsafedistance);
		startrotation = transform.rotation;

	}

	public void newlocation()
	{
		poolint = Random.Range(0,pooltargets.Count-1);
	}

	void Update()
	{
		switch (enemystate)
		{
		case 0:
			//start state

			if(importanttargets.Count ==0 && !busy)
			{
				movetopoolarea();
			}
			else if(importanttargets.Count>0 && !busy)
			{
				enemystate = 1;
			}else if(busy)
			{
				enemystate = 5;
			}
		
			break;
		case 1:
			//important points
			movetoimportantobject();

			break;
		case 2:
			enemybyobject();
			//action on important object
			break;
		case 3:
			//player point
			//move to player point
			break;
		case 4:
		//	print("back to 0");
			TimePerImportantLocation+= Time.deltaTime;
			transform.LookAt(importanttargets[0].position);
			//will wait for a few seconds then make the enemy go somewhere else
			if(TimePerImportantLocation>TimeLimitPerImportantLocation)
			{
				if(areaenemycontrol.importantobjs.Find(obj=>obj.gameObject == importanttargets[0]) != null)
				{
					areaenemycontrol.importantobjs.Remove(importanttargets[0].gameObject);
				}
				importanttargets.Remove(importanttargets[0]);
				print("taken care of");
				enemystate = 0;
			}
			break;
		case 5:
			if(!busy)
			{
				enemystate =0;
			}
			break;
		case 10:

	
		//	print("in 10 state");
			//paused, not moving at all
			//animation only
			break;

		}
	}


	public void enemybyobject()
	{
		switch(importanttargets[0].tag)
		{
		case "element":
			elementchange();
			break;
		case "corecenter":
			centerchange();
			break;
		}
	}


	public void movetoimportantobject()
	{
		float distance = Vector3.Distance(transform.position,importanttargets[0].position);
		elementaction eleref = importanttargets[0].GetComponent<elementaction>();

		if(distance>worldsafedistance && (eleref.purestate && !eleref.captured))
		{
			transform.LookAt(importanttargets[0].position);
			transform.position = Vector3.MoveTowards(transform.position,importanttargets[0].position,movespeed *Time.deltaTime);

			if(busy)
			{
				enemystate =5;
				return;
			}
		
		}else
		{
			enemystate =2;
		}

	}


	public void elementchange()
	{
		elementaction eleref = importanttargets[0].GetComponent<elementaction>();

		if(eleref.purestate)
		{
			eleref.corruptelement();
			TimePerImportantLocation = 0;
			enemystate =4;
		}else
		{
			enemystate =0;
			if(areaenemycontrol.importantobjs.Find(obj=>obj.gameObject == importanttargets[0]) != null)
			{
				areaenemycontrol.importantobjs.Remove(importanttargets[0].gameObject);
			}
			importanttargets.Remove(importanttargets[0]);
			print("already pure!");
		}
	}

	public void centerchange()
	{}

	public void movetopoolarea()
	{
		float distance = Vector3.Distance(transform.position,pooltargets[poolint].position);

		if(distance>poolsafedistance)
		{
			transform.LookAt(pooltargets[poolint].position);
			transform.position = Vector3.MoveTowards(transform.position,pooltargets[poolint].position,movespeed *Time.deltaTime);
		}else
		{
			TimePerPoolLocation +=Time.deltaTime;
		//	transform.rotation =startrotation;
			if(TimePerPoolLocation>TimeLimitPerPoolLocation)
			{

				TimePerPoolLocation =0;
				if(poolint ==pooltargets.Count -1)
				{
					poolint =0;
				}else
				{
					int temppoolint  = Random.Range(0,pooltargets.Count-1);
					poolint = temppoolint;
				}

			}
		}
	}



}
