namespace Dots.GamePlay.PowerupsPerent.SlowTime
{
    public class SlowSpawnSpeedPowerup : Powerups
    {
        private void Awake()
        {
            powerupDuration = 5f;
        }
        public override void BehaveWhenInteractWithPlayer()
        {
            base.BehaveWhenInteractWithPlayer();
        }
    }
}