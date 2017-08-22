using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeReticle : MonoBehaviour {
	public static MergeReticle instance;
	public Sprite fullScreenSprite;
	public Sprite vrScreenSprite;
	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		Merge.MergeCubeSDK.instance.OnViewModeSwap += SetMode;
	}
	public void SetMode(bool isVRMode){
		transform.GetChild (0).GetComponent<SpriteRenderer> ().sprite = isVRMode ? vrScreenSprite : fullScreenSprite;
	}
	public void ActiveIt(bool isActive){
		transform.GetChild (0).gameObject.SetActive (isActive);
	}
}
