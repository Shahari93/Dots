using System;
using UnityEngine;
using Dots.Utils.Destroy;

namespace Dots.GamePlay.Powerups.Shield
{
    [CreateAssetMenu(fileName = "ShieldPowerup")]

    public class ShieldPowerup : PowerupEffectSO
    {
        public static Action<bool> OnCollectedShieldPowerup;

        public override void Apply(GameObject target)
        {
            DestroingPowerup.OnCollectedPower?.Invoke(powerupDuration);
            OnCollectedShieldPowerup?.Invoke(true);
        }
    } 
}