using Dots.GamePlay.Dot;

public class BadDot : DotsBehaviour
{
    public override void BehaveWhenIteract()
    {
        //Show "bad" particles
        base.BehaveWhenIteract();
        // Trigger lose game
    }
}
