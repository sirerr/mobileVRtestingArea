using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Vuforia;

namespace Merge
{		
	public class MergeCubeSDK : MonoBehaviour 
	{
		public static MergeCubeSDK instance;

		void Awake()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				DestroyImmediate(this.gameObject);
		}
	
		public enum ViewMode { HEADSET, FULLSCREEN };

		private static ViewMode currentViewMode = ViewMode.FULLSCREEN;
		public ViewMode viewMode = ViewMode.FULLSCREEN;

		private Transform arCameraRef;

		private VideoBackgroundBehaviour leftVidBackBehaviour;
		private VideoBackgroundBehaviour rightVidBackBehaviour;

		public GameObject headsetViewSetup;
		RenderTexture headsetViewRenderTexture;

		private bool isActive = false;

		public UnityEngine.UI.Image viewSwitchButton;
		public UnityEngine.UI.Image viewSwitchGraphic;
		public Sprite fullscreenSprite;
		public Sprite headsetViewSprite;
		public Sprite disabledSprite;

		public Animator mainPanelAnimator;
		bool menuIsOpen = false;

		public delegate void ViewModeSwapEvent(bool swappedToHeadsetView);
		public ViewModeSwapEvent OnViewModeSwap;

		void Start()
		{
			arCameraRef = Camera.main.transform;

			viewMode = currentViewMode;

			if (currentViewMode == ViewMode.HEADSET)
			{
				if (headsetViewRenderTexture == null) {
					CreateRenderTexture ();
				}
				Camera.main.targetTexture = headsetViewRenderTexture;
				headsetViewSetup.SetActive(true);
			}
			else
			{
				if(Camera.main != null)
					Camera.main.targetTexture = null;

				headsetViewSetup.SetActive(false);
			}
		}


		void CreateRenderTexture()
		{
			headsetViewRenderTexture = new RenderTexture (1488, 750, 24, RenderTextureFormat.ARGB32);
			headsetViewRenderTexture.Create ();

			Renderer[] childs = GetComponentsInChildren<Renderer> (true);
			foreach (Renderer tp in childs) {
				if (tp.name == "L" || tp.name == "R") {
					tp.sharedMaterial.SetTexture ("_Texture", headsetViewRenderTexture);
				}
			}
		}


		void OnValidate()
		{
//			currentViewMode = viewMode;

			if (currentViewMode == ViewMode.HEADSET)
			{
				if (headsetViewRenderTexture == null) {
					CreateRenderTexture ();
				}
				Camera.main.targetTexture = headsetViewRenderTexture;
				headsetViewSetup.SetActive(true);
			}
			else
			{
				if(Camera.main != null)
					Camera.main.targetTexture = null;

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
				SetToHeadsetView ();
				viewSwitchGraphic.sprite = headsetViewSprite;
			} 
			else 
			{
				SetToFullscreenView ();
				viewSwitchGraphic.sprite = fullscreenSprite;
			}

			viewSwitchButton.gameObject.SetActive (false);

			if (OnViewModeSwap != null)
			{
				OnViewModeSwap.Invoke((viewMode == ViewMode.HEADSET));
			}

			Invoke ("EnableViewChangeBtn", 0.5f);
		}

		void EnableViewChangeBtn()
		{
			viewSwitchButton.gameObject.SetActive (true);
		}



		void SetToFullscreenView()
		{
			Camera.main.targetTexture = null;
			headsetViewSetup.SetActive(false);
		}

		void SetToHeadsetView()
		{
			if (headsetViewRenderTexture == null) {
				CreateRenderTexture ();
			}
			Camera.main.targetTexture = headsetViewRenderTexture;

			headsetViewSetup.SetActive(true);
		}
			
		bool isFlashOn = false;

		public void SwitchFlashLight()
		{
			isFlashOn = !isFlashOn;

			if (isFlashOn) 
			{
				TurnFlashOn ();
			} 
			else 
			{
				TurnFlashOff ();
			}
		}

		void TurnFlashOff()
		{
			Vuforia.CameraDevice.Instance.SetFlashTorchMode (false);
		}

		void TurnFlashOn()
		{
			Vuforia.CameraDevice.Instance.SetFlashTorchMode (true);
		}

	}
}