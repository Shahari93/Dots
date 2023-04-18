using System;
using Dots.GamePlay.Dot;

public class BadDot : DotsBehaviour
{
    public static event Action OnLoseGame;
    private void Awake()
    {
        IsGoodDot = false;
    }

    public override void BehaveWhenIteractWithPlayer()
    {
        base.BehaveWhenIteractWithPlayer();
        // Trigger lose game
        OnLoseGame?.Invoke();
    }
}