using UnityEngine;
using System.Collections;

public class enemyaction : MonoBehaviour {

	public GameObject dropobject;

	private enemystats statsref;

	public virtual void Awake()
	{
		statsref = GetComponent<enemystats>();
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
}
