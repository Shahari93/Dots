using Dots.GamePlay.Dot.Good;
using Dots.Utils.Powerups.Objectpool;

namespace Dots.GamePlay.Powerups.AllGreen
{
    public class AllGreensPowerup : Powerups
    {
        public override void BehaveWhenInteractWithPlayer()
        {
            GoodDot.spawnChance = 1f;
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
            PowerupsSpawner.CanSpawn = true;
            OnCollectedPower?.Invoke();
        }
    } 
}