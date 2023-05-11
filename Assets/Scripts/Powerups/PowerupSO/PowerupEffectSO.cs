using System;
using UnityEngine;

namespace Dots.GamePlay.Powerups
{
    public abstract class PowerupEffectSO : ScriptableObject
    {
        public static Action<float> InvokePowerupUI;
        public abstract void Apply(GameObject target);
    }
}