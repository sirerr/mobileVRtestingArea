using UnityEngine;
using System.Collections;

public class headcontrol : MonoBehaviour {

	public MouseLook mref;

	void Update()
	{
		if(Input.GetMouseButton(1))
		{
			mref.enabled = true;
		}
		else
		{
			mref.enabled = false;
		}
	}

}
