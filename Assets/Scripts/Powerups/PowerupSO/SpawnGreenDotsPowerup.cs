using UnityEngine;
using Dots.Utils.Destroy;
using Dots.GamePlay.Dot.Good;

namespace Dots.GamePlay.Powerups.SpawnGreens
{
    [CreateAssetMenu(fileName = "SpawnGreens")]
    public class SpawnGreenDotsPowerup : PowerupEffectSO
    {
        public override void Apply(GameObject target)
        {
            GoodDot.spawnChance = 1f;
            DestroyingPowerup.OnCollectedPower?.Invoke(powerupDuration);
            InvokePowerupUI?.Invoke(powerupDuration);
        }
    }
}