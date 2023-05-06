namespace Dots.GamePlay.Powerups.AllGreen
{
    public class AllGreensPowerup : Powerups
    {
        public override void BehaveWhenInteractWithPlayer()
        {
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
            OnCollectedPower?.Invoke();
        }
    } 
}