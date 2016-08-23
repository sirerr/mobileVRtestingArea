using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cellaction : MonoBehaviour {
	//required energy to have before being fine to leave
	public int requiredpower = 10;
	//returns to the area start point
	public GameObject returnobj;
	private Vector3 returnobjlocation;
	//being looked at
	public bool acklook;
	//is the cell completed
	public bool celldone = false;
	//the power added by the cells
	public int addedpower =0;
	//spawn location
	public Transform spawnloc;

	//rotation vars
	//default rotation
	private Vector3 defaultrotation;
	private Vector3 currentrotation;
	private Vector3 lastdirection;

	public bool dorotate = false;
	public bool gotvalues = false;
	public Vector3 raypoint;

	//objects to create when the scene begins
	public GameObject elementobj;
	public GameObject centralobj;
	public GameObject bulbobj;

	private bool firstlook = false;

	public GameObject cellskinobj;
	private Color finishedcellskin;

	private GameObject makerobj;

	public virtual	void Awake()
	{
		finishedcellskin = Color.cyan;
		//create the elements, centers and bulbs
		StartCoroutine(populate());

		defaultrotation = transform.rotation.eulerAngles;
		returnobjlocation = returnobj.transform.position;
	}

	public virtual IEnumerator populate()
	{
		makerobj = new GameObject("maker");
		makerobj.transform.position = transform.position;


		for(int i = 0; i<=requiredpower;i++)
		{
			GameObject ele = Instantiate(elementobj,transform.position,transform.rotation) as GameObject;
			ele.transform.parent = makerobj.transform;
			yield return new WaitForSeconds(.5f);
		}

		for(int i = 0; i<5;i++)
		{
			GameObject cen = Instantiate(centralobj,transform.position,transform.rotation) as GameObject; 
			cen.transform.parent = makerobj.transform;
			yield return new WaitForSeconds(.5f);
		}

		int count = Random.Range(1,10);
		for(int i =0; i<=count;i++)
		{
			GameObject bulb = Instantiate(bulbobj,transform.position,transform.rotation) as GameObject;
			bulb.transform.parent = makerobj.transform;
			yield return new WaitForSeconds(.5f);
		}

	}

	public virtual void finishedcell()
	{
	//	returnobj.SetActive(true);

		cellskinobj.GetComponent<MeshRenderer>().material.color = finishedcellskin;

	}

	public virtual void Update()
	{
		//returnobj.transform.position = returnobjlocation;

		if(playerinteraction.lookedatobj == transform.gameObject)
		{
			acklook = true;
			if(!firstlook)
			{
				firstlook = true;
			}
		}
		else
		{
			acklook = false;

		}

		if(addedpower >= requiredpower && !celldone)
		{
			celldone = true;
			finishedcell();
		}

		if(dorotate)
		{
			rotatecell();
		}
			
	}

	public virtual void getrotateready(Vector3 lookpoint)
	{
		if(!gotvalues)
		{		
			currentrotation = transform.rotation.eulerAngles;
			lastdirection = lookpoint - transform.position;
			gotvalues = true;
		}
	}

	public virtual void rotatecell()
	{
		Vector3 targetdir = raypoint - transform.position;

		Quaternion newdir = Quaternion.FromToRotation(lastdirection,targetdir);

		transform.rotation = newdir * transform.rotation;
		lastdirection = targetdir;

	}

	public virtual void leavecell()
	{

		gmanager.playerobj.transform.position = gmanager.lastjumplocation;
		gmanager.playerobj.transform.rotation = gmanager.lastjumprotation;
//		print(gmanager.lastjumplocation);
//		print(gmanager.lastjumprotation);
	}

	public virtual Vector3 arriveincelllocation()
	{
		return spawnloc.position;
	}

	public virtual Quaternion arriveincellrotation()
	{
		return spawnloc.rotation;
	}
}
