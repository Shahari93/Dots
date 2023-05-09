using UnityEngine;

namespace Dots.GamePlay.Powerups.SlowSpeed
{
    [CreateAssetMenu(fileName = "SlowSpeedPowerup")]
    public class SlowSpeedPowerup : PowerupEffectSO
    {
        public override void Apply(GameObject target)
        {
            Debug.Log("Hit");
        }
    } 
}