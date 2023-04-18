using Dots.GamePlay.Dot;
using System;

public class BadDot : DotsBehaviour
{
    public static event Action OnLoseGame;
    private void Awake()
    {
        IsGoodDot = false;
    }

    public override void BehaveWhenIteract()
    {
        base.BehaveWhenIteract();
        // Trigger lose game
        OnLoseGame?.Invoke();
    }
}
