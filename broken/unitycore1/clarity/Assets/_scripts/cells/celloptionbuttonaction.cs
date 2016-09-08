using UnityEngine;
using System.Collections;

public class celloptionbuttonaction : MonoBehaviour {

	public cellaction cellactionref;

	void Start () {
	
		cellactionref = transform.GetComponentInParent<cellaction>();
		transform.LookAt(cellactionref.spawnloc);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
