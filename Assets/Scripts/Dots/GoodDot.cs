using UnityEngine;
using Dots.GamePlay.Dot;

public class GoodDot : DotsBehaviour
{
    private int pointsValue;

    private void Awake()
    {
        IsGoodDot = true;
    }

    public override void BehaveWhenIteractWithPlayer()
    {
        //Show "good" particles
        ShowDestroyParticles(IsGoodDot);
        base.BehaveWhenIteractWithPlayer();
        // Add point
    }
}
