using Dots.GamePlay.Dot.Good;

namespace Dots.GamePlay.Powerups.AllGreen
{
    public class AllGreensPowerup : Powerups
    {
        public override void BehaveWhenInteractWithPlayer()
        {
            base.BehaveWhenInteractWithPlayer();
            GoodDot.spawnChance = 1f;
        }
    }
}