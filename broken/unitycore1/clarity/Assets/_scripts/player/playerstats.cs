using UnityEngine;
using System.Collections;

public class playerstats : MonoBehaviour {

	public playerinteraction playerintref;

	public static int playerposenergy = 15;
	public static int playerposenergylimit = 50;
	public static int playerhealth =10;
	void Awake()
	{

		playerintref = GameObject.FindGameObjectWithTag("controller").GetComponent<playerinteraction>();

	}
		
}
