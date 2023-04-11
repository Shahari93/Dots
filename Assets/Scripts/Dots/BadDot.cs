using UnityEngine;
using Dots.GamePlay.Dot;

public class BadDot : DotsBehaviour
{
    private void Awake()
    {
        IsGoodDot = false;
    }

    public override void BehaveWhenIteractWithPlayer()
    {
        //Show "bad" particles
        ShowDestroyParticles(false);
        base.BehaveWhenIteractWithPlayer();
        // Trigger lose game
    }
}
