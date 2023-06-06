using System;
using UnityEngine;

namespace Dots.GamePlay.Powerups
{
    [Serializable]
    public class PowerupData
    {
        public float powerupDurationData;
    }
    public abstract class PowerupEffectSO : ScriptableObject
    {
        public float powerupDuration;
        public float powerupDurationLimit;

        public static Action<float> InvokePowerupUI;
        public abstract void Apply(GameObject target);
    }
}