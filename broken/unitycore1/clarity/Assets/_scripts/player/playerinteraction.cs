using UnityEngine;
using System.Collections;
using Gvr.Internal;
using System.Collections.Generic;
using UnityEngine.UI;

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
	//app button press
	public bool appButtonPressDown = false;
	// touchpad touch
	public bool touchpaddown = false;
	//click button
	public bool clickpress = false;
	//click button up
	public bool clickpressup = false;
	//app button being pressed
	public bool appbuttoncontinuedpress = false;
	//click press down
	public bool clickPressDown =false;
	//touchpad holding it
	public bool touchpadcontinuedpress = false;
	//determines if the controller is connected or not
	//private enum controllerStatus{controllerOn,controllerOff};

	//player canvas
	public Canvas playercanvas;

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
	//crystal object
	public Transform maincontrol;

	//mainraycast hit
	public RaycastHit rhit;
	//raycast distance
	public float raydistance = 100f;
	private float raygatherdistance =3f;
	private float raydistancetemp =0;

	//distance from hit
	public float hitdistance=0;
	//hit value
	Vector3? point;
	//the pointer obj
	public GameObject raybox;
	//object collected
	public static GameObject lookedatobj;
	// player state system
	public static int playerstate =0;
	//hit transform
	private Transform hit;
	//list of elements collected
	public List<GameObject> elementcolection = new List<GameObject>();
	//game objects collected count
	public int elementintcount = 0;
	//collect point
	public Transform collectpoint;
	//central collect point
	public Transform centercollectpoint;
	//front of diamond
	public Transform diamondfrontpoint;

	//the center object collected by the player
	private GameObject centercollected;

	//the positive energy blast object
	public GameObject posblastobj;

	private int pmask = (1<< 8)| (1<<11) |(1<<9 | (1<<12));

 	private bool firstjump = false;
	//determine if the button has been held down long enough to enable gatherer
	private float holdappbuttontime =0;
	public float holdappbuttontimelimit = 3;
	//holding touchpad timer
	private float touchingTouchPadtime=0;
	public float touchingTouchPadTimeLimit = 2;

	//will change when loading is down but always on for now
	public bool areaready = true;
	//testing the new input system, will turn it off or on
	public bool newimputsystem =false;

	void Awake()
	{
	//	maincontrol.rotation = transform.rotation;
		startcontrollerOR = transform.rotation;
		currentrecmat = rec.GetComponent<MeshRenderer>().material;
		startmat = currentrecmat;
		startmat.color = currentrecmat.color;
		raydistancetemp = raydistance;
	}

	void OnEnable()
	{
		gazer = GazeInputModule.gazePointer;
	}



	// Update is called once per frame
	void Update () {

		//find out the connection status of the controller

		//test code

		//all controller input
		controllerOR = GvrController.Orientation;
		appButtonPressDown = GvrController.AppButtonDown;
		touchpaddown = GvrController.TouchDown;
		clickpress = GvrController.ClickButton;
		clickPressDown = GvrController.ClickButtonDown;
		clickpressup = GvrController.ClickButtonUp;
		appbuttoncontinuedpress = GvrController.AppButton;
		touchpadcontinuedpress = GvrController.IsTouching;
		//old code isn't used
 
	
		//touchpad
		touchpadxy = GvrController.TouchPos;
		//movement
		controlleraccel = GvrController.Accel;
		//controller orientation
		maincontrol.localRotation = controllerOR;


		//raycast 
	//	Debug.DrawRay(diamondfrontpoint.position,diamondfrontpoint.forward * Mathf.Infinity,Color.red,.5f);

		if(Physics.Raycast(diamondfrontpoint.position,diamondfrontpoint.forward,out rhit,raydistance,pmask) && areaready)
		{
				hit = rhit.transform;
				raybox.SetActive(true);
				hitdistance = rhit.distance;
				lookedatobj = hit.gameObject;
				point = rhit.point;
			raybox.transform.position = (raybox.transform.parent.transform.position + point.Value) / 2f;
			raybox.transform.localScale = new Vector3 (raybox.transform.localScale.x, raybox.transform.localScale.y, Vector3.Distance(diamondfrontpoint.position,point.Value));
	
			//testing with gaze input
			gazer.OnGazeStart(GetComponent<Camera>(),hit.gameObject,rhit.point,true);


		}
	 else 
		{
			raybox.SetActive (false);
			point = null;
			hit = null;
			lookedatobj = null;

		}
		//raycast

		if(newimputsystem)
		{
			if(clickPressDown && hit!=null)
			{
				switch (hit.tag)
				{
				case "element":
					currentrecmat = newrecmat;
					elementcollector(hit);
					break;

				case "corecenter":
					currentrecmat = newrecmat;
					elementcenter(hit);
					break;
				case "cellholeloc":
					cellholecenterlocaction(hit);
					break;
				case "bulb":
					currentrecmat = newrecmat;
					bulblookat(hit);
					break;
				case "jumppoint":
					newlocationjump(hit);
					break;
				case "outsphere":
					gotocell(hit);
					break;
				case "returner":
					leavecell(hit);
					break;
				case "enemy":
					positiveshot (hit);
					break;
				case "helperA":
					positiveshot(hit);
					break;
				case "gatherer":
					startGatheringObjs(hit);
					break;
				case "rotator":
					startRotateCell(hit);
					break;
				case "UIexit":
					stopRotateCell(hit);
					break;
				}

			}

			if(appButtonPressDown && hit!=null)
			{
				switch(hit.tag)
				{
				case "cell":
					celloptionaction(hit);
					break;
				}
			}

			if(touchpadcontinuedpress && hit!=null && !clickPressDown)
			{
				touchingTouchPadtime +=Time.deltaTime;
				if(touchingTouchPadtime>touchingTouchPadTimeLimit)
				{
					switch(hit.tag)
					{
					case "element":
						elementcleansing(hit);
						break;
					case "corecenter":
						touchpadactioncenterobj();
						break;
					}
				}
			}
		}
		else
		{
			if(appButtonPressDown && hit!=null)
			{
				switch (hit.tag)
				{
				case "element":
					elementcollector(hit);
					break;

				case "corecenter":
					elementcenter(hit);
					break;
				case "cellholeloc":
					cellholecenterlocaction(hit);
					break;
				case "bulb":
					bulblookat(hit);
					break;
				case "jumppoint":
					newlocationjump(hit);
					break;
				case "outsphere":
					gotocell(hit);
					break;
				case "returner":
					leavecell(hit);
					break;
				case "enemy":
					positiveshot (hit);
					break;
				case "helperA":
					positiveshot(hit);
					break;
				case "gatherer":
					
					break;
				case "rotator":
					
					break;
				case "UIexit":

					break;
					
				}
			}

			if(clickpress && hit!=null)
			{
				switch(hit.tag)
				{
				case "cell":
					cellrotation(rhit.point,hit);
					break;
				}
			}

			if (clickpressup && hit!=null)
			{
				switch(hit.tag)
				{
				case "cell":
					resetcellrotation(hit);
					break;
				}
			}

			if(touchpaddown && hit!=null)
			{

				switch(hit.tag)
				{
				case "element":
					elementcleansing(hit);
					break;
				case "corecenter":
					touchpadactioncenterobj();
					break;
				}

			}


		}
			
	}

	public void startRotateCell(Transform rotateEnter)
	{
		rotateEnter.GetComponentInParent<celloptionbuttonaction>().enterRotate();
	}

	public void stopRotateCell(Transform rotateExit)
	{
		rotateExit.GetComponentInParent<celloptionbuttonaction>().exitRotate();
	}

	public void startGatheringObjs(Transform gatherStart)
	{
		gatherStart.GetComponentInParent<celloptionbuttonaction>().startGather();
	}

	public void celloptionaction(Transform hit)
	{
		cellaction cellref = hit.GetComponent<cellaction>();

		if(!cellref.cellOptionOn)
		{
			cellref.cellOptionOn = true;
		}
		else
		{
			cellref.cellOptionOn = false;
		}
	}

	public void gatherobjects(Transform cellhit)
	{
		cellhit.GetComponent<cellaction>().talktomaker();
		holdappbuttontime = 0;
	}

	public void leavecell(Transform returner)
	{
		playerstate =0;
		returner.GetComponentInParent<celloptionbuttonaction>().cellactionref.leavecell();
		returner.GetComponentInParent<celloptionbuttonaction>().disableobj();
	}

	public void gotocell(Transform cellobj)
	{
		
		if(!firstjump)
		{
			gmanager.lastjumplocation = gmanager.playerobj.transform.position;
			gmanager.lastjumprotation = gmanager.playerobj.transform.rotation;
		}

		playerstate = 1;
		gmanager.playerobj.transform.position = cellobj.parent.transform.GetComponent<cellaction>().arriveincelllocation();
		gmanager.playerobj.transform.rotation = cellobj.parent.transform.GetComponent<cellaction>().arriveincellrotation();
	}



	public void resetcellrotation(Transform hitobj)
	{
		hitobj.GetComponent<cellaction>().dorotate = false;
		hitobj.GetComponent<cellaction>().gotvalues = false;		
	}

	public void cellrotation(Vector3 hitpoint,Transform celltrans)
	{
		celltrans.GetComponent<cellaction>().getrotateready(hitpoint);
		celltrans.GetComponent<cellaction>().raypoint = hitpoint;
		celltrans.GetComponent<cellaction>().dorotate = true;
	}



	public void elementcleansing(Transform elementobj)
	{

		if(playerstats.playerposenergy>0)
		{
			hit.GetComponent<elementaction>().cleanelement();
			playerstats.playerposenergy--;
		}
		touchingTouchPadtime =0;
	}

	public void newlocationjump(Transform jumpobj)
	{
		if(!firstjump){
		firstjump = true;
		}

		//need to add the blink in there later but for now just move around the scene
		gmanager.playerobj.transform.rotation = jumpobj.GetComponent<jumppointaction>().rotateplayer();
		gmanager.playerobj.transform.position = jumpobj.GetComponent<jumppointaction>().playerhere();

	}

	public void positiveshot(Transform currentpoint)
	{
		if(playerstats.playerposenergy >0)
		{
			GameObject posobj = Instantiate(posblastobj,diamondfrontpoint.position,diamondfrontpoint.rotation) as GameObject;
			playerstats.playerposenergy--;
		}
	}

	public void bulblookat(Transform obj)
	{

		obj.GetComponent<bulbaction>().absorbed();
	}

	public void cellholecenterlocaction(Transform obj)
	{
		if(centercollected!=null)
		{
			centercollected.GetComponent<centralaction>().tohole(obj);
			//centercollected.transform.parent = obj;
			centercollected = null;
		}
	}

	public void touchpadactioncenterobj()
	{
		hit.GetComponent<centralaction>().centralstateaction();
		touchingTouchPadtime =0;
	}


	public void elementcollector(Transform obj)
	{
	//	print(playerstats.playerposenergy);
	//	print(playerstats.playerposenergylimit);
//		if(playerstats.playerposenergy<playerstats.playerposenergylimit)
//		{
//
//		}

		if(obj.GetComponent<elementaction>().purestate)
		{
			obj.GetComponent<elementaction>().collected(collectpoint);
			elementcolection.Add(obj.gameObject);
			elementintcount++;
		}
	}
		

	public void elementcenter(Transform hit)
	{
		centralaction centeractref = hit.GetComponent<centralaction>();

		if(centeractref.centralstate ==2)
		{
			centercollected = hit.gameObject;
			hit.GetComponent<centralaction>().grabbed(centercollectpoint);

		}else if(centeractref.centralstate ==1 && !centeractref.fullpower) {

			if(elementintcount>0)
			{
				elementcolection[elementintcount-1].GetComponent<elementaction>().letloose(rhit.point);
				elementcolection.Remove(elementcolection[elementintcount-1]);
				elementintcount--;

			}
		}



	}
}
