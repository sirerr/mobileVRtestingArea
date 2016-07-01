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

	void Awake()
	{
		startcontrollerOR = transform.rotation;
		currentrecmat = rec.GetComponent<MeshRenderer>().material;
	}

	void OnEnable()
	{
		gazer = GazeInputModule.gazePointer;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//touchpad
		touchpadxy = GvrController.TouchPos;
		//movement
		controlleraccel = GvrController.Accel;

		//raycast 
		Debug.DrawRay(transform.position,transform.forward * raydistance,Color.red,.5f);

		if(Physics.Raycast(transform.position,transform.forward,out rhit,Mathf.Infinity,1 << LayerMask.NameToLayer("incell")))
		{
			Transform hit = rhit.transform;
				raybox.SetActive(true);
				hitdistance = rhit.distance;
				point = rhit.point;
				raybox.transform.position = (transform.position + point.Value) / 2f;
				raybox.transform.localScale = new Vector3 (raybox.transform.localScale.x, raybox.transform.localScale.y, Vector3.Distance(transform.position,point.Value));

			//testing with gaze input
			gazer.OnGazeStart(GetComponent<Camera>(),hit.gameObject,rhit.point,true);

			//when seeing a element
			if(hit.CompareTag("element"))
			{
				rec.GetComponent<MeshRenderer>().material.color = newrecmat.color;

					switch(playerstate)
					{
					case 0:
					if(appbuttonpress)
					{
						hit.parent = transform;
						hit.GetComponent<Rigidbody>().isKinematic = true;

						collected = hit.gameObject;
						hit.GetComponent<elementaction>().captured = true;
						print(playerstate);
						playerstate =1;
					}
						break;
					case 1:
						if(appbuttonpress)
						{
							playerstate =2;
						}
						break;
					case 2:
						hit.parent = null;
						hit.GetComponent<Rigidbody>().isKinematic = false;
						hit.GetComponent<elementaction>().captured = false;
						collected = null;
						print(playerstate);
						playerstate =0;
						break;
					}

				//moving element back and forth
				float direction = touchpadxy.y * 10;
				float speed = 1f;

				if(direction<6 && touchingpad)
				{

					float step = speed *Time.deltaTime;
					rhit.transform.position = Vector3.MoveTowards(rhit.transform.position,transform.position,step *-1);
				}else if(direction>6 && touchingpad)
				{
					float step = speed *Time.deltaTime;
					rhit.transform.position = Vector3.MoveTowards(rhit.transform.position,transform.position,step);
				}
			}
				else
			{
				print("change color!!!!");
				rec.GetComponent<MeshRenderer>().material = currentrecmat;
				if(collected!=null)
				{
					hit.parent = null;
					hit.GetComponent<Rigidbody>().isKinematic = false;
					playerstate =0;
					hit.GetComponent<elementaction>().captured = false;
					collected = null;
				}
			}
		}
	 else 
		{
			raybox.SetActive (false);
			point = null;
		}
		appbuttonpress = GvrController.AppButtonDown;
		touchingpad = GvrController.IsTouching;
	}



	void FixedUpdate()
	{
		controllerOR = GvrController.Orientation;
		transform.rotation = controllerOR;
		
	}
}
