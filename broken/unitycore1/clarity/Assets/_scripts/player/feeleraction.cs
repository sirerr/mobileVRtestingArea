using UnityEngine;
using System.Collections;

public class feeleraction : MonoBehaviour {

	private float feelerspeed = 0;
	public float speedadder = 0;

	void Awake()
	{
		
	}

	void Update()
	{
		feelerspeed = playerstats.playerposenergylimit * Time.deltaTime;

		if(feelerspeed>0)
		{
			transform.Rotate(Vector3.up * feelerspeed * speedadder);

		}
	}

}
