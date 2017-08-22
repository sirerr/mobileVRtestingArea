using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequencer : MonoBehaviour
{
	public static IntroSequencer instance;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
		{
			DestroyImmediate(this.gameObject);
			return;
		}
			
		DontDestroyOnLoad(gameObject);
	}

	//This allows the intro sequence to play out of the box with no other managers handling calling it's start.
	public bool shouldAutoStart = true;
	public Callback OnIntroSequenceComplete;

	void Start()
	{
		Merge.MergeCubeSDK.instance.OnInitializationComplete += SignalSDKReady;

		if (shouldAutoStart)
			StartCoroutine(WaitForSDKInit());
	}
		
	bool mergeCubeSDKReady = false;
	void SignalSDKReady()
	{
		mergeCubeSDKReady = true;
	}

	public void StartIntroSequencer()
	{
		StartCoroutine(WaitForSDKInit());
	}

	IEnumerator WaitForSDKInit()
	{
		yield return new WaitUntil( () => mergeCubeSDKReady );
		BeginSequencer();
	}

	//Entry
	void BeginSequencer()
	{
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;

		Merge.MergeCubeSDK.instance.RemoveMenuElement(Merge.MergeCubeSDK.instance.viewSwitchButton);

		SplashScreenManager.instance.OnSplashSequenceEnd += HandleSplashSequenceComplete;
		TitleScreenManager.instance.OnTitleSequenceComplete += HandleTitleSequenceComplete;

		SplashScreenManager.instance.StartSplashSequence();
	}

	void HandleSplashSequenceComplete()
	{
		TitleScreenManager.instance.ShowTitleScreen();
	}

	void HandleTitleSequenceComplete( bool shouldPlayTutorial )
	{
		if (shouldPlayTutorial)
		{
			MergeTutorial.ins.OnTutorialComplete += HandleTutorialSequenceComplete;

			if (!PlayerPrefs.HasKey("HasPlayedBefore"))
			{
				PlayerPrefs.SetString("HasPlayedBefore", "true");
			}

			MergeTutorial.ins.StartTutorial(Merge.MergeCubeSDK.instance.viewMode == MergeCube.MergeCubeBase.ViewMode.FULLSCREEN);

		}
		else 
		{
			EndIntroSequence();
		}
	}

	void HandleTutorialSequenceComplete()
	{
		EndIntroSequence();
	}
		
	//Exit
	void EndIntroSequence()
	{
		Merge.MergeCubeSDK.instance.AddMenuElement(Merge.MergeCubeSDK.instance.viewSwitchButton, 3);

		if(OnIntroSequenceComplete != null)
		{
			OnIntroSequenceComplete.Invoke();
		}
	}
}
