using UnityEngine;
using Dots.GamePlay.Dot;

public class GoodDot : DotsBehaviour
{
    private void Awake()
    {
        IsGoodDot = true;
    }

    public override void BehaveWhenIteractWithPlayer()
    {
        //Show "good" particles
        ShowDestroyParticles(true);
        Debug.Log("Show good particles");
        base.BehaveWhenIteractWithPlayer();
        // Add point
    }
}
