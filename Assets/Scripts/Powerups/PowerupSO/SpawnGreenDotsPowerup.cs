using UnityEngine;
using Dots.GamePlay.Dot.Good;

[CreateAssetMenu(fileName = "SpawnGreens")]
public class SpawnGreenDotsPowerup : PowerupEffectSO
{
    [Range(5,10)]
    public float powerupDuration;

    public override void Apply(GameObject target)
    {
        Debug.Log("Hit");
        GoodDot.spawnChance = 1f;
        DestroingPowerup.OnCollectedPower?.Invoke(powerupDuration);
    }
}