using System;
using Dots.GamePlay.Dot;

public class GoodDot : DotsBehaviour
{
    public static event Action<int> OnPlayerCollectedDot;

    void Awake()
    {
        IsGoodDot = true;
    }

    public override void BehaveWhenIteractWithPlayer()
    {
        base.BehaveWhenIteractWithPlayer();
        OnPlayerCollectedDot?.Invoke(1);
    }
}