using UnityEngine;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Dot
{
    public abstract class DotsBehaviour : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] float speed;
        [SerializeField] Vector2 direction;
        [SerializeField] ParticleSystem particlesSystem;

        public bool IsGoodDot { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bounds"))
            {
                BehaveWhenIteractWithBounds();
            }
        }

        // What happens if a dot hits the bounds collider
        private void BehaveWhenIteractWithBounds()
        {
            // Don't add points / Set fail state
            // Return dot to pool
            // Show Particels based on color 
            ShowDestroyParticles(IsGoodDot);
            //Destroy(gameObject);
        }

        // What happens when a player collects the dot
        public virtual void BehaveWhenIteractWithPlayer()
        {
            //Return dot to pool
            Destroy(gameObject);
        }

        public void ShowDestroyParticles(bool isGoodDot)
        {
            Color particlesColor = isGoodDot ? Color.green : Color.red;
            ParticleSystem.MainModule main = particlesSystem.main;
            main.startColor = particlesColor;
            particlesSystem.Play();
        }
    }
}