using UnityEngine;
using System.Collections;

public class enemyaction : MonoBehaviour {

	public GameObject dropobject;

	private enemystats statsref;
	private enemyAI airef;

	public virtual void Awake()
	{
		statsref = GetComponent<enemystats>();
		airef = GetComponent<enemyAI>();
	}

	public virtual void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("pattack"))
		{
			statsref.ehealth--;
		}

		if(statsref.ehealth <=0)
		{
			Instantiate(dropobject,transform.position,transform.rotation);
			statsref.currentarea.GetComponent<areaenemycontrol>().enemylist.Remove(transform.gameObject);
			Destroy(gameObject);
		}
	}

	public virtual void OnTriggerEnter(Collider col)
	{
		switch(col.tag)
		{
		case "element":
			print("saw element");
			if(col.GetComponent<elementaction>().purestate && !col.GetComponent<elementaction>().captured)
			{
				airef.importanttargets.Add(col.transform);
			}

			break;
		case "corecenter":
			print("saw corecenter");
			if(col.GetComponent<centralaction>().fullpower)
			{
				airef.importanttargets.Add(col.transform);
			}
			break;
		}
	}


}
