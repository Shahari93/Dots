using UnityEngine;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Dot
{
    public class DotsBehaviour : MonoBehaviour, IInteractableObjects
    {
        float speed;
        Vector2 direction;
        [SerializeField] ParticleSystem particlesSystem;
        [SerializeField] Rigidbody2D rb2D;

        public bool IsGoodDot { get; set; }

        private void OnEnable()
        {
            speed = 80f;
            direction = new Vector2(UnityEngine.Random.Range(-180, 181), UnityEngine.Random.Range(-180, 181)).normalized;
        }

        private void FixedUpdate()
        {
            SetSpeedAndDirection();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bounds"))
            {
                BehaveWhenIteractWithBounds();
            }
        }

        private void SetSpeedAndDirection()
        {
            rb2D.velocity = speed * direction * Time.fixedDeltaTime;
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