using Dots.GamePlay.Dot.Good;
using Dots.Utils.Powerups.Objectpool;

namespace Dots.GamePlay.Powerups.AllGreen
{
    public class AllGreensPowerup : Powerups
    {
        public override void BehaveWhenInteractWithPlayer()
        {
            PowerupsSpawner.CanSpawn = true;
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
            GoodDot.spawnChance = 1f;
        }
    }
}