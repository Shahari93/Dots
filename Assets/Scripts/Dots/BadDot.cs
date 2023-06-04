using System;
using Dots.GamePlay.Powerups.Shield;
using Dots.GamePlay.Player.Interaction.Shields;
using Dots.Audio.Manager;
using System.Threading.Tasks;

namespace Dots.GamePlay.Dot.Bad
{
    public class BadDot : DotsBehaviour
    {
        public static float spawnChance = 0.85f;
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