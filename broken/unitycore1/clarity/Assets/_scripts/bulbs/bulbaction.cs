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
	public int ranx=0;
	public int rany=0;
	public int ranz=0;

	public int powermax =10;

	public float overridemovementspeed = 0;
	public bool movetoparent = false;
	void Awake()
	{

		rbody =GetComponent<Rigidbody>();
		col = GetComponent<Collider>();

		ranx = Random.Range(-5,5);
		rany = Random.Range(-5,5);
		ranz = Random.Range(-5,5);

		rbody.AddForce(ranx,rany,ranz);

		bulbpower = Random.Range(1,powermax);
	}

	public void absorbed()
	{
		playerstats.playerposenergy += bulbpower;
	//	print(playerstats.playerposenergy);
		// will make it reappear different places 

		//for now
		Destroy(gameObject);
		//for now

	}

	public void Update()
	{
		if(movetoparent)
		{
			Vector3.MoveTowards(transform.position,transform.parent.position,overridemovementspeed * Time.deltaTime);
		}

	}

}
