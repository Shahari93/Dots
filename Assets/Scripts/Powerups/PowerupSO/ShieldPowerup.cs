using System;
using UnityEngine;
using Dots.Utilities.Destroy;
using Dots.Utilities.GooglePlayServices;

namespace Dots.GamePlay.Powerups.Shield
{
    /// <summary>
    /// This class is responsible for the shield powerup logic 
    /// </summary>
    [CreateAssetMenu(fileName = "ShieldPowerup")]
    public class ShieldPowerup : PowerupEffectSO
    {
        int timesCollected;

        public static Action<bool> OnCollectedShieldPowerup;

        void Awake()
        {
            if (PlayerPrefs.HasKey("TimesShieldCollected"))
            {
                timesCollected = PlayerPrefs.GetInt("TimesShieldCollected");
            }
            else
            {
                timesCollected = 0;
            }
        }

        public override void Apply(GameObject target)
        {
            DestroyingPowerup.OnCollectedPower?.Invoke(powerupDuration);
            OnCollectedShieldPowerup?.Invoke(true);
            timesCollected++;
            if (timesCollected == 1)
            {
                if (GoogleServices.Instance.connectedToGooglePlay)
                {
                    Social.ReportProgress("CgkIm-Xn1MEZEAIQDg", 100.0f, null);
                }
            }
            PlayerPrefs.SetInt("TimesShieldCollected", timesCollected);
        }
    } 
}