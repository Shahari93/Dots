using UnityEngine;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Dot
{
    public abstract class DestroyingDots : MonoBehaviour, IInteractableObjects, IDestroyableObject
    {
        [SerializeField] protected ParticleSystem particles;

        protected Vector3 startScale = new Vector3(0.71f, 0.71f, 0f);

        protected bool IsGoodDot { get; set; }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bounds"))
            {
                BehaveWhenInteractWithBounds();
            }
        }

        // What happens if a dot hits the bounds collider
        public void BehaveWhenInteractWithBounds()
        {
            transform.localScale -= startScale;
            DisablePowerupVisuals();
        }

        /// <summary>
        /// Abstract method to control what happens when a dot is hit by the player
        /// </summary>
        public abstract void BehaveWhenInteractWithPlayer();

        public void DisablePowerupVisuals()
        {
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
        }

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