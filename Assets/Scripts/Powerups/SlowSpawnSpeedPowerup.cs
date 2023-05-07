using Dots.Utils.Powerups.Objectpool;

namespace Dots.GamePlay.Powerups.SlowTime
{
    public class SlowSpawnSpeedPowerup : Powerups
    {
        public override void BehaveWhenInteractWithPlayer()
        {
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
            PowerupsSpawner.CanSpawn = true;
            OnCollectedPower?.Invoke();
        }
    }
}