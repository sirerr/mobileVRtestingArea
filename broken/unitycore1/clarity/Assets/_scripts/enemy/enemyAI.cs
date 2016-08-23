using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyAI : MonoBehaviour {

	private bool raycastobj = false;
	public enemystats statsref;
	public  List <Transform> pooltargets = new List<Transform>();
	private List <Transform> importanttargets = new List<Transform>();

	// the states of the objects
	private int enemystates=0;


	void OnEnable()
	{
		statsref = GetComponent<enemystats>();

	}

}
