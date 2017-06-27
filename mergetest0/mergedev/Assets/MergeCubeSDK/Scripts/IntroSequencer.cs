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

	public bool shouldAutoStart = true;

	void Start()
	{
		if(shouldAutoStart)
			StartIntroSequencer();
	}

	public Callback OnIntroSequenceComplete;
	public void StartIntroSequencer()
	{
		SplashScreenManager.instance.OnSplashSequenceEnd += StartSocialLogin;
		ViewModeSelectionManger.instance.OnStartGame += StartGame;

		SplashScreenManager.instance.StartSplashSequence();
	}

	void StartSocialLogin()
	{
		ViewModeSelectionManger.instance.ShowViewChoicePanel();
	}

	void StartGame()
	{
		if (OnIntroSequenceComplete != null)
		{
			OnIntroSequenceComplete.Invoke();
		}
	}
}
