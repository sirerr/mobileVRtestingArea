using UnityEngine;
using System.Collections;

public class bulbaction : MonoBehaviour {
	//amount of energy in bulb
	private int bulbpower = 0;
	//Rigidbody
	public Rigidbody rbody;
	//collider
	public Collider col;
	// movement value
	public float speed =2f;
	//random firstmoves
	private float ranx=0;
	private float rany=0;
	private float ranz=0;

	public float minForce = -10;
	public float maxForce =10;

	public int powermax =10;

	public float overridemovementspeed = 0;
	public bool movetoparent = false;
	void Awake()
	{

		rbody =GetComponent<Rigidbody>();
		col = GetComponent<Collider>();

		ranx = Random.Range(minForce,maxForce);
		rany = Random.Range(minForce,maxForce);
		ranz = Random.Range(minForce,maxForce);

		rbody.AddForce(ranx,rany,ranz);

		bulbpower = Random.Range(1,powermax);
	}

	public virtual void absorbed()
	{
		playerstats.playerposenergy += bulbpower;
	//	print(playerstats.playerposenergy);
		// will make it reappear different places 

		//for now
		Destroy(gameObject);
		//for now

	}

	public virtual void Update()
	{
		if(movetoparent)
		{
			print("going to parent");
			Vector3.MoveTowards(transform.position,transform.parent.position,overridemovementspeed * Time.deltaTime);
		}

	}

}
