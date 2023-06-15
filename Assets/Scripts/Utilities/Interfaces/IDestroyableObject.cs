namespace Dots.Utils.Interface.Destroy
{
    public interface IDestroyableObject
    {
        void BehaveWhenInteractWithBounds();
        void ShowDestroyParticles(bool? isGoodDot = null);
        void BehaveWhenInteractWithPlayer();
        void DisablePowerupVisuals();
    } 
}