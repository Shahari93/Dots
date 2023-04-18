using Dots.GamePlay.Dot;
using System;

public class GoodDot : DotsBehaviour
{
    public static event Action<int> OnPlayerCollectedDot;
    private void Awake()
    {
        IsGoodDot = true;
    }

    public override void BehaveWhenIteract()
    {
        base.BehaveWhenIteract();
        OnPlayerCollectedDot?.Invoke(1);
    }
}
