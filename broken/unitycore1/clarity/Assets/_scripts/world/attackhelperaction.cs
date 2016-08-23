using UnityEngine;
using System.Collections;

public class attackhelperaction : MonoBehaviour {

	public int chargerequirement = 10;
	private int chargecount =0;

	public int usagelimit = 3;
	private int usage = 0;

	private bool  activestate = true;
	private bool readystate = true;
	public void OnCollisionEnter(Collision col)
	{
		if(col.transform.CompareTag("pattack")&& readystate)
		{
			chargecount++;
			print(chargecount);
		}

		if(chargecount == chargerequirement && activestate && readystate)
		{
			readystate = false;
			print(readystate);
			areaattack();
		}
	}

	public void areaattack()
	{
		//expand out
		//flash of some type, show it has power

		StartCoroutine(gobacktoready());
	}

	IEnumerator gobacktoready()
	{
		yield return new WaitForSeconds (10f);
		print("reset to accept again");
		chargecount =0;
		usage++;
		readystate = true;
	}

	void Update()
	{
		if(usage == usagelimit)
		{
			activestate = false;
			//deactivate the device
		}
	}
}
