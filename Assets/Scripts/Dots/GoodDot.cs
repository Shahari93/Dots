using System;
using Dots.Audio.Manager;
using CandyCoded.HapticFeedback;

namespace Dots.GamePlay.Dot.Good
{
    public class GoodDot : DotsBehaviour
    {
        public static float spawnChance = 0.15f;
        public static event Action<int> OnPlayerCollectedDot;

        void Awake()
        {
            IsGoodDot = true;
        }

        public override void BehaveWhenInteractWithPlayer()
        {
            HapticFeedback.MediumFeedback();
            ShowDestroyParticles(IsGoodDot);
            transform.localScale -= startScale;
            gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX("CollectedGreen");
            OnPlayerCollectedDot?.Invoke(1);
        }
    } 
}