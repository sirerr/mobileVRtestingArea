using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blitter : MonoBehaviour 
{
//	[HideInInspector]
	public RenderTexture mainCameraTexture { get; private set; }

	Camera cam;
	void Start()
	{
		cam = GetComponent<Camera>();

		mainCameraTexture = Merge.MergeCubeSDK.instance.GetHeadsetTexture();
		if (mainCameraTexture == null)
		{
			Invoke("RegrabMainTexture", .2f);
		}
	}

	void RegrabMainTexture()
	{
		mainCameraTexture = Merge.MergeCubeSDK.instance.GetHeadsetTexture();
		if (mainCameraTexture == null)
		{
			Invoke("RegrabMainTexture", .2f);
		}
	}

	void OnPreRender()
	{
		if (Merge.MergeCubeSDK.instance.viewMode == Merge.MergeCubeSDK.ViewMode.FULLSCREEN)
		{
			cam.targetTexture = mainCameraTexture;
		}
	}

	void OnPostRender()
	{
		if (Merge.MergeCubeSDK.instance.viewMode == Merge.MergeCubeSDK.ViewMode.FULLSCREEN)
		{
			cam.targetTexture = null;
			if (mainCameraTexture != null)
			{
				Graphics.Blit(mainCameraTexture, null as RenderTexture);
			}
		}
		else
		{
			cam.targetTexture = mainCameraTexture;
		}
	}
}
