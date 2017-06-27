using UnityEngine;
using Vuforia;

public class IntroTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
	
	private TrackableBehaviour mTrackableBehaviour;

	public delegate void TrackingEvent();
	public event TrackingEvent OnTrackingFound;
	public event TrackingEvent OnTrackingLost;

	public bool isTracking { get; private set; }

	void Start()
	{
		isTracking = false;

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();

		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		OnTrackingFound += HandleTrackingFound;
		OnTrackingLost += HandleTrackingLost;
	}

	public void OnTrackableStateChanged( TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus )
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			isTracking = true;

			if (OnTrackingFound != null)
			{
				OnTrackingFound();
			}
		}
		else
		{
			isTracking = false;

			if (OnTrackingLost != null)
			{
				OnTrackingLost();
			}
		}
	}


	void HandleTrackingFound()
	{
		transform.SendMessage ("OnTrackingFound", SendMessageOptions.DontRequireReceiver);
	}

	void HandleTrackingLost()
	{
		transform.SendMessage ("OnTrackingLost", SendMessageOptions.DontRequireReceiver);
	}

	public void RemoveTrackingLogic()
	{
		mTrackableBehaviour.UnregisterTrackableEventHandler(this);
	}

}


