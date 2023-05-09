using UnityEngine;

namespace Dots.GamePlay.Powerups
{
	public abstract class PowerupEffectSO : ScriptableObject
	{
		public abstract void Apply(GameObject target);
	} 
}