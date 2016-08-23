using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyAI : MonoBehaviour {

	private bool raycastobj = false;
	public enemystats statsref;
	public  List <Transform> pooltargets = new List<Transform>();
	public List <Transform> importanttargets = new List<Transform>();
	//
	public float movespeed = 5f;
	public float angularspeed = 100f;
	public float poolsafedistance = .5f;
	public float worldsafedistance = 1;
	public float playersafedistance = 3;


	// the states of the enemy
	private int enemystate=0;
	private int poolint =0;
	public bool busy = false;

	//enemy orientation
	private Quaternion startrotation;
	//raycasthit
	private RaycastHit hit;
	public Transform enemyfront;

	private float timeperlocation =0;
	public float timelimitperlocation = 5;

	void OnEnable()
	{
		statsref = GetComponent<enemystats>();
		poolsafedistance = Random.Range(5,20);
		print(poolsafedistance);
		newlocation();
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

			if(importanttargets.Count ==0 &&!busy)
			{
				movetopoolarea();
			}
			else
			{
				enemystate = 1;
				busy = true;
			}
		
			break;
		case 1:
			//important points
			movetoimportantobject();

			break;
		case 2:
			raycastobj = true;

			//action on important object
			break;
		case 3:
			//player point
			break;
		case 4:
			enemystate =0;
			//done at point
			break;
		case 10:
			//paused, not moving at all
			//animation only
			break;

		}

		if(raycastobj)
		{
			if(Physics.Raycast(enemyfront.position,enemyfront.forward,out hit,Mathf.Infinity,LayerMask.NameToLayer("incell")))
				{
				Transform transhit = hit.transform;
				print("raycast worked");
				switch (transhit.tag)
				{
				case "element":
					elementchange(transhit);
					break;
				case "corecenter":
					centerchange(transhit);
					break;

				}
				}
		}


	}


	public void movetoimportantobject()
	{
		float distance = Vector3.Distance(transform.position,importanttargets[0].position);

		if(distance>worldsafedistance)
		{
			transform.LookAt(importanttargets[0].position);
			transform.position = Vector3.MoveTowards(transform.position,importanttargets[0].position,movespeed *Time.deltaTime);
		}else
		{
			enemystate =2;
		}

	}


	public void elementchange(Transform elementhit)
	{
		//set time in here to convert it over time, not just automatically
		raycastobj = false;
		elementhit.GetComponent<elementaction>().corruptelement();
		busy = false;
		importanttargets.Remove(importanttargets[0]);
		enemystate = 4;
	}

	public void centerchange(Transform centerhit)
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
			transform.rotation =startrotation;
		}
	}



}
