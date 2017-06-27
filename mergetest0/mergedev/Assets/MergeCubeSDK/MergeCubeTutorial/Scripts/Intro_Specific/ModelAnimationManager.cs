using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAnimationManager : MonoBehaviour 
{

	public Animator modelAnimator;
	string animTag = "Tutorial_Anim";
	public List<GameObject> floatingTargets = new List<GameObject>();

	GameObject multiTarget;
	void Awake()
	{
		multiTarget = GameObject.Find("MultiTarget");
	}

	bool isInit = false;
	void OnEnable()
	{
		if (!isInit) {
			isInit = true;
			transform.parent.FaceToCamera (this.transform);
			//Merge.CubeOrientation.OrientateToCamera(multiTarget.transform, transform.parent);
		}
	}
		

	AnimatorStateInfo currentState;
	public void ResetCurrentState()
	{
		currentState = modelAnimator.GetCurrentAnimatorStateInfo(0);
		if(currentState.IsTag(animTag))
		{
			modelAnimator.Play(currentState.fullPathHash, -1, 0f);
		}
	}


	public void setState(int stateIndex)
	{
		switch (stateIndex) {
		case 0:
			modelAnimator.SetTrigger ("0");
			break;
		case 1:
			modelAnimator.SetTrigger ("1");
			break;
		case 2:
			modelAnimator.SetTrigger ("2");
			break;
		case 3:
			modelAnimator.SetTrigger ("3");
			break;
		case 4: 
			DisableFloatingTargets();
			modelAnimator.SetTrigger ("4");
			break;
		case 5: 
			modelAnimator.SetTrigger ("5");
			break;
		case 6: 
			modelAnimator.SetTrigger ("6");
			break;
		default:
			Debug.Log ("Animation change failure.");
			break;
		}
	}


	public void DisableFloatingTargets()
	{
		foreach (GameObject target in floatingTargets)
		{
			target.SetActive(false);
		}
	}
}
