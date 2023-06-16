using UnityEngine;
using Dots.Utilities.Interface.Destroy;
using Dots.Utilities.Interface.Interaction;

namespace Dots.GamePlay.Dot
{
    /// <summary>
    /// This abstract class is the base class for all the dots in the game (Green and Red for now)
    /// It handles all the behaviors when colliding
    /// </summary>
    public abstract class DestroyingDots : MonoBehaviour, IInteractableObjects, IDestroyableObject
    {
        [SerializeField] protected ParticleSystem particles;

        protected Vector3 startScale = new(0.71f, 0.71f, 0f);

        protected bool IsGoodDot { get; set; }

        public abstract float SpawnChance { get; set; }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bounds"))
            {
                BehaveWhenInteractWithBounds();
            }
        }

        /// <summary>
        /// What happens if a dot hits the bounds collider
        /// </summary>
        public void BehaveWhenInteractWithBounds()
        {
            transform.localScale -= startScale;
            DisableVisuals();
        }

        /// <summary>
        /// Abstract method to control what happens when a dot is hit by the player
        /// </summary>
        public abstract void BehaveWhenInteractWithPlayer();

        /// <summary>
        /// Disabling the dots visuals
        /// </summary>
        public void DisableVisuals()
        {
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Showing particles when the dot is being collided with
        /// </summary>
        /// <param name="isGoodDot">If this bool is true we show the green dot particles, if not the red dot particles.
        /// If null we show the powerup particles
        /// </param>
        public void ShowDestroyParticles(bool? isGoodDot)
        {
            GameObject particleGO = Instantiate(particles.gameObject, this.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleGO.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = particleSystem.main;

            Color particlesColor = (bool)isGoodDot ? Color.green : Color.red;
            main.startColor = particlesColor;

            particleSystem.Play();
            Destroy(particleSystem.gameObject, main.duration + 0.1f);
        }
    }
}