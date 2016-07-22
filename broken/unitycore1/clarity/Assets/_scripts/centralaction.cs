using UnityEngine;
using System.Collections;

public class centralaction : MonoBehaviour {

	//for element parts
	public int attachpointsactive =0;
	public int activecounter=0;
	public float collectedpower = 0;

	public GameObject[] attachpointobjs;
	private int childrencount;

	//for central mode

	public bool isgrabbed = false;
	public bool setinplace = false;

	public bool fullpower = false;

	public int elementaddedcounter = 0;

	public Rigidbody rbody;
	public Collider col;

	void Awake()
	{
		createelementhitlocs();
	
		rbody = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
	
	}


	public void createelementhitlocs()
	{
		attachpointsactive = Random.Range(2,6);
		childrencount = transform.childCount;

		// creates element hit locations
		for(int i=0;i<childrencount;i++)
		{

			int a = Random.Range(0,2);
			if(a ==1 && (activecounter<attachpointsactive))
			{
				attachpointobjs[i] = transform.GetChild(i).gameObject;
				activecounter++;
			}
			else
			{
				attachpointobjs[i] = transform.GetChild(i).gameObject;
				attachpointobjs[i].SetActive(false);
			}
		}

		for(int i=0;i<childrencount;i++)
		{
			if((activecounter!=attachpointsactive) && (!attachpointobjs[i].activeSelf))
			{
				attachpointobjs[i].SetActive(true);
				activecounter++;
			}

		}
	}

	public virtual void grabbed(Transform looker)
	{
		isgrabbed = true;
		transform.LookAt(looker);
	}

	public virtual void setinspot(Transform par)
	{
		transform.parent = par;
		col.enabled = false;
	//	rbody.isKinematic = true;
		rbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	public virtual void letgo()
	{
		isgrabbed = false;
		transform.parent = null;
		
	}

	public virtual void poweredup()
	{
		//do action when fully powered
	}

	public virtual void Update()
	{

		if(elementaddedcounter == childrencount && !fullpower)
		{
			fullpower = true;
			poweredup();
		}
	}

}
