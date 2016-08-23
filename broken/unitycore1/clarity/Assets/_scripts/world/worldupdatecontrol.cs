using UnityEngine;
using System.Collections;

public class worldupdatecontrol : MonoBehaviour {

	protected bool gamepause = false;

	public virtual void gameupdate()
	{
		
	}

	public virtual void gamefixedupdate()
	{
		
	}
		

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(!gamepause)
		{
			gameupdate();
		}

	}

	void FixedUpdate()
	{
		if(!gamepause)
		{
			gamefixedupdate();
		}
	}
}
