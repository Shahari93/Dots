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
        ShowDestroyParticles(IsGoodDot);
        base.BehaveWhenIteractWithPlayer();
    }
}
