using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewModeSelectionManger : MonoBehaviour 
{
	public static ViewModeSelectionManger instance;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			DestroyImmediate(this.gameObject);

		choicePanel.SetActive(false);
	}

	public GameObject choicePanel;
	public Callback OnStartGame;

	public void ShowViewChoicePanel()
	{
		choicePanel.SetActive(true);
	}

	public void PlayInMergeMode()
	{
		this.gameObject.SetActive(false);

		if(Merge.MergeCubeSDK.instance.viewMode != Merge.MergeCubeSDK.ViewMode.HEADSET )
			Merge.MergeCubeSDK.instance.SwitchView();

		if (OnStartGame != null)
		{
			OnStartGame.Invoke();
		}
	}

	public void PlayInMobileMode()
	{
		this.gameObject.SetActive(false);

		if(Merge.MergeCubeSDK.instance.viewMode != Merge.MergeCubeSDK.ViewMode.FULLSCREEN )
			Merge.MergeCubeSDK.instance.SwitchView();

		if (OnStartGame != null)
		{
			OnStartGame.Invoke();
		}
	}

}
