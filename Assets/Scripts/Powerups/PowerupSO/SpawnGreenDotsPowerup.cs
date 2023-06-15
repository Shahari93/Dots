using UnityEngine;
using Dots.Utilities.Destroy;
using Dots.GamePlay.Dot.Good;

namespace Dots.GamePlay.Powerups.SpawnGreens
{
    /// <summary>
    /// This class is responsible for the spawn green dots powerup logic 
    /// </summary>
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