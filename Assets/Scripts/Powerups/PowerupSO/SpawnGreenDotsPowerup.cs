using UnityEngine;
using Dots.GamePlay.Dot.Good;
using Dots.Utils.Destroy;

namespace Dots.GamePlay.Powerups.SpawnGreens
{
    [CreateAssetMenu(fileName = "SpawnGreens")]
    public class SpawnGreenDotsPowerup : PowerupEffectSO
    {
        [Range(5, 10)]
        public float powerupDuration;

        public override void Apply(GameObject target)
        {
            Debug.Log("Hit");
            GoodDot.spawnChance = 1f;
            DestroingPowerup.OnCollectedPower?.Invoke(powerupDuration);
        }
    } 
}