using UnityEngine;
using System.Collections;
using Gvr.Internal;

public class celloptionbuttonaction : MonoBehaviour {

	public cellaction cellactionref;
	public int cellOptionstate = 0;

	public GameObject rotateOptionObj;
	public GameObject collectorOptionObj;

	public GameObject leaveRotateObj;
	public float waittime=2;
	//swipe code
	private float deltaX =0;
	private float deltaY =0;

	private bool touchdown = false;
	private bool touchup = false;

	private Vector2 pos1;
	private Vector2 pos2;
	private float mag =.05f;

	private Transform parentcell;

	private float rotatetimer =0;
	public float rotatetimerlimit =3;

	void Start () {
	
		cellactionref = transform.GetComponentInParent<cellaction>();
		transform.LookAt(cellactionref.spawnloc);
		parentcell = transform.parent;
	}

	void OnEnable()
	{
		cellOptionstate =1;
	}

	void OnDisable()
	{
		rotatetimer = rotatetimerlimit +1;
		cellactionref.swipeRotationOn = false;
		cellOptionstate=0;
		rotateOptionObj.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
	
		touchdown = GvrController.TouchDown;
		touchup = GvrController.TouchUp;

		if(cellOptionstate==2)
		{
		//	print(rotatetimer + " at start option 2");
			if(touchdown)
			{
				transform.parent =null;
				cellactionref.swipeRotationOn = true;
				pos1 = GvrController.TouchPos;
				rotatetimer =0;
			}
			if(touchup)
			{
				
				pos2 =GvrController.TouchPos;

				 deltaX = pos2.x - pos1.x;
				 deltaY = pos2.y - pos1.y;

				if(Mathf.Abs(deltaX)>Mathf.Abs(deltaY))
				{
				//	print("deltaX "+ deltaX);
					if(deltaX>0 && deltaX>mag)
					{
						cellactionref.swipedirection =1;
						cellactionref.swipevalue = deltaX;
					//	cellactionref.RotatebySwipe(1,deltaX);
					//	print("swipping right");
					}else if(deltaX<0 && Mathf.Abs(deltaX)>mag)
					{
						cellactionref.swipedirection =1;
						cellactionref.swipevalue = deltaX;
					//	cellactionref.RotatebySwipe(1,deltaX);
					//	print("swipping left");	
					}
	 
				}
				else
				{
				//	print("deltaY "+ deltaY);
					if(deltaY>0 &&deltaY>mag)
					{
						cellactionref.swipedirection =2;
						cellactionref.swipevalue = deltaY;
					//	cellactionref.RotatebySwipe(2,deltaX);
					//	print("swipping down");

					}	
					else if( deltaY<0 && Mathf.Abs(deltaY)>mag)
					{
						cellactionref.swipedirection =2;
						cellactionref.swipevalue = deltaY;
					//	cellactionref.RotatebySwipe(2,deltaX);
					//	print("swipping up");	
					}
				}		
			}
			rotatetimer+=Time.deltaTime;
		}

		if(rotatetimer>rotatetimerlimit)
		{
			cellactionref.swipeRotationOn = false;
		}
	}
		

	public void disableobj()
	{

		cellactionref.cellOptionOn = false;
		print(rotatetimer);
	}

	public void exitRotate()
	{
		Quaternion tempquanternion = transform.rotation;
		transform.parent = parentcell;
		transform.rotation = tempquanternion;
		cellactionref.swipeRotationOn = false;
		rotateOptionObj.SetActive(false);
	//	print("can no longer rotate");
		cellOptionstate =1;
		cellactionref.swipeRotationOn = false;
		rotatetimer = rotatetimerlimit +1;
	}

	public void enterRotate()
	{
		cellOptionstate =2;
		cellactionref.swipeRotationOn = false;
		rotatetimer = rotatetimerlimit +1;
		rotateOptionObj.SetActive(true);
	
	}

	public void startGather()
	{
		cellOptionstate =3;
		StartCoroutine(gatherWaitTime());
		cellactionref.makeractionref.gatherchildren();
		print("gathering all the parts together");
	}

	IEnumerator gatherWaitTime()
	{
		
		yield return new WaitForSeconds(cellactionref.makeractionref.gathertimerlimit);
		cellOptionstate=1;
	}

}
