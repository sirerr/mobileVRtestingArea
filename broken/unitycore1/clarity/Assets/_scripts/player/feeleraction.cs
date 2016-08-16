using UnityEngine;
using System.Collections;

public class feeleraction : MonoBehaviour {

	public float feelerspeed = 0;
	public float speedadder = 0;



	void Update()
	{
		feelerspeed = playerstats.playerposenergy;

		if(feelerspeed>1)
		{
			transform.Rotate(Vector3.up * feelerspeed * speedadder * Time.deltaTime);

		}else
		{
			transform.Rotate(Vector3.up * feelerspeed);
		}
	}

}
