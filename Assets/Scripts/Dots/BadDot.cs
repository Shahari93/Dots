using System;
using Dots.Ads.Init;
using Dots.Audio.Manager;
using Dots.GamePlay.Powerups.Shield;
using Dots.GamePlay.Player.Interaction.Shields;

namespace Dots.GamePlay.Dot.Bad
{
    /// <summary>
    /// This class if for the behavior of the bad dot (red one) 
    /// when it collides with the player
    /// </summary>
    public class BadDot : DestroyingDots
    {
        // What is the chance of the red dot being spawned
        public static float spawnChance = 0.85f;

        public override float SpawnChance
        {
            get
            {
                return spawnChance;
            }
            set
            {
                spawnChance = value;
            }
        }

        // The event we invoke when the player collided with a red dot
        public static event Action OnLoseGame;

        void Awake()
        {
            IsGoodDot = false;
        }
        /// <summary>
        /// The behavior when the player collided with the red dot
        /// </summary>
        public override void BehaveWhenInteractWithPlayer()
        {
            ShowDestroyParticles(IsGoodDot);
            transform.localScale -= startScale;
            gameObject.SetActive(false);
            if (ActiveShields.AreShieldsActive)
            {
                AudioManager.Instance.PlaySFX("PowerupDisabled");
                ShieldPowerup.OnCollectedShieldPowerup(false);
                ActiveShields.AreShieldsActive = false;
                IronSourceInit.IsShieldFromRV = false;
            }
            else
            {
                AudioManager.Instance.PlaySFX("LoseGame");
                OnLoseGame?.Invoke();
            }
        }
    }
}