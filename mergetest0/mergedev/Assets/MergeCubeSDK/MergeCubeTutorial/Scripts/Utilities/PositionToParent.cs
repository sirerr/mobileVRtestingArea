using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToParent : MonoBehaviour 
{
	
	public enum parentObjects { None, ARCamera, MultiTarget};
	public parentObjects rootParent;
	Transform parent;

	public bool startEnabled = false;
	public Vector3 localPositionToParent;
	public Vector3 localRotationToParent;




	public void ApplyReparenting()
	{
		ApplyToParent();

		if (parent != null)
		{
			transform.parent = parent;
			transform.localPosition = localPositionToParent;
			transform.localRotation = Quaternion.Euler(localRotationToParent.x, localRotationToParent.y, localRotationToParent.z);
			gameObject.SetActive(startEnabled);
		}
		else
		{
			transform.parent = null;
		}
	}

	void ApplyToParent()
	{
		switch (rootParent)
		{
			case parentObjects.None:
				parent = null;
				break;
			case parentObjects.ARCamera:
				parent = Camera.main.transform;
				break;
		case parentObjects.MultiTarget:
				parent = MergeMultiTarget.instance.transform;
				break;
			default:
				Debug.Log("ERROR: Something bad happened to " + this.gameObject.name + "'s parenting");
				break;
		}
	}

}
