using UnityEngine;
using System.Collections;

public class attackhelperaction : MonoBehaviour {
	//start position
	public Transform startpos;
	//amount of times you have to hit it with energy before activating
	public int chargerequirement = 4;
	//keep the charge count
	public int chargecount =0;
	//amount of times used by player
	public int usagelimit = 3;
	//keeps count of the amount of times the helper is used
	private int usage = 0;
	//tells the state of the helper
	public int helperstate =0;
	//helpers current target
	public GameObject currenttarget;

	public float speed =3;
	public float safedistance =.5f;

	public areaenemycontrol areaenemyref;
	public areamanager areamanagerref;

	public bool donewithdestroy = false;

	void OnEnable()
	{
		startpos.position = transform.position;
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

		transform.LookAt(currenttarget.transform);
		Vector3.MoveTowards(transform.position,currenttarget.transform.position,speed *Time.deltaTime);
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
		Destroy(currenttarget);
		currenttarget =null;

	}

	IEnumerator gobacktoready()
	{
		yield return new WaitForSeconds (10f);
		print("reset to accept again");
		chargecount =0;
		usage++;

		if(usage == usagelimit)
		{
			helperstate =5;
		}
		else
		{
			transform.position  = startpos.position;
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
