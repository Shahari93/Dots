using System;
using UnityEngine;

namespace Dots.GamePlay.Powerups
{
    [Serializable]
    public class PowerupData
    {
        public float powerupDurationData;
    }

    /// <summary>
    /// Base scriptable object class for each powerup we'll create
    /// It has the basic fields that each powerup need
    /// And the apply method that is being applied to the player when collided
    /// </summary>
    public abstract class PowerupEffectSO : ScriptableObject
    {
        public float powerupDuration;
        public float powerupDurationLimit;
        public float spawnChance;

        public int upgradeCoinsCost = 10;

        public static Action<float> InvokePowerupUI;
        public abstract void Apply(GameObject target);
    }
}