using UnityEngine;
using System.Collections;

public class playerstats : MonoBehaviour {

	public playerinteraction playerintref;
	//public Transform mainplayer;
	public static int playerposenergy = 10;
	public static int playerposenergylimit = 50;
	public static int playerhealth =10;


	void Awake()
	{

		playerintref = GameObject.FindGameObjectWithTag("controller").GetComponent<playerinteraction>();
	//	mainplayer = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Start()
	{

	}
		
}
