using UnityEngine;
using System.Collections;

public class centralaction : MonoBehaviour {

	//elements added to the central object
	public int elementadded;
	// power needed to be complete
	public float neededpower=0;
	//power collected by the center object
	public float collectedpower = 0;

	//for central mode
	public bool fullpower = false;

	//Rigidbody and collider ref
	public Rigidbody rbody;
	public Collider col;
	//the state of the central object
	public int centralstate =0;
	//is it being raycasted to
	public bool acklook = false;

	public float vel = 5f;
	void Awake()
	{	
		rbody = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
	}

	public virtual void tohole(Transform holepoint)
	{
		rbody.constraints = RigidbodyConstraints.None;
		Vector3 dir = (holepoint.position - transform.position) *vel;
		rbody.velocity = dir;
		rbody.isKinematic = false;

	}


	public virtual void centralstateaction()
	{
		if(centralstate == 0)
		{
			becomeactive();
		}else if(centralstate ==1 && !fullpower)
		{
				rbody.constraints = RigidbodyConstraints.None;
			float ranx = Random.Range(0,5);
			float rany = Random.Range(0,5);
			float ranz = Random.Range(0,5);
			rbody.AddForce(ranx,rany,ranz);
			centralstate =0;
		}else if (centralstate==1 &&fullpower)
		{
			StartCoroutine(centralstatewait());
		}
	}

	IEnumerator centralstatewait()
	{

		yield return new WaitForSeconds(3f);
		rbody.constraints = RigidbodyConstraints.FreezeRotation;
		centralstate =2;
	}

	public virtual void grabbed(Transform centercollect)
	{
		rbody.constraints = RigidbodyConstraints.FreezeAll;
		rbody.isKinematic = true;
		transform.position = centercollect.position;
		int childs = transform.childCount;
		for(int i=0;i<childs;i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}

	

	}

		
	public virtual void becomeactive()
	{
		centralstate =1;
		rbody.velocity = Vector3.zero;
		rbody.constraints = RigidbodyConstraints.FreezePosition;

		//also expand the circle around it and make it look good
	}
		

	public virtual void poweredup()
	{
		//do action when fully powered
	
		rbody.AddForce(Vector3.zero);
	}

	public virtual void Update()
	{

		if(collectedpower == neededpower)
		{
			fullpower = true;
			poweredup();
		}

		if(playerinteraction.lookedatobj == transform.gameObject)
		{
			acklook = true;
		}
		else
		{
			acklook = false;
		}

	}

	public virtual void OnCollisionEnter(Collision col)
	{
		if(col.collider.CompareTag("element"))
		{
			if(col.gameObject.GetComponent<elementaction>().captured)
			{
				col.transform.parent = transform;
				col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				elementadded++;
				collectedpower += col.gameObject.GetComponent<elementaction>().elementpower;
				col.collider.enabled = false;
			}
		}

	}

}
