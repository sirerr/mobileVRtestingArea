using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MergeCube;
using UnityEngine.EventSystems;
using Vuforia;

namespace Merge
{
	public class MergeCubeSDK : MergeCubeBase
	{
		public static MergeCubeSDK instance;

		void Awake()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				DestroyImmediate(this.gameObject);

			#if !UNITY_EDITOR
			if (SystemInfo.deviceModel.Contains("iPad") || (Application.platform != RuntimePlatform.IPhonePlayer && DeviceDiagonalSizeInInches() >= 7.5f))
			{
				Debug.Log("This is a tablet!");
				deviceIsTablet = true;
			}
			#else
			deviceIsTablet = false;
			#endif

		}
			
		private static ViewMode currentViewMode = ViewMode.FULLSCREEN;
		public static bool deviceIsTablet { get; private set; }
		private Transform arCameraRef;

		private VideoBackgroundBehaviour leftVidBackBehaviour;
		private VideoBackgroundBehaviour rightVidBackBehaviour;

		public GameObject headsetViewSetup;
		private RenderTexture headsetViewRenderTexture;
		public RenderTexture GetHeadsetTexture(){return headsetViewRenderTexture;}

		private bool isActive = false;

		public CanvasScaler canvasScaler;

		public Transform[] allUIPositions;
//		public Transform[] dynamicUIPositions;

		[SerializeField]
		public List<MenuButton> menuButtons = new List<MenuButton>();

		//fixed position buttons
		public Transform menuButton; //0
		public Transform userAccountsButton; //5

		//dynamic position buttons
		public Transform flashlightButton; // almost always, may have edge cases
		public Transform viewSwitchButton; //not on ipad
		public Transform headsetCompatabilityButton; //Only visible when in headset mode.
		public Transform recordingButton; // not if there aren't plugins for it

		private int menuButtonPriority = 0;
		private int userAccountsPriority = 5;
		private int recordingPriority = 1;
		private int flashlightPriority = 2;
		private int viewSwitchPriority = 3;
		private int headsetCompatabilityPriority = 4;

		//Extra divider elements
		public Transform dividerB;
		public Transform dividerC;
		public Transform dividerD;

		public UnityEngine.UI.Image viewSwitchGraphic;
		public Sprite fullscreenSprite;
		public Sprite headsetViewSprite;
		public Sprite disabledSprite;

		public Text userAccountName;
		public Color defaultPurple;

		private GameObject reticleInstance;
		public GameObject reticlePrefab;

		public Animator mainPanelAnimator;
		bool menuIsOpen = false;

		public delegate void ViewModeSwapEvent(bool swappedToHeadsetView);
		public Callback OnInitializationComplete;
		public Callback<DeviceOrientation> OnOrientationEvent;

		public ViewModeSwapEvent OnViewModeSwap;

		public bool useMergeUserAccount = true;
		public bool userAccountDebugMode = false;
		void Start()
		{
			//unity base settings setup:
			Application.targetFrameRate = 60;

			//unity base scene setup:
			if (GameObject.Find("EventSystem") == null)
			{
				GameObject tmp = new GameObject("EventSystem");
				EventSystem evnSys = tmp.AddComponent<EventSystem>();
				StandaloneInputModule inpModule = tmp.AddComponent<StandaloneInputModule>();
			}

			//our setup:
			Initialize();
		}

		public void LaunchUserLoginPage()
		{
			//This is a separate component so we'll check for user error
			if (MergeUserAccount.instance != null)
			{
				MergeUserAccount.instance.OpenUserAccount();
			}
			else
			{
				Debug.LogError("The User Accounts system must be included in order to use this feature!");
			}

		}

		void HandleUserLogin( string result )
		{
			if (result != "")
			{
				userAccountName.text = MergeUserAccount.instance.UserName;
				userAccountName.color = Color.white;
				Debug.LogError("User name set to :" + MergeUserAccount.instance.UserName);
			}
			else
			{
				userAccountName.text = "ACCOUNT";
				userAccountName.color = defaultPurple;
				Debug.LogError("Username set to null. Name is now ACCOUNT!");
			}
		}
			
