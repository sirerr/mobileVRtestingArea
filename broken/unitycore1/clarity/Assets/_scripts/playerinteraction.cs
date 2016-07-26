using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class playerinteraction : MonoBehaviour {

	public	IGvrGazePointer gazer;

	//start orientation
	private Quaternion startcontrollerOR;
	//player orientation
	private Quaternion controllerOR;
	//the touchpad
	private Vector2 touchpadxy;
	//acellerometer
	private Vector3 controlleraccel;
	//app button pressed
	public bool appbuttonpress = false;
	// touchpad touch
	public bool touchingpad = false;

	//recticle
	public Transform rec;
	// rec material
	public Material currentrecmat;
	//raycast rec material
	public Material newrecmat;
	//raycast start material
	public Material startmat;

	//headmovement
	public Transform headobj;

	//mainraycast hit
	public RaycastHit rhit;
	//raycast distance
	public float raydistance = 100f;
	//distance from hit
	public float hitdistance=0;
	//hit value
	Vector3? point;
	//the pointer obj
	public GameObject raybox;
	//object collected
	public static GameObject collected;
	// player state system
	public static int playerstate =0;
	//hit transform
	private Transform hit;
	//grabbed onto something
	private bool inhand = false;

	//movement variables
	private	float direction =0;
	private float speed = 1f;

	void Awake()
	{
		startcontrollerOR = transform.rotation;
		currentrecmat = rec.GetComponent<MeshRenderer>().material;
		startmat = currentrecmat;
		startmat.color = currentrecmat.color;
	}

	void OnEnable()
	{
		gazer = GazeInputModule.gazePointer;
	}


	
	// Update is called once per frame
	void Update () {

		appbuttonpress = GvrController.AppButtonDown;
		touchingpad = GvrController.IsTouching;

		direction = touchpadxy.y * 10;
	
		//touchpad
		touchpadxy = GvrController.TouchPos;
		//movement
		controlleraccel = GvrController.Accel;

		//raycast 
		Debug.DrawRay(transform.position,transform.forward * raydistance,Color.red,.5f);

		if(Physics.Raycast(transform.position,transform.forward,out rhit,Mathf.Infinity,1 << LayerMask.NameToLayer("incell")))
		{
				hit = rhit.transform;
				raybox.SetActive(true);
				hitdistance = rhit.distance;
				point = rhit.point;
				raybox.transform.position = (transform.position + point.Value) / 2f;
				raybox.transform.localScale = new Vector3 (raybox.transform.localScale.x, raybox.transform.localScale.y, Vector3.Distance(transform.position,point.Value));

			//testing with gaze input
			gazer.OnGazeStart(GetComponent<Camera>(),hit.gameObject,rhit.point,true);


		}
	 else 
		{
			raybox.SetActive (false);
			point = null;
		}
		//raycast

 
 
 
			switch (playerstate)
			{
			case 0:
			if(appbuttonpress)
			{
				grabbedobject(hit);
			}
				break;
			case 1:
						if(direction<6 &&touchingpad)
						{
							float step = speed *Time.deltaTime;
							collected.transform.position = Vector3.MoveTowards(collected.transform.position,transform.position,step *-1);
						}else if ((direction<6 &&touchingpad) && hitdistance>.5f)
						{
							float step = speed *Time.deltaTime;
							collected.transform.position = Vector3.MoveTowards(collected.transform.position,transform.position,step);
						}
			if(appbuttonpress)
			{
				playerstate =2;
			}
				break;
			case 2:
				letgoobject(hit);
				break;

			}
 
	}

	void FixedUpdate()
	{
		controllerOR = GvrController.Orientation;
		transform.rotation = controllerOR;
		
	}


	public void grabbedobject(Transform obj)
	{
		switch(hit.tag)
		{
		case "element":
			collected = hit.gameObject;
			print("hitting element");
			break;
		case "center":
			collected = hit.gameObject;
			print("hitting center");
			break;

		}
		if(collected!=null)
		{
			collected.transform.parent = transform.parent;
			inhand=true;
			collected.transform.position = rhit.point;
			playerstate =1;
		}

	}

	public void letgoobject(Transform obj0)
	{
		collected =null;
		playerstate =0;
	}
}
