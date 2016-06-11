using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class gameman : MonoBehaviour {

	public static gameman gamemanref;

	public GameObject maincontroller;
	public GameObject mainplayer;


	void Awake()
	{
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
	
	}
}
