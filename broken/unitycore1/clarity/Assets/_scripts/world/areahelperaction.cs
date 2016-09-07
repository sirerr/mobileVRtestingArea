using UnityEngine;
using System.Collections;

public class areahelperaction : MonoBehaviour {

	public float expansionsizeinc =10;

	private bool attack = false;
	public float waittimebeforeexpanding =4;
	public float activetime = 30;
	private Vector3 sizescale;
	void OnEnable()

	{
		// show energy glowing, getting bigger
		StartCoroutine(bornthendie());
	}

	IEnumerator bornthendie()
	{
		yield return new WaitForSeconds(waittimebeforeexpanding);
		attack = true;
		sizescale = transform.localScale;
		yield return new WaitForSeconds(activetime);
		Destroy(gameObject);
	}


	void Update()
	{

		if(attack)
		{
			sizescale.x += expansionsizeinc *Time.deltaTime;
			sizescale.y += expansionsizeinc *Time.deltaTime;
			sizescale.z += expansionsizeinc *Time.deltaTime;
			transform.localScale = sizescale;
		}
	}

}
