using UnityEngine;

namespace Dots.Utils.Spawnable
{
    public interface ISpawnableObjects
    {
        float Speed { get; set; }
        float RandX { get; set; }
        float RandY { get; set; }
        Vector2 Direction { get; set; }

        void SetSpeedAndDirection();
        void BehaveWhenInteractWithBounds();
        void ShowDestroyParticles(bool? isGoodDot = null);
    }
}