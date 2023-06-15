using UnityEngine;

namespace Dots.GamePlay.Powerups.SlowSpeed
{
    /// <summary>
    /// This class is responsible for the Slow speed powerup logic (Not in the game right now)
    /// </summary>
    [CreateAssetMenu(fileName = "SlowSpeedPowerup")]
    public class SlowSpeedPowerup : PowerupEffectSO
    {
        public override void Apply(GameObject target)
        {
            InvokePowerupUI?.Invoke(powerupDuration);
        }
    } 
}