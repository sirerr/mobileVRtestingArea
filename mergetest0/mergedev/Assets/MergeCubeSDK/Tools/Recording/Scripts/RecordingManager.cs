
//Uncomment the #define line to use the recording extensions for iVidCapPro for iOS.
//Android recording support is in progress and will be released in a later patch
//The plugin can be acquired here:
//http://eccentric-orbits.com/eoe/site/ividcappro-unity-plugin/

//For iOS:
//A iVidCapPro component MUST be added to this gameobject manually. Programatically placing it has issues when running.

//For Android:
//Import Images2Video plugin. Drop in a VirtualCamera prefab from the Images2Video folder to the exposed variable on this manager.

//#define SHOULD_USE_RECORDING_PLUGIN

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingManager : MonoBehaviour 
{

	#if SHOULD_USE_RECORDING_PLUGIN
	public readonly static bool isUsingRecordingPlugin = true;
	#else
	public readonly static bool isUsingRecordingPlugin = false;
	#endif
	
	#if SHOULD_USE_RECORDING_PLUGIN

	public static RecordingManager instance;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			DestroyImmediate(this.gameObject);
	}

	#if UNITY_IOS
	private iVidCapPro vidCapManager;
	private int recFrames = 0;
	#endif

	bool isRecording = false;
	bool isInitialized = false;

	#if UNITY_ANDROID
	public tw.com.championtek.VirtualCamera androidRecordingCamera;
	#endif

	Callback cb;


	void Start()
	{
		Initialize();

		#if UNITY_IOS
		vidCapManager = this.GetComponent<iVidCapPro>();
		vidCapManager.RegisterSessionCompleteDelegate(HandleSessionCompleteDelegate);
		#endif
		
	}

	void Initialize()
	{
//		if (Camera.main.GetComponent<tw.com.championtek.VirtualCamera>() == null)
//		{
//			Camera.main.gameObject.AddComponent<tw.com.championtek.VirtualCamera>();
//		}
//
		//Note: You must add this to the recording manager!
		#if UNITY_IOS
		vidCapManager = this.GetComponent<iVidCapPro>();
		
		
		if (Camera.main.GetComponent<Blitter>() == null)
		{
			Camera.main.gameObject.AddComponent<Blitter>();	
		}


		if (Camera.main.gameObject.GetComponent<AudioListener>() == null)
		{
			Camera.main.gameObject.AddComponent<AudioListener>();

			if (Camera.main.transform.parent.GetComponent<AudioListener>() != null)
			{
				//remove vuforia's listener in favor of somewhere we expect it...
				//we can only use one audio listener, and we want to make sure it is with the iVidCapProAudio guy as he is the one handling recording audio through it.
				DestroyImmediate(Camera.main.transform.parent.GetComponent<AudioListener>());
			}
		}

		if (Camera.main.transform.parent.GetComponent<iVidCapProAudio>() == null)
		{
			Camera.main.gameObject.AddComponent<iVidCapProAudio>();
		}

		vidCapManager.saveAudio = Camera.main.GetComponent<iVidCapProAudio>();
//		vidCapManager.SetDebug(true);
		#endif

//		#if UNITY_ANDROID
//		androidRecordingCamera = Camera.main.GetComponent<tw.com.championtek.VirtualCamera>();
//		#endif
	}

	private void SetRecordingTexture()
	{
		Camera.main.GetComponent<Blitter>().enabled = true;

		#if UNITY_IOS
		Debug.Log("Should have set a recording texture to: " + vidCapManager);
		vidCapManager.SetCustomRenderTexture(Camera.main.GetComponent<Blitter>().mainCameraTexture);
		#endif

//		#if UNITY_ANDROID
//		androidRecordingCamera.SetRenderTexture(Camera.main.GetComponent<Blitter>().mainCameraTexture);
//		#endif

		isInitialized = true;

	}

	public void StartRec( string outputName, Callback recCompleteCb )
	{
		#if UNITY_EDITOR
		Debug.Log("Cannot make recordings in editor");
		return;
		#endif

		SetRecordingTexture();

		if (!isRecording)
		{
			#if UNITY_ANDROID
			//Images2Video Style:

			Camera recCam = androidRecordingCamera.GetComponent<Camera>();

			RenderTexture rt = recCam.targetTexture;
			recCam.CopyFrom(Camera.main);
			recCam.targetTexture = rt;

			androidRecordingCamera.outputVideoName = outputName;
			androidRecordingCamera.BeginShot();
			#endif

			#if UNITY_IOS
			//VidCapPro Style:
			Debug.Log("Starting recording session");
			vidCapManager.BeginRecordingSession(outputName, 1334, 750, 60f, iVidCapPro.CaptureAudio.Audio, iVidCapPro.CaptureFramerateLock.Unlocked);
			#endif

			if(recCompleteCb != null)
			{
				cb = recCompleteCb;
			}

			isRecording = true;
		}
	}


	public void StopRec()
	{
		#if UNITY_EDITOR
		Debug.Log("Cannot make recordings in editor");
		return;
		#endif

		if (!isInitialized)
			return;

		Debug.Log("Stop Rec try");

		if (isRecording)
		{
			#if UNITY_ANDROID
			//Images2Video Style:
			androidRecordingCamera.EndShot(HandleSessionCompleteDelegate);
			#endif

			#if UNITY_IOS
			//VidCapPro Style:
			vidCapManager.EndRecordingSession(iVidCapPro.VideoDisposition.Save_Video_To_Album, out recFrames);
			//session complete delegate registered via VidCapPro
			#endif
			
			Camera.main.GetComponent<Blitter>().enabled = false;
			Camera.main.targetTexture = null;

			isRecording = false;
		}
	}

	private void HandleSessionCompleteDelegate()
	{
		if (cb != null)
		{
			cb.Invoke();
			cb = null;
		}
	}

	public GameObject recordStartExample;
	public GameObject recordSavingExample;



	//called when recording starts
	//Setup for waiting before recording. (Show rec symbol, picture frames, disable game controls, etc)
	void HandleRecStartSetup()
	{
		Debug.Log("HandleRecStartSetup");
		recordStartExample.SetActive(true);
		Merge.MergeCubeSDK.instance.RemoveMenuElement(Merge.MergeCubeSDK.instance.viewSwitchButton);
	}

	//called when recording ends and begins saving
	//Setup for waiting while recording. (Loading screen, disable game controls, etc)
	void HandleRecCompleteSetup()
	{
		Debug.Log("HandleRecCompleteSetup");
		recordStartExample.SetActive(false);
		recordSavingExample.SetActive(true);
	}

	//Called when recording finishes saving.
	//Setup for game logic. (Loading screen, disable game controls, etc)
	void HandleRecSaveComplete()
	{
		Debug.Log("HandleRecSaveComplete");
		recordSavingExample.SetActive(false);
		Merge.MergeCubeSDK.instance.AddMenuElement(Merge.MergeCubeSDK.instance.viewSwitchButton, 3);
	}

	#endif

	//Currently being called by the Recording button in the scene. Saves videos as day_month_year_hour_minute
	public void ToggleRecording()
	{
		#if SHOULD_USE_RECORDING_PLUGIN
		#if UNITY_EDITOR
		Debug.Log("Cannot make recordings in editor");
		return;
		#endif

		if (!isRecording)
		{
			HandleRecStartSetup();
			StartRec( System.DateTime.Now.Day.ToString()+"_"+System.DateTime.Now.Month.ToString()+"_"+
				System.DateTime.Now.Year.ToString()+"_"+System.DateTime.Now.Hour.ToString()+"_"+System.DateTime.Now.Minute.ToString(), HandleRecSaveComplete );
			isRecording = true;
		}
		else
		{
			HandleRecCompleteSetup();
			StopRec();
			isRecording = false;
		}

		#endif
	}

}
