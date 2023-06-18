using System;
using Dots.Audio.Manager;
using CandyCoded.HapticFeedback;

namespace Dots.GamePlay.Dot.Good
{
    public class GoodDot : DestroyingDots
    {
        /// <summary>
        /// What is the chance of the red dot being spawned
        /// </summary>
        public static float spawnChance = 0.15f;
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
        /// <summary> 
        /// The event we invoke when the player collided with a green dot
        /// </summary>
        public static event Action<int> OnPlayerCollectedDot;

        void Awake()
        {
            IsGoodDot = true;
        }
        /// <summary>
        /// The behavior when the player collided with the red dot 
        /// </summary>
        public override void BehaveWhenInteractWithPlayer()
        {
            if (SettingMenuPresenter.IsHapticOn)
            {
                HapticFeedback.MediumFeedback();
            }

            ShowDestroyParticles(IsGoodDot);
            transform.localScale -= startScale;
            gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX("CollectedGreen");
            OnPlayerCollectedDot?.Invoke(1);
        }
    }
}