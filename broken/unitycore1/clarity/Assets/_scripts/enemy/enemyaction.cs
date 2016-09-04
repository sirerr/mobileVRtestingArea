using UnityEngine;
using System.Collections;

public class enemyaction : MonoBehaviour {

	public GameObject dropobject;
	private float childtimeactive = 3;
	private float childwaittime = 15;
	private float childactivetimer =0;
	private enemystats statsref;
	private enemyAI airef;
	public areaenemycontrol enemycontrolref;
	public  GameObject childcol;

	//timer for being attacked
	private float beingattackedtimer = 0;
	public float beingattackedtimerlimit =30;
	//object for attacking player
	private float attacktheplayertimer =0;
	public float attacktheplayertimerlimit =2;
	public GameObject attack0obj;


	public bool makechildactive = false;

	//safe
	public virtual void Awake()
	{
		
		statsref = GetComponent<enemystats>();
		airef = GetComponent<enemyAI>();
		childcol = transform.GetChild(0).gameObject;
		childcol.SetActive(false);
	}
	//safe
	public virtual void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("pattack"))
		{
			beingattackedtimer =0;
			statsref.ehealth--;
			airef.busy = true;
		}

		if(statsref.ehealth <=0)
		{
			enemydeath();
		}

		if(col.transform.CompareTag("helperA"))
		{print("it happened");
			enemydeath();
		}
	}

	public virtual void enemydeath()
	{
		Instantiate(dropobject,transform.position,transform.rotation);
		enemycontrolref.enemylist.Remove(transform.gameObject);
		enemycontrolref.enemyactionref.Remove(GetComponent<enemyaction>());
		Destroy(gameObject);
	}

	public virtual void attackplayer()
	{
		attacktheplayertimer += Time.deltaTime;

		if(attacktheplayertimer>attacktheplayertimerlimit)
		{
			GameObject attack = Instantiate(attack0obj,airef.enemyfront.position,airef.enemyfront.rotation) as GameObject;
			attacktheplayertimer = 0;
		}

	}

	protected void Update()
	{

		beingattackedtimer += Time.deltaTime;

		if(beingattackedtimer>beingattackedtimerlimit && airef.busy)
		{
			airef.busy = false;
			beingattackedtimer = 0;
		}

		if(makechildactive)
		{
			childdectection();
		}
	}

	public virtual void childdectection()
	{
		childcol.SetActive(true);
		childactivetimer += Time.deltaTime;

		if(childactivetimer>childtimeactive)
		{
		//	print("time active " + childactivetimer);
			childcol.SetActive(false);
			childactivetimer = 0;
			makechildactive = false;
		}
	}


	public virtual void OnTriggerEnter(Collider col)
	{
		switch(col.tag)
		{
		case "element":
			//	print("saw element");
			elementaction eleref = col.GetComponent<elementaction>();
			if(eleref.purestate && !eleref.captured)
			{
				print ("found one!!!");
				enemycontrolref.newfind = true;
				enemycontrolref.importantobjs.Add(eleref.gameObject);
				//	airef.importanttargets.Add(col.transform);
			}

			break;
		case "corecenter":
		//	print("saw corecenter");
			if(col.GetComponent<centralaction>().fullpower)
			{
			

				//	airef.importanttargets.Add(col.transform);
			}
			break;
		}
	}


}
