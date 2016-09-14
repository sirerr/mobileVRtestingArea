using UnityEngine;
using System.Collections;

public class attackhelperaction : MonoBehaviour {
	//start position
	private Vector3 startpos;
	//start rotation
	private Quaternion startrot;
	//amount of times you must hit it in order to activate the object
	public int chargerequirement = 4;
	//keep the charge count
	public int chargecount =0;
	//amount of times used by player
	public int usagelimit = 3;
	//keeps count of the amount of times the helper is used
	public int usage = 0;
	//tells the state of the helper
	private int helperstate =0;
	//helpers current target
	private GameObject currenttarget;

	public float speed =3;
	public float safedistance =.5f;

	public areaenemycontrol areaenemyref;
	public areamanager areamanagerref;

	public bool donewithdestroy = false;

	void OnEnable()
	{
		startpos = transform.position;
		startrot = transform.rotation;
	}

	public void OnCollisionEnter(Collision col)
	{
		if(col.transform.CompareTag("pattack")&& helperstate ==0)
		{
			chargecount++;
			print(chargecount);
		}

		if(chargecount == chargerequirement && helperstate ==0)
		{
			decideOnEnemy();
		}
	}

	public void decideOnEnemy()
	{
		currenttarget = areaenemyref.enemylist[0].gameObject;
		helperstate =1;
	}

	public void findandsearch()
	{
		print("finding and searching");
		transform.LookAt(currenttarget.transform);
		transform.position = Vector3.MoveTowards(transform.position,currenttarget.transform.position,speed *Time.deltaTime);
		if(Vector3.Distance(transform.position,currenttarget.transform.position)<safedistance)
		{
			destroyenemy();
			if(helperstate ==2 && !donewithdestroy)
			{
				donewithdestroy = true;
				StartCoroutine(gobacktoready());
			}
		}
	}

	public void destroyenemy()
	{
		helperstate =2;
		// destroy enemy
		areaenemyref.enemylist.RemoveAt(0);
		areaenemyref.enemyactionref.RemoveAt(0);
		Destroy(currenttarget);
		currenttarget =null;

	}

	IEnumerator gobacktoready()
	{
		yield return new WaitForSeconds (2f);
		print("reset to accept again");
		chargecount =0;
		usage++;

		if(usage == usagelimit)
		{
			helperstate =5;
			transform.rotation = startrot;
			transform.position  = startpos;
		}
		else
		{
			transform.rotation = startrot;
			transform.position  = startpos;
			helperstate =0;
		}

	}

	void Update()
	{
		switch (helperstate)
		{
		case 0:
			break;
		case 1:
			findandsearch();
			break;
		case 2:
			break;
		case 5:
			break;
		}
	}
}
