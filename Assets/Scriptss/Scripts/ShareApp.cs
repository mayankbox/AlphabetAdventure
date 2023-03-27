using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ShareApp : MonoBehaviour
{
	LinkClass lc = new LinkClass();

	string shareTxt;

	void Start()
    {
		shareTxt = lc.appUrl;
	}

    void Update()
    {
        
    }

	private bool isFocus = false;
	private bool isProcessing = false;

	void OnApplicationFocus(bool focus)
	{
		isFocus = focus;
	}

	public void ShareText()
	{
		if (!isProcessing)
		{
			StartCoroutine(ShareScreenshot());
		}
	}

	IEnumerator ShareScreenshot()
	{
		isProcessing = true;

		yield return new WaitForSecondsRealtime(0.3f);

		if (!Application.isEditor)
		{
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

			intentObject.Call<AndroidJavaObject>("setType", "text/plain");

			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareTxt);

			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
				intentObject, shareTxt);
			currentActivity.Call("startActivity", chooser);

			yield return new WaitForSecondsRealtime(1);
		}

		yield return new WaitUntil(() => isFocus);

		isProcessing = false;
	}
	public void RateUS()
    {
		lc.RateUs();
    }
	public void MoreGame()
    {
		lc.MoreGames();
    }
	public void PrivacyPolicy()
    {
		lc.PrivacyPolicy();
    }
}
