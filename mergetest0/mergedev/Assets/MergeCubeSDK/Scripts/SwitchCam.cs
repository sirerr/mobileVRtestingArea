using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour {
	static SwitchCam s_ins;
	public static SwitchCam ins{
		get { return s_ins;}
	}
	void Awake(){
		if (SwitchCam.ins != null) {
			DestroyImmediate (this.gameObject);
			return;
		}
		s_ins = this;
		DontDestroyOnLoad (gameObject);
	}
	void OnApplicationQuit(){
		Vuforia.VuforiaConfiguration.Instance.Vuforia.CameraDirection = Vuforia.CameraDevice.CameraDirection.CAMERA_BACK;
	}

	public void SwitchToFront(){
		Debug.LogWarning ("Switch To Front");
		StartCoroutine (FAction());
	}
	public void SwitchToBack(){
		Debug.LogWarning ("Switch To Back");
		StartCoroutine (BAction ());
	}

	IEnumerator FAction(){
		Vuforia.CameraDevice.Instance.Stop();
		yield return null;
		Vuforia.CameraDevice.Instance.Deinit();
		yield return null;
		Vuforia.VuforiaConfiguration.Instance.Vuforia.CameraDirection = Vuforia.CameraDevice.CameraDirection.CAMERA_FRONT;
		yield return null;
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
	IEnumerator BAction(){
		Vuforia.CameraDevice.Instance.Stop();
		yield return null;
		Vuforia.CameraDevice.Instance.Deinit();
		yield return null;
		Vuforia.VuforiaConfiguration.Instance.Vuforia.CameraDirection = Vuforia.CameraDevice.CameraDirection.CAMERA_BACK;
		yield return null;
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);

	}
}
