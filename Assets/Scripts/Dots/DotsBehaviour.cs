using UnityEngine;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Dot
{
    public class DotsBehaviour : MonoBehaviour, IInteractableObjects
    {
        float speed;
        Vector2 direction;
        [SerializeField] ParticleSystem particles;
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
         
        // TODO: Find a way to refactor those 2 methods because they do the same thing 
        // What happens if a dot hits the bounds collider
        private void BehaveWhenIteractWithBounds()
        {
            DisableComponnetsWhenDestroied();
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
        }

        // What happens when a player collects the dot
        public virtual void BehaveWhenIteractWithPlayer()
        {
            DisableComponnetsWhenDestroied();
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
        }

        private void DisableComponnetsWhenDestroied()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        public void ShowDestroyParticles(bool isGoodDot)
        {
            GameObject particle = Instantiate(particles.gameObject, this.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
            Color particlesColor = isGoodDot ? Color.green : Color.red;
            ParticleSystem.MainModule main = particleSystem.main;
            main.startColor = particlesColor;
            particleSystem.Play();

            if (particleSystem.isStopped)
            {
                Destroy(particles.gameObject);
            }
        }
    }
}