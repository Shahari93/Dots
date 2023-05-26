using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Dots.Ads.Init
{
    public class IronSourceInit : MonoBehaviour
    {
#if UNITY_ANDROID
        readonly string appKey = "19f28384d";
#elif Unity_iOS
string appKey = "19f99b595";
#endif

        bool isPaused = false;

        public static IronSourceInit Instance;
        const string COINS_PLACEMENT = "Extra_Coins";
        const string SHIELD_PLACEMENT = "Level_Start";

        public static event Action OnCoinsRvWatched;
        public static event Func<bool> OnCheckIfUpgradeable;

        public static event Func<bool> OnShieldRvWatched;
        public static bool IsShieldFromRV;


        List<Button> rewardedAdsButton = new List<Button>();
        [SerializeField] Button coinsRVButton, shieldRVButton;


        void OnEnable()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

            //Interstitial 
            IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
            IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
            IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
            IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
            IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
            IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
            IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

            //Add AdInfo Rewarded Video Events
            IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;


            //BadDot.OnLoseGame += ShowInterstitialAd;
        }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            coinsRVButton.onClick.AddListener(delegate { ShowRewardedAd(COINS_PLACEMENT); });
            shieldRVButton.onClick.AddListener(delegate { ShowRewardedAd(SHIELD_PLACEMENT); });

            // For testing
            rewardedAdsButton.Add(coinsRVButton);
            rewardedAdsButton.Add(shieldRVButton);
        }

        private void SdkInitializationCompletedEvent()
        {
            IronSource.Agent.validateIntegration();
            IronSource.Agent.loadInterstitial();
            IronSource.Agent.loadRewardedVideo();
        }

        void Start()
        {
            InitAgents();
        }

        #region Init Ads
        private void InitAgents()
        {
            //For Rewarded Video
            IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
            //For Interstitial
            IronSource.Agent.init(appKey, IronSourceAdUnits.INTERSTITIAL);
        }
        #endregion

        #region Interstitial Ads
        private void ShowInterstitialAd()
        {
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
            }
            else
            {
                Debug.Log("Ad is not ready");
                return;
            }
        }
        #endregion

        #region Interstitial Callbacks
        // Interstitial Callbacks
        // Invoked when the interstitial ad was loaded succesfully.
        void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
        {
            Debug.Log("Ad is ready");
        }
        // Invoked when the initialization process has failed.
        void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
        {
        }
        // Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
        void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
        }
        // Invoked when end user clicked on the interstitial ad
        void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
        {
        }
        // Invoked when the ad failed to show.
        void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
        {
        }
        // Invoked when the interstitial ad closed and the user went back to the application screen.
        void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
            IronSource.Agent.loadInterstitial();
        }
        // Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
        // This callback is not supported by all networks, and we recommend using it only if  
        // it's supported by all networks you included in your build. 
        void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
        {
        }
        #endregion

        #region Rewarded Ads
        public void ShowRewardedAd(string placement)
        {
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo(placement);
                _ = IronSource.Agent.getPlacementInfo(placement);
                IronSource.Agent.loadRewardedVideo();
            }
            else
            {
                Debug.Log("Ad is not ready");
                return;
            }

        }
        #endregion

        #region Rewarded Ads Callbacks
        // Rewarded Ads Callbacks
        /************* RewardedVideo AdInfo Delegates *************/
        // Indicates that there’s an available ad.
        // The adInfo object includes information about the ad that was loaded successfully
        // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
        void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
        {
        }
        // Indicates that no ads are available to be displayed
        // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
        void RewardedVideoOnAdUnavailable()
        {
        }
        // The Rewarded Video ad view has opened. Your activity will loose focus.
        void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            OnApplicationFocus(false);
        }
        // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
        void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
            IronSource.Agent.loadRewardedVideo();
        }
        // The user completed to watch the video, and should be rewarded.
        // The placement parameter will include the reward data.
        // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
        void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
            //Placement can return null if the placementName is not valid.
            if (placement != null)
            {
                string getPlacementName = placement.getPlacementName();
                _ = placement.getRewardName();
                int getRewardAmount = placement.getRewardAmount();

                // TODO: FInd a way to check the placement according to the pressed RV button
                if (getPlacementName == COINS_PLACEMENT)
                {
                    Debug.Log("Shahar coins test");
                    OnCoinsRvWatched?.Invoke();
                    OnCheckIfUpgradeable?.Invoke();
                }
                else if (getPlacementName == SHIELD_PLACEMENT)
                {
                    Debug.Log("Shahar shield");
                    OnShieldRvWatched?.Invoke();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                Debug.Log("Shahar true non");
            }
            if (IronSource.Agent.isRewardedVideoPlacementCapped(placement.getPlacementName()))
            {
                for (int i = 0; i < rewardedAdsButton.Count; i++)
                {
                    if (placement.getRewardName() == rewardedAdsButton[i].name)
                    {
                        rewardedAdsButton[i].gameObject.SetActive(false);
                    }
                }
            }
            OnApplicationFocus(true);
            IronSource.Agent.loadRewardedVideo();
        }
        // The rewarded video ad was failed to show.
        void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
        {
        }
        // Invoked when the video ad was clicked.
        // This callback is not supported by all networks, and we recommend using it only if
        // it’s supported by all networks you included in your build.
        void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
            IronSource.Agent.loadRewardedVideo();
        }
        #endregion

        void OnApplicationFocus(bool hasFocus)
        {
            isPaused = !hasFocus;
            IronSource.Agent.onApplicationPause(isPaused);
        }
        void OnDisable()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent -= SdkInitializationCompletedEvent;

            //Interstitial 
            IronSourceInterstitialEvents.onAdReadyEvent -= InterstitialOnAdReadyEvent;
            IronSourceInterstitialEvents.onAdLoadFailedEvent -= InterstitialOnAdLoadFailed;
            IronSourceInterstitialEvents.onAdOpenedEvent -= InterstitialOnAdOpenedEvent;
            IronSourceInterstitialEvents.onAdClickedEvent -= InterstitialOnAdClickedEvent;
            IronSourceInterstitialEvents.onAdShowSucceededEvent -= InterstitialOnAdShowSucceededEvent;
            IronSourceInterstitialEvents.onAdShowFailedEvent -= InterstitialOnAdShowFailedEvent;
            IronSourceInterstitialEvents.onAdClosedEvent -= InterstitialOnAdClosedEvent;

            // RV
            IronSourceRewardedVideoEvents.onAdOpenedEvent -= RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent -= RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent -= RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent -= RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent -= RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent -= RewardedVideoOnAdClickedEvent;

            //BadDot.OnLoseGame -= ShowInterstitialAd;
        }
    }
}