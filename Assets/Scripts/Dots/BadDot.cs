using System;
using Dots.GamePlay.Dot;

public class BadDot : DotsBehaviour
{
    public static event Action OnLoseGame;

    void Awake()
    {
        IsGoodDot = false;
    }

    public override void BehaveWhenIteractWithPlayer()
    {
        ShowDestroyParticles(IsGoodDot);
        gameObject.SetActive(false);
        OnLoseGame?.Invoke();
    }
}