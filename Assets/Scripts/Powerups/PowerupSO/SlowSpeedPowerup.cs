using UnityEngine;

namespace Dots.GamePlay.Powerups.SlowSpeed
{
    [CreateAssetMenu(fileName = "SlowSpeedPowerup")]
    public class SlowSpeedPowerup : PowerupEffectSO
    {
        [Range(5, 10)]
        public float powerupDuration;

        public override void Apply(GameObject target)
        {
            InvokePowerupUI?.Invoke(powerupDuration);
        }
    } 
}