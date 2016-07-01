using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class elementaction : MonoBehaviour {

	IGvrGazePointer gazepoint;

	public bool canuse = true;
	public bool captured = false;

	public float strength = 0;

	void OnEnable()
	{


	}

	// Use this for initialization
	void Start () {
	
	}
		

	// Update is called once per frame
	void Update () {
		if(!canuse)
		{
			GetComponent<Collider>().enabled = false;
		}
	
	}
}
