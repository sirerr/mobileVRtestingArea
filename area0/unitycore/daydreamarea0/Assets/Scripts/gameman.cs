using UnityEngine;
using System.Collections;
using Gvr.Internal;
using System.Collections.Generic;

public class gameman : MonoBehaviour {

	public static gameman gamemanref;

	public GameObject maincontroller;
	public GameObject mainplayer;

	public List<GameObject> liveballons = new List<GameObject>();
	public int liveballoncount =0;
	public int balloncounter=0;

	void Awake()
	{
		liveballoncount = liveballons.Count;

		gamemanref =this;
		maincontroller = GameObject.FindGameObjectWithTag("controller");
		mainplayer = GameObject.FindGameObjectWithTag("Player");
	}
	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate()
	{
		
	}

	// Update is called once per frame
	void Update () {
	
		liveballoncount = liveballons.Count;

	}
}
