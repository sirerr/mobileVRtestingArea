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
			statsref.ehealth--;
			airef.busy = true;
		}

		if(statsref.ehealth <=0)
		{
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

	protected void Update()
	{

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
				areaenemycontrol.importantobjs.Add(eleref.gameObject);
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
