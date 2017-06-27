using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cellmakeraction : MonoBehaviour {

	public bool allfinishedmaking = false;

	private float gathertimer =0;
	public float gathertimerlimit =3;
	private float gathercooldown =0;
	public float gathercooldownlimit =10;

	public float gatherspeed = 2;

	public bool gatheredup = false;
	public void gatherchildren()
	{
		if(!gatheredup)
		{
		print("gathering objects now");
			for(int i =0;i<transform.childCount;i++)
			{
			switch(transform.GetChild(i).tag)
			{
			case "element":
				transform.GetChild(i).GetComponent<elementaction>().stopmovement();
				transform.GetChild(i).GetComponent<elementaction>().movetoparent = true;
				transform.GetChild(i).GetComponent<elementaction>().overridemovementspeed = gatherspeed;
				break;
			case "corecenter":
				transform.GetChild(i).GetComponent<centralaction>().stopmovement();
				transform.GetChild(i).GetComponent<centralaction>().movetoparent = true;
				transform.GetChild(i).GetComponent<centralaction>().overridemovementspeed = gatherspeed;
				break;

			case "bulb":
				transform.GetChild(i).GetComponent<bulbaction>().stopmovement();
				transform.GetChild(i).GetComponent<bulbaction>().movetoparent = true;
				transform.GetChild(i).GetComponent<bulbaction>().overridemovementspeed = gatherspeed;
				break;
			}
			}
 
		StartCoroutine(gatherchildrenup());
	}
	}

	IEnumerator gatherchildrenup()
	{
		yield return new WaitForSeconds(gathertimerlimit);
		gatheredup = true;
		for(int i =0;i<transform.childCount;i++)
		{
			switch(transform.GetChild(i).tag)
			{
			case "element":
				transform.GetChild(i).GetComponent<elementaction>().movetoparent = false;
				transform.GetChild(i).GetComponent<elementaction>().startmovement();
			//	transform.GetChild(i).GetComponent<elementaction>().overridemovementspeed = gatherspeed;
				break;
			case "corecenter":
				transform.GetChild(i).GetComponent<centralaction>().movetoparent = false;
				transform.GetChild(i).GetComponent<centralaction>().startmovement();
			//	transform.GetChild(i).GetComponent<centralaction>().overridemovementspeed = gatherspeed;
				break;

			case "bulb":
				transform.GetChild(i).GetComponent<bulbaction>().movetoparent = false;
				transform.GetChild(i).GetComponent<bulbaction>().startmovemnt();
			//	transform.GetChild(i).GetComponent<bulbaction>().overridemovementspeed = gatherspeed;
				break;
			}
		}

		StartCoroutine(cooldownaftergatherup());
	}

	IEnumerator cooldownaftergatherup()
	{
		yield return new WaitForSeconds(gathercooldownlimit);
		gatheredup = false;
	}

}
