using Dots.Audio.Manager;
using Dots.Utils.Destroy;
using System;
using UnityEngine;

namespace Dots.GamePlay.Powerups.Shield
{
    [CreateAssetMenu(fileName = "ShieldPowerup")]

    public class ShieldPowerup : PowerupEffectSO
    {
        public static Action<bool> OnCollectedShieldPowerup;

        public override void Apply(GameObject target)
        {
            InvokePowerupUI?.Invoke(powerupDuration);
            DestroingPowerup.OnCollectedPower?.Invoke(powerupDuration); // TODO: Not final need to refactor
            OnCollectedShieldPowerup?.Invoke(true);
        }
    } 
}