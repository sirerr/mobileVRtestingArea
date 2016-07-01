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
	public Vector2 touchpadxy;


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
	public GameObject collected;

	void Awake()
	{
		startcontrollerOR = transform.rotation;
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
		
			gazer.OnGazeStart(GetComponent<Camera>(),hit.gameObject,rhit.point,true);
		}
	 else 
	{
		raybox.SetActive (false);
		point = null;
	}

	}



	void FixedUpdate()
	{
		controllerOR = GvrController.Orientation;
		transform.rotation = controllerOR;
		
	}
}
