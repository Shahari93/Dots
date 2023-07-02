using System;
using UnityEngine;
using Dots.Utilities.Destroy;
using Dots.Utilities.GooglePlayServices;

namespace Dots.GamePlay.Powerups.SlowSpeed
{
    /// <summary>
    /// This class is responsible for the Slow speed powerup logic (Not in the game right now)
    /// </summary>
    [CreateAssetMenu(fileName = "SlowSpeedPowerup")]
    public class SlowSpeedPowerup : PowerupEffectSO
    {
        int timesCollected;

        public static Action<float> OnCollectedSlowSpeedPowerup;

        private static bool isSlowPowerup;
        public static bool IsSlowPowerup { get => isSlowPowerup; set => isSlowPowerup = value; }

        void Awake()
        {
            isSlowPowerup = false;
            if (PlayerPrefs.HasKey("TimesSlowTimeCollected"))
            {
                timesCollected = PlayerPrefs.GetInt("TimesSlowTimeCollected");
            }
            else
            {
                timesCollected = 0;
            }
        }

        public override void Apply(GameObject target)
        {
            InvokePowerupUI?.Invoke(powerupDuration);
            OnCollectedSlowSpeedPowerup?.Invoke(powerupDuration);
            DestroyingPowerup.OnCollectedPower?.Invoke(powerupDuration);

            isSlowPowerup = true;
            timesCollected++;
            if (timesCollected == 1)
            {
                if (GoogleServices.Instance.connectedToGooglePlay)
                {
                    Social.ReportProgress("CgkIm-Xn1MEZEAIQEA", 100.0f, null);
                }
            }
            PlayerPrefs.SetInt("TimesSlowTimeCollected", timesCollected);
        }
    }
}