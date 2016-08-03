using UnityEngine;
using System.Collections;

public class playerstats : MonoBehaviour {

	public playerinteraction playerintref;

	public static int playerposenergy = 0;
	public static int playerhealth =10;
	void Awake()
	{

		playerintref = GameObject.FindGameObjectWithTag("controller").GetComponent<playerinteraction>();

	}
		
}
