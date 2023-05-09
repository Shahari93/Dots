using UnityEngine;
using Dots.Utils.Destroy;
using Dots.GamePlay.Dot.Good;

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
            InvokePowerupUI?.Invoke(powerupDuration);
        }
    }
}