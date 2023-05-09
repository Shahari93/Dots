using UnityEngine;

namespace Dots.GamePlay.Powerups.Shield
{
    [CreateAssetMenu(fileName = "ShieldPowerup")]

    public class ShieldPowerup : PowerupEffectSO
    {
        public override void Apply(GameObject target)
        {
            Debug.Log("Hit");
        }
    } 
}