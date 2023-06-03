using System;

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
            ShowDestroyParticles(IsGoodDot);
            transform.localScale -= startScale;
            gameObject.SetActive(false);
            OnPlayerCollectedDot?.Invoke(1);
        }
    } 
}