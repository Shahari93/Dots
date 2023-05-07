using Dots.Utils.Powerups.Objectpool;

namespace Dots.GamePlay.Powerups.AllGreen
{
    public class AllGreensPowerup : Powerups
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