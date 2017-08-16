using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour 
{
	public static TitleScreenManager instance;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			DestroyImmediate(this.gameObject);

		titleScreen.SetActive(false);
	}

	public GameObject titleScreen;
	public Transform mergeModeButton;
	public Transform mobileModeButton;
	public Sprite mergeModeDefaultSprite;
	public Sprite mergeModeDisabledSprite;

	public Transform playTutorialToggle;
	public Sprite playTutorialTrueSprite;
	public Sprite playTutorialFalseSprite;

	public bool shouldPlayTutorial { get; private set; }

	public Callback<bool> OnTitleSequenceComplete;

	//Entry Method
	public void ShowTitleScreen()
	{
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = true;
		
		if (Merge.MergeCubeSDK.deviceIsTablet)
		{
			DisableMergeMode();
		}

		if (MergeTutorial.ins == null)
		{
			shouldPlayTutorial = false;
			playTutorialToggle.gameObject.SetActive(false);
		}

		shouldPlayTutorial = !PlayerPrefs.HasKey("HasPlayedBefore");
		playTutorialToggle.GetComponent<Image>().sprite = (shouldPlayTutorial) ? playTutorialTrueSprite : playTutorialFalseSprite;

		titleScreen.SetActive(true);
	}

	//State Management
	public void DisableMergeMode()
	{
		//Show disabled graphics for MergeMode Button
		mergeModeButton.GetComponent<Image>().sprite = mergeModeDisabledSprite;
		mergeModeButton.GetComponent<Button>().interactable = false;
	}

	public void ToggleShouldPlayTutorial()
	{
		shouldPlayTutorial = !shouldPlayTutorial;	
		playTutorialToggle.GetComponent<Image>().sprite = (shouldPlayTutorial) ? playTutorialTrueSprite : playTutorialFalseSprite;
	}


	//Exit Conditions called by GUI elements
	public void PlayInMergeMode()
	{
		this.gameObject.SetActive(false);

		if(Merge.MergeCubeSDK.instance.viewMode != Merge.MergeCubeSDK.ViewMode.HEADSET )
			Merge.MergeCubeSDK.instance.SwitchView();

		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;

		if (OnTitleSequenceComplete != null)
		{
			OnTitleSequenceComplete.Invoke( shouldPlayTutorial );
		}
	}

	public void PlayInMobileMode()
	{
		this.gameObject.SetActive(false);

		if(Merge.MergeCubeSDK.instance.viewMode != Merge.MergeCubeSDK.ViewMode.FULLSCREEN )
			Merge.MergeCubeSDK.instance.SwitchView();

		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;

		if (OnTitleSequenceComplete != null)
		{
			OnTitleSequenceComplete.Invoke( shouldPlayTutorial );
		}
	}

}
