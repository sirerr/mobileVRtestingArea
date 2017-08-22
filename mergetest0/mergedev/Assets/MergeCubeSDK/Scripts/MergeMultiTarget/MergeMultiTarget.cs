using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MergeMultiTarget : MonoBehaviour{
	static MergeMultiTarget s_ins;
	void Awake(){
		if (MergeMultiTarget.instance != null) {
			Destroy (gameObject);
			return;
		}
		s_ins = this;
		KillReferenceCube ();
	}
	public static MergeMultiTarget instance{ get { return s_ins; } }
	public enum HandleType{
		DoNothing,HideChild,DisableChild,DisableSelected
	}
	[Tooltip("HideChild: Hide all children without disable gameobjects;\nDisableChild: will disable all immediate children; ")]
	public HandleType trackingHandleType = HandleType.DisableChild;

	public delegate void TrackingEvent();
	public event TrackingEvent OnTrackingFound;
	public event TrackingEvent OnTrackingLost;

	public GameObject[] selectToDisable;

	public bool isTracking { get; private set; }

	List<MergeTrackableEventHandler> mergeTrackables = new List<MergeTrackableEventHandler>();
	List<MergeTrackableEventHandler> trackers = new List<MergeTrackableEventHandler>();

	public void AddMergeTrackable(MergeTrackableEventHandler trackable){
		mergeTrackables.Add (trackable);
	}
	public void LockToTrackable(MergeTrackableEventHandler trackable){
		foreach (MergeTrackableEventHandler tp in mergeTrackables) { 
			if (tp != trackable) {
				tp.Die ();
			}
		}
	}
	public void OnMergeTrackingFound(MergeTrackableEventHandler tracker){
		int countTp = trackers.Count;
		if (!trackers.Contains (tracker)) {
			trackers.Add (tracker);
		}
		if (transform.parent != tracker.transform) {
			transform.parent = tracker.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
		if (trackers.Count > 0 && countTp == 0) {
			MergeTrackingFoundHandler ();
		}
	}
	public void OnMergeTrackingLost(MergeTrackableEventHandler tracker){
		if (trackers.Contains (tracker)) {
			trackers.Remove (tracker);
		}
		if (trackers.Count == 0) {
			MergeTrackingLostHandler ();
		}
	}
	void MergeTrackingFoundHandler(){
		isTracking = true;
		if (OnTrackingFound != null)
		{
			OnTrackingFound();
		}
		VuforiaTrackingEventHandle (true);
	}
	void MergeTrackingLostHandler(){
		isTracking = false;
		if (OnTrackingLost != null)
		{
			OnTrackingLost();
		}
		VuforiaTrackingEventHandle (false);
	}

	void VuforiaTrackingEventHandle(bool isTracking){
		if (trackingHandleType == HandleType.HideChild) {
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer> (true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider> (true);

			// Enable rendering:
			foreach (Renderer component in rendererComponents) {
				component.enabled = isTracking;
			}

			// Enable colliders:
			foreach (Collider component in colliderComponents) {
				component.enabled = isTracking;
			}
		}
		else if (trackingHandleType == HandleType.DisableSelected) {
			for (int i = 0; i < selectToDisable.Length; i++) {
				selectToDisable[i].SetActive (isTracking);
			}
		}
		else if (trackingHandleType == HandleType.DisableChild) {
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild (i).gameObject.SetActive (isTracking);
			}
		}
	}
	void KillReferenceCube(){
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild (i).name == @"ReferenceCube(SelfDestroyOnRun)") {
				Destroy (transform.GetChild (i).gameObject);
			}
		}
	}

	#if UNITY_ANDROID
	void Start()
	{
		SetCameraFocus ();
	}
	void SetCameraFocus()
	{
		if (!isTracking)
		{
			CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);		
		}
		Invoke("SetCameraFocus", 2f);
	}
	#endif
}
