using System;
using Dots.Audio.Manager;
using CandyCoded.HapticFeedback;

namespace Dots.GamePlay.Dot.Good
{
    public class GoodDot : DestroyingDots
    {

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

        public static event Action<int> OnPlayerCollectedDot;

        void Awake()
        {
            IsGoodDot = true;
        }

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