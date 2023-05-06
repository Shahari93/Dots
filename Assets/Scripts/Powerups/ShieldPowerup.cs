using Dots.GamePlay.Dot.Good;
using Dots.GamePlay.Powerups;

namespace Dots.GamePlay.Powerups.Shield
{
    public class ShieldPowerup : Powerups
    {
        public override void BehaveWhenInteractWithPlayer()
        {
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
        }
    } 
}