using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class controllermovement : MonoBehaviour {

	public Vector3 controllaccel;
	public float speed =0;
	public GameObject emitpointobj;
	public GameObject ballonobj;

	private GameObject currentballon;
	public float ballonsize =0;
	public Vector3 startsize;
	public bool appbuttonpress = false;

	public bool okaytomakeballon = true;

	void Awake()
	{
		GvrViewer.Create();
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

		appbuttonpress = GvrController.AppButton;
		// take button press and make ballon

		if(appbuttonpress)
		{
			if(okaytomakeballon)
			{
				currentballon = Instantiate(ballonobj,emitpointobj.transform.position,emitpointobj.transform.rotation) as GameObject;
				okaytomakeballon = false;
				currentballon.transform.SetParent(emitpointobj.transform);
			}
			startsize = currentballon.transform.localScale;
			print(startsize.x + ballonsize);
			if(startsize.x > startsize.x +ballonsize)
			{ 
				print(startsize.x + ballonsize);
					startsize += new Vector3(startsize.x + .1f,startsize.y +.1f,startsize.z +.1f);
				currentballon.transform.localScale = startsize;
			}

		}



		//print("pressing app button");
	}
}
