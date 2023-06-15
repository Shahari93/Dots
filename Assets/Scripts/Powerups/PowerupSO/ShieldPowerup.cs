using System;
using UnityEngine;
using Dots.Utilities.Destroy;

namespace Dots.GamePlay.Powerups.Shield
{
    /// <summary>
    /// This class is responsible for the shield powerup logic 
    /// </summary>
    [CreateAssetMenu(fileName = "ShieldPowerup")]
    public class ShieldPowerup : PowerupEffectSO
    {
        public static Action<bool> OnCollectedShieldPowerup;

        public override void Apply(GameObject target)
        {
            DestroyingPowerup.OnCollectedPower?.Invoke(powerupDuration);
            OnCollectedShieldPowerup?.Invoke(true);
        }
    } 
}