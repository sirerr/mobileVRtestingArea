using UnityEngine;
using System.Collections;

public class gmanager : MonoBehaviour {

	public static GameObject currentarea;
	public static GameObject playerobj;

	public static Vector3 lastjumplocation;

	void Awake()
	{
		playerobj = GameObject.FindGameObjectWithTag("Player");
	}

}
