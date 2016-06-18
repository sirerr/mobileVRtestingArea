using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ballonmovement : MonoBehaviour {

	public Rigidbody rbody;
	public float ballonforcey =0;
	public float addforce =0;
	//direction to go in
	public Vector3 newdirection;

	// Use this for initialization
	void Start () {
	
	}
	void Awake()
	{
		rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void direction()
	{
		rbody.AddForce(newdirection *addforce);

	}

	public void letgo()
	{
		rbody.AddForce(Vector3.up * ballonforcey);
		rbody.AddForce(Vector3.forward * ballonforcey *2);
		StartCoroutine(correctmovement());
	}

	IEnumerator correctmovement()
	{
		yield return new WaitForSeconds(2f);
		rbody.AddForce(Vector3.zero);
		rbody.AddForce(Vector3.up * ballonforcey);
	}
}
