using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.Audio.Manager;
using Dots.PauseGame.Presenter;
using UnityEngine.SceneManagement;

namespace Dots.Ads.Init
{
    /// <summary>
    /// This class is how we communicate with IronSource ads
    /// We initialize the ads and call them when we want to display ads
    /// </summary>
    public class IronSourceInit : MonoBehaviour
    {
#if UNITY_ANDROID
        readonly string appKey = "19f28384d";
#elif Unity_iOS
string appKey = "19f99b595";
#endif

        bool isPaused = false;

        const string COINS_PLACEMENT = "Extra_Coins";
        const string DOUBLE_COINS_PLACEMENT = "Double_Coins";
        const string SHIELD_PLACEMENT = "Start_Shield";

        public static event Action<int> OnCoinsRvWatched;
        public static event Func<bool> OnCheckIfUpgradeable;

        public static event Func<bool> OnShieldRvWatched;
        public static bool IsShieldFromRV;

        [SerializeField] Button coinsRV;
        [SerializeField] Button shieldRV;
        [SerializeField] Button reviveRV;
        [SerializeField] Button doubleCoinsRV;

        /// <summary>
        /// Subscribing to all the IronSource ads events
        /// </summary>
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

            IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
            IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
            IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
            IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;

            //This is for the rv ad button on main menu scene
            if (coinsRV != null)
            {
                coinsRV.interactable = !IsRewardedVideoPlacementCapped();
            }

            // This is for the double coins rv ad button if the player collected any coins
            if (doubleCoinsRV != null && CoinsModel.CoinsToAdd != 0)
            {
                doubleCoinsRV.gameObject.SetActive(true);
            }

            LoseGameUIPresenter.OnRestartClicked += ShowInterstitialAd;
            LoseGameUIPresenter.OnReturnHomeClicked += ShowInterstitialAd;
        }

        void Start()
        {
            InitAdUnits();
            IronSource.Agent.validateIntegration();
            IronSource.Agent.shouldTrackNetworkState(true);
        }

        void SdkInitializationCompletedEvent()
        {
            IronSource.Agent.loadInterstitial();
            IronSource.Agent.loadRewardedVideo();
        }

        #region Init Ads
        void InitAdUnits()
        {
            //For Rewarded Video
            IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
            //For Interstitial
            IronSource.Agent.init(appKey, IronSourceAdUnits.INTERSTITIAL);
        }
        #endregion

        #region Interstitial Ads
        void ShowInterstitialAd()
        {
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
                IronSource.Agent.loadInterstitial();
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Interstitial Callbacks
        // Interstitial Callbacks
        // Invoked when the interstitial ad was loaded successfully.
        void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
        {
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
            AudioManager.Instance.PlaySFX("ButtonClick");
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo(placement);
                coinsRV.interactable = !IsRewardedVideoPlacementCapped();
            }
            else
            {
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
        }
        // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
        void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
            coinsRV.interactable = !IsRewardedVideoPlacementCapped();
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
                string getRewardName = placement.getRewardName();

                if (getPlacementName == SHIELD_PLACEMENT || getRewardName == "Shield")
                {
                    OnShieldRvWatched?.Invoke();
                    IsShieldFromRV = true;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }

                // TODO: FInd a way to check the placement according to the pressed RV button
                if (getPlacementName == COINS_PLACEMENT || getRewardName == "Coins")
                {
                    OnCoinsRvWatched?.Invoke(CoinsModel.CurrentCoinsAmount + placement.getRewardAmount());
                    OnCheckIfUpgradeable?.Invoke();
                    coinsRV.interactable = !IsRewardedVideoPlacementCapped();
                }

                if (getPlacementName == DOUBLE_COINS_PLACEMENT || getRewardName == "DoubleCoins")
                {
                    OnCoinsRvWatched?.Invoke(CoinsModel.CoinsToAdd);
                    doubleCoinsRV.gameObject.SetActive(false);
                    doubleCoinsRV.interactable = !IsRewardedVideoPlacementCapped();
                }
            }
            OnApplicationFocus(true);
        }

        bool IsRewardedVideoPlacementCapped()
        {
            return IronSource.Agent.isRewardedVideoPlacementCapped(COINS_PLACEMENT);
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
        }
        #endregion

        void OnApplicationFocus(bool hasFocus)
        {
            isPaused = !hasFocus;
            IronSource.Agent.onApplicationPause(isPaused);
        }
        void OnDestroy()
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

            LoseGameUIPresenter.OnRestartClicked -= ShowInterstitialAd;
            LoseGameUIPresenter.OnReturnHomeClicked -= ShowInterstitialAd;
        }
    }
}