namespace Dots.GamePlay.Powerups.SlowTime
{
    public class SlowSpawnSpeedPowerup : Powerups
    {
        public override void BehaveWhenInteractWithPlayer()
        {
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
        }
    }
}