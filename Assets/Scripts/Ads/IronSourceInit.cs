using System;
using UnityEngine;

namespace Dots.Ads.Init
{
    public class IronSourceInit : MonoBehaviour
    {
#if UNITY_ANDROID
        string appKey = "19f28384d";
#elif Unity_iOS
string appKey = "19f99b595";
#endif

        void OnEnable()
        {
            IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
        }

        private void SdkInitializationCompletedEvent()
        {
            IronSource.Agent.validateIntegration();
        }

        void Start()
        {
            //For Rewarded Video
            IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
            //For Interstitial
            IronSource.Agent.init(appKey, IronSourceAdUnits.INTERSTITIAL);
        }

        void OnApplicationPause(bool isPaused)
        {
            IronSource.Agent.onApplicationPause(isPaused);
        }

    }
}