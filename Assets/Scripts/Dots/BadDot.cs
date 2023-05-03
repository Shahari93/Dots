using System;

namespace Dots.GamePlay.Dot.Bad
{
    public class BadDot : DotsBehaviour
    {
        public static event Action OnLoseGame;

        void Awake()
        {
            IsGoodDot = false;
        }

        public override void BehaveWhenInteractWithPlayer()
        {
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
            OnLoseGame?.Invoke();
        }
    } 
}