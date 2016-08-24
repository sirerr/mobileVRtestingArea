using UnityEngine;
using System.Collections;

public class enemyaction : MonoBehaviour {

	public GameObject dropobject;
	private float childtimeactive = 5;
	private float childwaittime = 15;
	private float childactivetimer =0;
	private enemystats statsref;
	private enemyAI airef;
	private GameObject childcol;

	public virtual void Awake()
	{
		
		statsref = GetComponent<enemystats>();
		airef = GetComponent<enemyAI>();
		childcol = transform.GetChild(0).gameObject;
	}

	public virtual void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("pattack"))
		{
			statsref.ehealth--;
			airef.busy = true;
		}

		if(statsref.ehealth <=0)
		{
			Instantiate(dropobject,transform.position,transform.rotation);
			statsref.currentarea.GetComponent<areaenemycontrol>().enemylist.Remove(transform.gameObject);
			Destroy(gameObject);
		}
	}

	protected void Update()
	{
		childactivetimer += Time.deltaTime;

		if(childactivetimer>childtimeactive)
		{
			childcol.SetActive(false);
			if(childactivetimer>childwaittime)
			{
				childcol.SetActive(true);
				childactivetimer = 0;
			}
		}

	}


	public virtual void OnTriggerEnter(Collider col)
	{
		switch(col.tag)
		{
		case "element":
		//	print("saw element");
			if(col.GetComponent<elementaction>().purestate && !col.GetComponent<elementaction>().captured)
			{
				airef.importanttargets.Add(col.transform);
			}

			break;
		case "corecenter":
		//	print("saw corecenter");
			if(col.GetComponent<centralaction>().fullpower)
			{
				airef.importanttargets.Add(col.transform);
			}
			break;
		}
	}


}
