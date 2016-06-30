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

	public Color mainmat;
	public Color reversemat;
	public GameObject childobj;
	public bool reverse  =false;
	public float frontback = 1;

	void Awake()
	{
		childobj = transform.GetChild(0).gameObject;
		mainmat = childobj.GetComponent<MeshRenderer>().material.color;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		reverse = GvrController.ClickButton;

		if(reverse)
		{
			childobj.GetComponent<MeshRenderer>().material.color = reversemat;
			frontback = -1;
		}else
		{
			childobj.GetComponent<MeshRenderer>().material.color = mainmat;
			frontback = 1;
		}

		RaycastHit rhit;

		touchingpad = GvrController.IsTouching;

		if(touchingpad)
		{
			Debug.DrawRay(transform.position,transform.forward * raydis,Color.red,.5f);

			if(Physics.Raycast(transform.position,transform.forward,out rhit,raydis))
			{
				

				if(rhit.transform.CompareTag(objtag))
				{
					rhit.transform.GetComponent<ballonmovement>().newdirection = (rhit.transform.position - transform.position) *frontback;
					if(reverse)
						rhit.transform.GetComponent<ballonmovement>().addforce =.5f;
					else
						rhit.transform.GetComponent<ballonmovement>().addforce =1f;
					rhit.transform.GetComponent<ballonmovement>().direction();

				}
					
			}
		}


	
	}
}
