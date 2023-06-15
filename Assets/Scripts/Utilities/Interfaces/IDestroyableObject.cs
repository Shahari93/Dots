namespace Dots.Utilities.Interface.Destroy
{
    public interface IDestroyableObject
    {
        void BehaveWhenInteractWithBounds();
        void ShowDestroyParticles(bool? isGoodDot = null);
        void BehaveWhenInteractWithPlayer();
        void DisableVisuals();
    } 
}