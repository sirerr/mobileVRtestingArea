using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class playerinteraction : MonoBehaviour {

	IGvrGazePointer gazepointer;

	//start orientation
	public Quaternion startcontrollerOR;
	//player orientation
	public Quaternion controllerOR;

	//mainraycast hit
	public RaycastHit rhit;
	//raycast distance
	public float raydistance = 100f;
	//distance from hit
	public float hitdistance=0;
	Vector3? point;

	public GameObject raybox;
	public GameObject collected;

	void Awake()
	{
		startcontrollerOR = transform.rotation;

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//raycast 
		Debug.DrawRay(transform.position,transform.forward * raydistance,Color.red,.5f);

		if(Physics.Raycast(transform.position,transform.forward,out rhit,raydistance))
		{

			if (rhit.transform.tag == "innerworld") {
				//print ("hitting innerworld");
				raybox.SetActive(true);
				hitdistance = rhit.distance;
				point = rhit.point;
				raybox.transform.position = (transform.position + point.Value) / 2f;
				raybox.transform.localScale = new Vector3 (raybox.transform.localScale.x, raybox.transform.localScale.y, hitdistance);
			} else 
			{
				raybox.SetActive (false);
			}


		}

	}

	void FixedUpdate()
	{
		controllerOR = GvrController.Orientation;
		transform.rotation = controllerOR;
		
	}
}
