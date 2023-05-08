using Dots.GamePlay.Dot.Good;

namespace Dots.GamePlay.PowerupsPerent.AllGreen
{
    public class AllGreensPowerup : Powerups
    {
        private void Awake()
        {
            powerupDuration = 5f;
        }

        public override void BehaveWhenInteractWithPlayer()
        {
            base.BehaveWhenInteractWithPlayer();
            GoodDot.spawnChance = 1f;
        }
    }
}