using System;

namespace Dots.GamePlay.Dot.Good
{
    public class GoodDot : DotsBehaviour
    {
        public static event Action<int> OnPlayerCollectedDot;

        void Awake()
        {
            IsGoodDot = true;
        }

        public override void BehaveWhenIteractWithPlayer()
        {
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
            OnPlayerCollectedDot?.Invoke(1);
        }
    } 
}