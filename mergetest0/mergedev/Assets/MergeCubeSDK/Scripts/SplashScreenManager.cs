using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SplashScreenManager : MonoBehaviour 
{
	public static SplashScreenManager instance;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			DestroyImmediate(this.gameObject);
	}

	public bool skipSplashScreen = false;
	public Callback OnSplashSequenceEnd;

	public GameObject gameLogo;

	[Range(0f,10f)]
	public float logoDuration = 3f;
	public Animator darkFader;

	public AudioClip logoSound;

	public void StartSplashSequence()
	{
		if (gameLogo != null)
		{
			gameLogo.SetActive(false);
		}

		darkFader.Play("FadeStayUp");

		if (skipSplashScreen)
		{
			EndSplashSequence();
		}
		else
		{
			StartCoroutine(BeginSplashSequencer());
		}
	}

	IEnumerator BeginSplashSequencer()
	{
		darkFader.Play("FadeIn");

		if (gameLogo != null)
		{
			gameLogo.SetActive(true);

			//Fade from black to user defined logo
			darkFader.Play("FadeOut");
			yield return new WaitForSeconds(0.5f);

			//Get end user's audio selection if not null
			if (logoSound != null)
			{
				GetComponent<AudioSource>().PlayOneShot(logoSound);
			}

			yield return new WaitForSeconds(logoDuration);
			darkFader.Play("FadeIn");
			yield return new WaitForSeconds(1.5f);
			gameLogo.SetActive(false);
		}

		EndSplashSequence();
	}

	void EndSplashSequence()
	{
		if (OnSplashSequenceEnd != null)
		{
			OnSplashSequenceEnd.Invoke();
		}

		if(gameLogo != null)
			gameLogo.SetActive(false);

		darkFader.Play("FadeOut");
	}

}
