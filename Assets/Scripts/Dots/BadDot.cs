using System;
using Dots.Audio.Manager;
using Dots.GamePlay.Powerups.Shield;
using Dots.GamePlay.Player.Interaction.Shields;

namespace Dots.GamePlay.Dot.Bad
{
    public class BadDot : DestroyingDots
    {
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

        public static event Action OnLoseGame;

        void Awake()
        {
            IsGoodDot = false;
        }

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
            }
            else
            {
                AudioManager.Instance.PlaySFX("LoseGame");
                OnLoseGame?.Invoke();
            }
        }
    }
}