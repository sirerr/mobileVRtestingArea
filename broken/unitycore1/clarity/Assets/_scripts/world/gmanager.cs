using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gmanager : MonoBehaviour {

	public static GameObject currentarea;
	public static GameObject playerobj;

	public static Vector3 lastjumplocation;
	public static Quaternion lastjumprotation;

	public List <GameObject> levellists = new List<GameObject>();

	private static gmanager singleton;
	//in case there isn't a game manager present
	public static gmanager Singleton
	{
		get
		{
			if(singleton == null)
			{
				GameObject gameman = new GameObject("GameManager");
				gameman.transform.tag = "gm";
				singleton = gameman.AddComponent<gmanager>();
			}
			return singleton;
		}

	}


	void Awake()
	{
		playerobj = GameObject.FindGameObjectWithTag("Player");
	}



}
