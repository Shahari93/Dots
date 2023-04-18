using Dots.GamePlay.Dot;
using System;

public class GoodDot : DotsBehaviour
{
    public static event Action<int> OnPlayerCollectedDot;
    private void Awake()
    {
        IsGoodDot = true;
    }

    public override void BehaveWhenIteractWithPlayer()
    {
        base.BehaveWhenIteractWithPlayer();
        OnPlayerCollectedDot?.Invoke(1);
    }
}
