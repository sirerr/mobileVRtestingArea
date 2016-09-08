using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cellmakeraction : MonoBehaviour {

	public bool allfinishedmaking = false;

	private float gathertimer =0;
	public float gathertimerlimit =10;
	private float gathercooldown =0;
	public float gathercooldownlimit =10;

	public float gatherspeed = 3;
	public void gatherchildren()
	{
		print("gathering objects now");
			for(int i =0;i<transform.childCount;i++)
			{
			switch(transform.GetChild(i).tag)
			{
			case "element":
				transform.GetChild(i).GetComponent<elementaction>().movetoparent = true;
				transform.GetChild(i).GetComponent<elementaction>().overridemovementspeed = gatherspeed;
				break;
			case "corecenter":
				transform.GetChild(i).GetComponent<centralaction>().movetoparent = true;
				transform.GetChild(i).GetComponent<centralaction>().overridemovementspeed = gatherspeed;
				break;

			case "bulb":
				transform.GetChild(i).GetComponent<bulbaction>().movetoparent = true;
				transform.GetChild(i).GetComponent<bulbaction>().overridemovementspeed = gatherspeed;
				break;
			}
			}
 
		StartCoroutine(gatherchildrenup());

	}

	IEnumerator gatherchildrenup()
	{
		yield return new WaitForSeconds(gathertimerlimit);
 		
		for(int i =0;i<transform.childCount;i++)
		{
			switch(transform.GetChild(i).tag)
			{
			case "element":
				transform.GetChild(i).GetComponent<elementaction>().movetoparent = false;
			//	transform.GetChild(i).GetComponent<elementaction>().overridemovementspeed = gatherspeed;
				break;
			case "corecenter":
				transform.GetChild(i).GetComponent<centralaction>().movetoparent = false;
			//	transform.GetChild(i).GetComponent<centralaction>().overridemovementspeed = gatherspeed;
				break;

			case "bulb":
				transform.GetChild(i).GetComponent<bulbaction>().movetoparent = false;
			//	transform.GetChild(i).GetComponent<bulbaction>().overridemovementspeed = gatherspeed;
				break;
			}
		}

	}

 

}
