using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class fanbehavoir : MonoBehaviour {

	// is user touching touchpad
	public bool touchingpad = false;
	//distance of raycast
	public float raydis = 0;
	// raycasted direction
	public Vector3 ballondirection;
	public string objtag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit rhit;

		touchingpad = GvrController.IsTouching;

		if(touchingpad)
		{
			if(Physics.Raycast(transform.position,transform.forward,out rhit,raydis))
			{
				Debug.DrawRay(transform.position,transform.forward,Color.red,1);

				if(rhit.transform.CompareTag(objtag))
				{
					rhit.transform.GetComponent<ballonmovement>().newdirection = rhit.transform.position - transform.position;
					print("hitting ballon!!");
				}
					
			}
		}


	
	}
}
