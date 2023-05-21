using System;
using UnityEngine;

namespace Dots.GamePlay.Powerups
{
    public abstract class PowerupEffectSO : ScriptableObject
    {
        [Range(0, 10)]
        public float powerupDuration;

        public static Action<float> InvokePowerupUI;
        public abstract void Apply(GameObject target);
    }
}