		void Initialize()
		{
			if (useMergeUserAccount) {				
				userAccountsButton.gameObject.SetActive (true);
				MergeUserAccount.instance.loginResult += HandleUserLogin;
				MergeUserAccount.instance.debugMode = userAccountDebugMode;
				MergeUserAccount.instance.Init ();
			}

			if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
			{
				Screen.autorotateToLandscapeLeft = true;
				Screen.autorotateToLandscapeRight = true;
				Screen.autorotateToPortrait = false;
				Screen.autorotateToPortraitUpsideDown = false;
			}
			else
			{
				Screen.autorotateToLandscapeLeft = true;
				Screen.autorotateToLandscapeRight = true;
				Screen.autorotateToPortrait = true;
				Screen.autorotateToPortraitUpsideDown = false;
			}

			#if UNITY_ANDROID

			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

			Debug.Log("Screen dimensions:" + Screen.width + ", " + Screen.height);

			canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
			canvasScaler.matchWidthOrHeight = (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) ? 0f : 1f;

//			if ((Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight))
//			{
//				if (Screen.width > 1680f)
//					canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
//				else
//					canvasScaler.referenceResolution = new Vector2(1680f, 1050f);
//			}
//			else
//			{
//				if (Screen.width > 1050f)
//					canvasScaler.referenceResolution = new Vector2(Screen.height, Screen.width);
//				else
//					canvasScaler.referenceResolution = new Vector2(1050f, 1680f);
//			}

			canvasScaler.matchWidthOrHeight = (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) ? 0f : 1f;

			#elif UNITY_IOS

			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
			canvasScaler.scaleFactor = 0.8f;

			#endif

			AddMenuElement(flashlightButton, flashlightPriority);

			if (!deviceIsTablet)
			{
				AddMenuElement(viewSwitchButton, viewSwitchPriority);
			}

			if (RecordingManager.isUsingRecordingPlugin)
			{
				AddMenuElement(recordingButton, recordingPriority);
			}
				
			//sort menubuttons by priority
			RefreshMenuButtonPositions();

			arCameraRef = Camera.main.transform;

			viewMode = currentViewMode;

			if (headsetViewRenderTexture == null) 
			{
				CreateRenderTexture ();
			}

			Camera.main.targetTexture = headsetViewRenderTexture;

			if (currentViewMode == ViewMode.HEADSET)
			{
				headsetViewSetup.SetActive(true);
			}
			else
			{
				headsetViewSetup.SetActive(false);
			}

			//This is to make sure that this version of the sdk has a way to push the contents of the target render texture to the screen in mobile mode.
			if (Camera.main.GetComponent<Blitter>() == null)
			{
				Camera.main.gameObject.AddComponent<Blitter>();	
			}

			if (reticleInstance == null && reticlePrefab != null)
			{
				reticleInstance = GameObject.Instantiate(reticlePrefab, arCameraRef.position, arCameraRef.rotation, arCameraRef) as GameObject; 
			}

			if (OnInitializationComplete != null)
			{
				OnInitializationComplete.Invoke();
			}
		}



