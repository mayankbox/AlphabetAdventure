using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    [SerializeField] private bool TestMode;

    [Header("TEST IDS")]
    [SerializeField] private string ios_banner_test;
    [SerializeField] private string ios_interstitial_test;
    [SerializeField] private string ios_reward_test;
    [SerializeField] private string android_banner_test;
    [SerializeField] private string android_interstitial_test;
    [SerializeField] private string android_reward_test;

    [Header("LIVE IDS")]
    [SerializeField] private string ios_banner;
    [SerializeField] private string ios_interstitial;
    [SerializeField] private string ios_reward;
    [SerializeField] private string android_banner;
    [SerializeField] private string android_interstitial;
    [SerializeField] private string android_reward;

    public static AdManager Instance;

    public GameObject Loader;

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    //private RewardedInterstitialAd rewardedInterstitialAd;

    private bool interstitial_is_loading;

    private Action interstitial_callback;

    private bool reward_is_loading;

    private Action rewardEvent;

    private bool getReward;
    bool isSplash;
    #region UNITY MONOBEHAVIOR METHODS

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AdManager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        print("==================================================================================");

        android_banner_test = "ca-app-pub-3940256099942544/6300978111";
        android_interstitial_test = "ca-app-pub-3940256099942544/1033173712";
        android_reward_test = "ca-app-pub-3940256099942544/5224354917";

        //   android_banner = "ca-app-pub-5212623602043994/2314545258";
        //  android_interstitial = "ca-app-pub-5212623602043994/3627626925";
        //  android_reward = "";

        MobileAds.SetiOSAppPauseOnBackground(true);

        List<string> deviceIds = new List<string>() { AdRequest.TestDeviceSimulator };

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
        deviceIds.Add("e43d8dbf-1846-4349-88d5-b714264725a2");
#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
            .SetTestDeviceIds(deviceIds).build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            //statusText.text = "Initialization complete";
            RequestBannerAd();

            RequestAndLoadInterstitialAd();

            RequestAndLoadRewardedAd();
        });
    }

    private void Update()
    {

   
        //if (showFpsMeter)
        //{
        //    fpsMeter.gameObject.SetActive(true);
        //    deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //    float fps = 1.0f / deltaTime;
        //    fpsMeter.text = string.Format("{0:0.} fps", fps);
        //}
        //else
        //{
        //    fpsMeter.gameObject.SetActive(false);
        //}
    }

 

    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();
    }

    #endregion

    #region BANNER ADS

    public void RequestBannerAd()
    {
        //statusText.text = "Requesting Banner Ad.";
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = TestMode ? android_banner_test : android_banner;
#elif UNITY_IPHONE
        string adUnitId = TestMode ? ios_banner_test : ios_banner;
#else
        string adUnitId = "unexpected_platform";
#endif
        // Clean up banner before reusing
        if (bannerView != null)
        {
            bannerView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Add Event Handlers
        //bannerView.OnAdLoaded += (sender, args) => {

        //};

        //bannerView.OnAdFailedToLoad += (sender, args) => {

        //};
        //bannerView.OnAdOpening += (sender, args) =>
        //{

        //};
        //bannerView.OnAdClosed += (sender, args) => { };

        // Load a banner ad
        bannerView.LoadAd(CreateAdRequest());

        HideBanner();
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    public void ShowBanner()
    {
        if (bannerView != null)
        {
            bannerView.Show();
        }
        else
        {
            RequestBannerAd();
        }
    }

    public void HideBanner()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }

    #endregion

    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd()
    {
        interstitial_is_loading = true;

        //statusText.text = "Requesting Interstitial Ad.";
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = TestMode ? android_interstitial_test : android_interstitial;
#elif UNITY_IPHONE
        string adUnitId = TestMode ? ios_interstitial_test : ios_interstitial;
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial before using it
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        interstitialAd = new InterstitialAd(adUnitId);

        // Add Event Handlers
        //interstitialAd.OnAdLoaded += (sender, args) => { };
        interstitialAd.OnAdFailedToLoad += (sender, args) =>
        {
            interstitial_is_loading = false;
        };
        //interstitialAd.OnAdOpening += (sender, args) => { };
        interstitialAd.OnAdClosed += (sender, args) =>
        {
            Loader.SetActive(false);
            interstitial_callback?.Invoke();

            interstitial_callback = null;
            //Advertisement.Banner.Show("Banner_Android");
           // ShowBanner();
            RequestAndLoadInterstitialAd();
        };

        // Load an interstitial ad
        interstitialAd.LoadAd(CreateAdRequest());
    }

    public void ShowInterstitialAd(Action callback)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (interstitialAd.IsLoaded())
            {
                Loader.SetActive(true);
                interstitial_callback = callback;

                interstitialAd.Show();
              
               HideBanner();

            }
            else
            {
                RequestAndLoadInterstitialAd();
                HideBanner();
               

            }
        }
        else
        {
            callback?.Invoke();

            callback = null;

            RequestAndLoadInterstitialAd();
        }
    }

    public void INTERSTITIALSHOWMATHOD()
    {
        ShowInterstitialAd(() =>
        {

        });
    }
    public void DestroyInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }
    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {
        reward_is_loading = true;

        //statusText.text = "Requesting Rewarded Ad.";
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = TestMode ? android_reward_test : android_reward;
#elif UNITY_IPHONE
        string adUnitId = TestMode ? ios_reward_test : ios_reward;
#else
        string adUnitId = "unexpected_platform";
#endif

        // create new rewarded ad instance
        rewardedAd = new RewardedAd(adUnitId);

        // Add Event Handlers
        //rewardedAd.OnAdLoaded += (sender, args) => { };
        rewardedAd.OnAdFailedToLoad += (sender, args) =>
        {

            reward_is_loading = false;
        };
        //rewardedAd.OnAdOpening += (sender, args) => { };
        //rewardedAd.OnAdFailedToShow += (sender, args) => { };
        rewardedAd.OnAdClosed += (sender, args) =>
        {
            if (getReward)
            {
                getReward = false;

                rewardEvent?.Invoke();
            }
            RequestAndLoadRewardedAd();
        };
        rewardedAd.OnUserEarnedReward += (sender, args) =>
        {
            getReward = true;
        };

        // Create empty ad request
        rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowRewardedAd(Action getrewardEvent)
    {
        if (reward_is_loading==true)
        {
            if (rewardedAd != null)
            {
                rewardEvent = getrewardEvent;

                rewardedAd.Show();
            }
            else
            {
                if (!reward_is_loading)
                {

                    RequestAndLoadRewardedAd();
                }
            }

            GameObject BTN = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            BTN.GetComponent<Button>().interactable = false;
        }

    }

  public void SHOWREWAERDADS()
    {
      ShowRewardedAd(() =>
        {

        });
    }



    #endregion
}