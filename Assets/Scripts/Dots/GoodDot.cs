using System;
using Dots.Audio.Manager;
using CandyCoded.HapticFeedback;
using UnityEngine;

namespace Dots.GamePlay.Dot.Good
{
    public class GoodDot : DotsBehaviour
    {
        public static float spawnChance = 0.15f;
        public static event Action<int> OnPlayerCollectedDot;

        private bool isHapticOn;

        void Awake()
        {
            IsGoodDot = true;
            isHapticOn = Convert.ToBoolean(PlayerPrefs.GetInt("HapticToggle"));
        }

        public override void BehaveWhenInteractWithPlayer()
        {
            if (isHapticOn)
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