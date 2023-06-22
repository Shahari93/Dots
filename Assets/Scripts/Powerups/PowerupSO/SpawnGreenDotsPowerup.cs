using UnityEngine;
using Dots.Utilities.Destroy;
using Dots.GamePlay.Dot.Good;
using Dots.Utilities.GooglePlayServices;

namespace Dots.GamePlay.Powerups.SpawnGreens
{
    /// <summary>
    /// This class is responsible for the spawn green dots powerup logic 
    /// </summary>
    [CreateAssetMenu(fileName = "SpawnGreens")]
    public class SpawnGreenDotsPowerup : PowerupEffectSO
    {
        int timesCollected;

        void Awake()
        {
            if (PlayerPrefs.HasKey("TimesSpawnCollected"))
            {
                timesCollected = PlayerPrefs.GetInt("TimesSpawnCollected");
            }
            else
            {
                timesCollected = 0;
            }
        }

        public override void Apply(GameObject target)
        {
            GoodDot.spawnChance = 1f;
            DestroyingPowerup.OnCollectedPower?.Invoke(powerupDuration);
            InvokePowerupUI?.Invoke(powerupDuration);

            timesCollected++;
            if (timesCollected == 1)
            {
                if (GoogleServices.Instance.connectedToGooglePlay)
                {
                    Social.ReportProgress("CgkIm-Xn1MEZEAIQDw", 100.0f, null);
                }
            }
            PlayerPrefs.SetInt("TimesSpawnCollected", timesCollected);
        }
    }
}