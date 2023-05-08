using System;
using UnityEngine;

namespace Dots.GamePlay.PowerupsPerent
{
    public abstract class Powerups : MonoBehaviour
    {
        protected float? powerupDuration;
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] protected ParticleSystem particles;

        public static event Action<float?> OnCollectedPower;

    }
}