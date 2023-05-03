using UnityEngine;

namespace Dots.Utils.Spawnable
{
    public interface ISpawnableObjects
    {
        float Speed { get; set; }
        Vector2 Direction { get; set; }
        int RandX { get; set; }
        int RandY { get; set; }

        void SetSpeedAndDirection();
        void BehaveWhenIteractWithBounds();
        void ShowDestroyParticles(bool? isGoodDot = null);
    } 
}