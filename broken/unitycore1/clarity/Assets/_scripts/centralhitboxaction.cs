using UnityEngine;
using System.Collections;

public class centralhitboxaction : MonoBehaviour {

	private bool occupied = false;
	private Transform par;

	void OnEnable()
	{
		par = transform.parent;

	}


}
