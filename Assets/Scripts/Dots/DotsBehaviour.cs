using UnityEngine;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Dot
{
    public class DotsBehaviour : MonoBehaviour, IInteractableObjects
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

            // Disable game object componnents 
            DisableComponnetsWhenDestroied();

            // Show Particels based on color 
            ShowDestroyParticles(IsGoodDot);

            // Return dot to pool
            // Make it async and wait until the particles are shown
            gameObject.SetActive(false);
        }

        // What happens when a player collects the dot
        public virtual void BehaveWhenIteractWithPlayer()
        {
            // Disable game object componnents 
            DisableComponnetsWhenDestroied();
            // Show particles
            ShowDestroyParticles(IsGoodDot);

            //Return dot to pool
            // Make it async and wait until the particles are shown
            gameObject.SetActive(false);
        }

        private void DisableComponnetsWhenDestroied()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
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