		void Update()
		{
			if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight
				|| Input.deviceOrientation == DeviceOrientation.Portrait )
			{
				canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
				canvasScaler.matchWidthOrHeight = (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) ? 0f : 1f;

				if (OnOrientationEvent != null)
				{
					OnOrientationEvent.Invoke(Input.deviceOrientation);
				}
			}
		}


		public void RefreshMenuButtonPositions()
		{
			//sort menubuttons by priority
			menuButtons.Sort((x,y)=> x.targetPosition.CompareTo(y.targetPosition));

			for (int index = 0; index < menuButtons.Count; index++)
			{
				if (menuButtons[index].useFixedPosition)
				{
					menuButtons[index].button.parent = allUIPositions[menuButtons[index].targetPosition];
				}
				else
				{
					menuButtons[index].button.parent = allUIPositions[index];
				}

				menuButtons[index].button.localPosition = Vector3.zero;
			}

			if (menuButtons.Count >= 4)
			{
				dividerB.gameObject.SetActive(true);
			}
			else if(menuButtons.Count < 4)
			{
				dividerB.gameObject.SetActive(false);
			}
			if (menuButtons.Count >= 5)
			{
				dividerC.gameObject.SetActive(true);
			}
			else if(menuButtons.Count < 5)
			{
				dividerC.gameObject.SetActive(false);
			}
			if (menuButtons.Count == 6)
			{
				dividerD.gameObject.SetActive(true);
			}
			else if (menuButtons.Count != 6)
			{
				dividerD.gameObject.SetActive(false);
			}
				
		}

		public void AddMenuElement( Transform button, int targetPosition, bool useFixedPosition = false )
		{
			if(menuButtons.Find(x => x.button == button ) != null )
			{
				return;
			}

			button.gameObject.SetActive(true);

			MenuButton buttonToAdd = new MenuButton(button, targetPosition, useFixedPosition);
			menuButtons.Add(buttonToAdd);

			RefreshMenuButtonPositions();
		}

		public void RemoveMenuElement( Transform button )
		{
			MenuButton buttonToRemove = menuButtons.Find(x => x.button == button); 

			if ( buttonToRemove != null )
			{
				menuButtons.Remove(buttonToRemove);
				button.gameObject.SetActive(false);
			}

			RefreshMenuButtonPositions();
		}

		public Material videoTexture;
		void CreateRenderTexture()
		{
			headsetViewRenderTexture = new RenderTexture(1488, 750, 24, RenderTextureFormat.ARGB32);
			headsetViewRenderTexture.Create();

			videoTexture.SetTexture("_Texture", headsetViewRenderTexture);

//			Renderer[] childs = GetComponentsInChildren<Renderer>(true);
//			foreach (Renderer tp in childs)
//			{
//				if (tp.name == "L" || tp.name == "R")
//				{
//					tp.sharedMaterial.SetTexture("_Texture", headsetViewRenderTexture);
//				}
//			}
		}

		void OnValidate()
		{
			currentViewMode = viewMode;

			if (currentViewMode == ViewMode.HEADSET)
			{
				headsetViewSetup.SetActive(true);
			}
			else
			{
				headsetViewSetup.SetActive(false);
			}
		}

		public void ToggleMenu()
		{
			if (menuIsOpen)
			{
				mainPanelAnimator.Play("Close");
			}
			else
			{
				mainPanelAnimator.Play("Open");
			}

			menuIsOpen = !menuIsOpen;

			//if the menu close from some other button, swap the menu button back to the off state.
			if (!menuButton.GetComponentInChildren<ToggleSprite>().isOn && menuIsOpen == false)
			{
				menuButton.GetComponentInChildren<ToggleSprite>().ToggleSpriteVisuals();
			}

			//And same here, make sure the menu's aren't out of sync with each other. The viewmode check is to make sure the menu actually is active.
			//Don't need to worry about this in the fullscreen mode.
			if (currentViewMode == ViewMode.HEADSET)
			{
				if (ExpandingMenuManager.instance.isOpen && menuIsOpen == false)
				{
					ExpandingMenuManager.instance.CollapseMenu();
				}
			}
		}

		public void SwitchView()
		{
			if (currentViewMode == ViewMode.HEADSET)
			{
				currentViewMode = ViewMode.FULLSCREEN;
				viewMode = ViewMode.FULLSCREEN;
			}
			else
			{
				currentViewMode = ViewMode.HEADSET;
				viewMode = ViewMode.HEADSET;
			}

			if (currentViewMode == ViewMode.HEADSET)
			{
				SetToHeadsetView();
				viewSwitchGraphic.sprite = headsetViewSprite;
			}
			else
			{
				SetToFullscreenView();
				viewSwitchGraphic.sprite = fullscreenSprite;
			}
				
			RemoveMenuElement(viewSwitchButton);

			if (OnViewModeSwap != null)
			{
				OnViewModeSwap.Invoke((viewMode == ViewMode.HEADSET));
			}

			EnableViewChangeBtn();
		}

		void EnableViewChangeBtn()
		{
			AddMenuElement(viewSwitchButton, viewSwitchPriority);
		}


		void SetToFullscreenView()
		{
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = false;

			Camera.main.targetTexture = null;
			headsetViewSetup.SetActive(false);

			canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
			canvasScaler.matchWidthOrHeight = (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) ? 0f : 1f;

			RemoveMenuElement(headsetCompatabilityButton);
		}

		public GameObject rotatePhoneGraphic;
		void SetToHeadsetView()
		{
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.autorotateToPortrait = false;
			Screen.autorotateToPortraitUpsideDown = false;

			if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown )
			{
				#if UNITY_EDITOR
				Debug.Log("Forcing flip event for unity editor");
				FinishHeadsetViewSetup();
				#else
				rotatePhoneGraphic.SetActive(true);
				OnOrientationEvent += HandleOrientationEvent;
				#endif
			}
			else
			{
				FinishHeadsetViewSetup();
			}

			canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
			canvasScaler.matchWidthOrHeight = (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight) ? 0f : 1f;
		}

		public void HandleOrientationEvent( DeviceOrientation grabbedOrientation )
		{
			if (grabbedOrientation == DeviceOrientation.LandscapeLeft || grabbedOrientation == DeviceOrientation.LandscapeRight)
			{
				//Clean event handler
				OnOrientationEvent -= HandleOrientationEvent;

				//Finish setup after a half second to give the device time to finish rotating.
				Invoke("FinishHeadsetViewSetup",0.5f);
				rotatePhoneGraphic.SetActive(false);
			}
		}

		void FinishHeadsetViewSetup()
		{
			if (headsetViewRenderTexture == null)
			{
				CreateRenderTexture();
			}

			Camera.main.targetTexture = headsetViewRenderTexture;

			headsetViewSetup.SetActive(true);

			AddMenuElement(headsetCompatabilityButton, headsetCompatabilityPriority);

//			AdjustLense();
		}

		bool isFlashOn = false;

		public void SwitchFlashLight()
		{
			isFlashOn = !isFlashOn;

			if (isFlashOn)
			{
				TurnFlashOn();
			}
			else
			{
				TurnFlashOff();
			}
		}

		void TurnFlashOff()
		{
			Vuforia.CameraDevice.Instance.SetFlashTorchMode(false);
		}

		void TurnFlashOn()
		{
			Vuforia.CameraDevice.Instance.SetFlashTorchMode(true);
		}

		//Used to determine if the device is a tablet. iOS types check if it is an iPad via SystemInfo.deviceModel.Contains("iPad") in Awake check
		float DeviceDiagonalSizeInInches ()
		{
			float screenWidth = Screen.width / Screen.dpi;
			float screenHeight = Screen.height / Screen.dpi;
			float diagonalInches = Mathf.Sqrt (Mathf.Pow (screenWidth, 2) + Mathf.Pow (screenHeight, 2));

			Debug.Log ("Getting device inches: " + diagonalInches);

			return diagonalInches;
		}

	}
}