using UnityEngine;
using System.Collections;

public class playerhealthaction : MonoBehaviour {


	public 	void OnTriggerEnter(Collider col)
	{
		
		if(col.CompareTag("eattack"))
		{
			// for now decrease health
		//	playerstats.playerhealth --;
			print("being hit");
		}

		if(playerstats.playerhealth<=0)
		{
			//player dead, respawns to start location outside
			//clear all elements collected
			//reset feeler energy start 
			//reset health
		}
	}

}
