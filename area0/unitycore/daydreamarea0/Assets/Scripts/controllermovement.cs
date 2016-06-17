using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class controllermovement : MonoBehaviour {

	private Vector3 controllaccel;
	public float speed =0;
	public GameObject emitpointobj;
	public GameObject ballonobj;
	public GameObject fanpivotobj;
	private GameObject currentballon;


	private Vector3 startsize;
	public float sizeincspeed =0;

	private float sizecounter =0;
	public float finalsizecounter =0;
	public float sizecounterinc =0;

	private Vector3 currentballonsize;
	public bool appbuttonpress = false;

	public bool okaytomakeballon = true;
	public bool letlooseballon = false;
	public bool letgo = false;

	public float ballonforcey =0;

	//controller touchpad
	public Vector2 touchpadloc;
	public bool firsttouch = false;

	//fan obj
	public GameObject fanobj;
	public Vector3 fandefaultrotation;
	public Vector3 fancurrentrotation;

	void Awake()
	{
		GvrViewer.Create();
		fandefaultrotation = fanobj.transform.rotation.eulerAngles;
	
	}
	// Use this for initialization
	void Start () {
		speed = speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		transform.rotation = GvrController.Orientation;
		controllaccel = GvrController.Accel;

	//	transform.position += controllaccel * speed;
	}

	void Update()
	{
		fancurrentrotation = fanobj.transform.rotation.eulerAngles;
		//take button and make ballon
		appbuttonpress = GvrController.AppButton;
		// touch pad location;
		touchpadloc = GvrController.TouchPos;

	
		//touchpad code
		fanobj.transform.position = fanpivotobj.transform.position;
		float xtouchvalue = touchpadloc.x *100f;
		float ytouchvalue = touchpadloc.y *100f;

		if(GvrController.TouchDown)
		{
			firsttouch = true;
		}

		//code needs to be fixed!!!

		if(xtouchvalue<50 && firsttouch)
		{
			fancurrentrotation.x = 310f + xtouchvalue;
			fanobj.transform.rotation = Quaternion.Euler(fancurrentrotation);


		}else if (xtouchvalue>51 && xtouchvalue<=100 && firsttouch)
		{

			fancurrentrotation.x = xtouchvalue -51;
			fanobj.transform.rotation = Quaternion.Euler(fancurrentrotation);
		}

		if(ytouchvalue<50 && firsttouch)
		{
			fancurrentrotation.y = 310f + ytouchvalue;
			fanobj.transform.rotation = Quaternion.Euler(fancurrentrotation);


		}else if (ytouchvalue>51 && ytouchvalue<=100 && firsttouch)
		{

			fancurrentrotation.y = ytouchvalue -51;
			fanobj.transform.rotation = Quaternion.Euler(fancurrentrotation);
		}


		//touchpad code

		if(appbuttonpress)
		{
			if(okaytomakeballon)
			{
				currentballon = Instantiate(ballonobj,emitpointobj.transform.position,emitpointobj.transform.rotation) as GameObject;
				okaytomakeballon = false;
				currentballon.transform.position = emitpointobj.transform.position;
				startsize = currentballon.transform.localScale;
				currentballonsize = startsize;
			}


			if(sizecounter<finalsizecounter)
			{
				sizecounter+=sizecounterinc;
				currentballonsize.x = currentballonsize.x + (sizeincspeed * Time.deltaTime);
				currentballonsize.y = currentballonsize.y + (sizeincspeed * Time.deltaTime);
				currentballonsize.z = currentballonsize.z + ((sizeincspeed )* Time.deltaTime);
				currentballon.transform.localScale = currentballonsize;

			}
			else
			{
				letlooseballon = true;
			}

			if(!letgo && letlooseballon)
			{
				letlooseballon = false;
				Rigidbody rbody = currentballon.GetComponent<Rigidbody>();
				rbody.AddForce(Vector3.up * ballonforcey);
				gameman.gamemanref.liveballons.Add(currentballon);
				currentballon = null;
				//keeps count in the list of the ballon location
				gameman.gamemanref.balloncounter++;
				gameman.gamemanref.liveballoncount++;
				letgo = true;
			}

		}

		if(currentballon !=null)
		{
			currentballon.transform.position = emitpointobj.transform.position;	
		}

		//print("pressing app button");
	}
}
