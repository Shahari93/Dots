using UnityEngine;

public abstract class PowerupEffectSO : ScriptableObject
{
    public abstract void Apply(GameObject target);
}