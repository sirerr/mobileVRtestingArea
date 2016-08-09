using UnityEngine;
using System.Collections;

public class feeleraction : MonoBehaviour {

	public float playerspeed = 0;

	void Awake()
	{
		
	}

	void Update()
	{
		playerspeed = playerstats.playerposenergylimit *4;

		if(playerspeed>0)
		{
			transform.Rotate(Vector3.up * playerspeed * Time.deltaTime);

		}
	}

}
